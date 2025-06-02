using System.Collections;

namespace SpaceBattle
{
    public class CustomAdapter : IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _sourceData;
        private readonly IDictionary<string, Func<object>> _customBehaviors;

        public CustomAdapter(
            IDictionary<string, object> sourceData,
            IDictionary<string, Func<object>> customBehaviors)
        {
            _sourceData = sourceData;
            _customBehaviors = customBehaviors;
        }

        public object this[string key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        private object GetValue(string key)
        {
            if (_customBehaviors.TryGetValue(key, out var behavior))
            {
                return behavior.Invoke();
            }

            return _sourceData[key];
        }

        private void SetValue(string key, object value)
        {
            if (_customBehaviors.ContainsKey(key))
            {
                throw new InvalidOperationException($"Property '{key}' uses custom behavior and is read-only");
            }

            _sourceData[key] = value;
        }

        public ICollection<string> Keys =>
            new HashSet<string>(_sourceData.Keys.Union(_customBehaviors.Keys));

        public ICollection<object> Values =>
            Keys.Select(k => this[k]).ToList();

        public int Count => Keys.Count;

        public bool IsReadOnly => _sourceData.IsReadOnly;

        public void Add(string key, object value)
        {
            if (_customBehaviors.ContainsKey(key))
            {
                throw new ArgumentException($"Property '{key}' is managed by custom behavior", nameof(key));
            }

            _sourceData.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item) => Add(item.Key, item.Value);

        public bool Remove(string key)
        {
            if (_customBehaviors.ContainsKey(key))
            {
                throw new InvalidOperationException($"Property '{key}' is managed by custom behavior");
            }

            return _sourceData.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item) => Remove(item.Key);

        public void Clear() => _sourceData.Clear();

        public bool ContainsKey(string key) =>
            _sourceData.ContainsKey(key) || _customBehaviors.ContainsKey(key);

        public bool TryGetValue(string key, out object value)
        {
            if (_customBehaviors.TryGetValue(key, out var behavior))
            {
                value = behavior.Invoke();
                return true;
            }

            return _sourceData.TryGetValue(key, out value!);
        }

        public bool Contains(KeyValuePair<string, object> item) =>
            TryGetValue(item.Key, out var val) && Equals(val, item.Value);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            foreach (var key in Keys)
            {
                array[arrayIndex++] = new KeyValuePair<string, object>(key, this[key]);
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var key in Keys)
            {
                yield return new KeyValuePair<string, object>(key, this[key]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
