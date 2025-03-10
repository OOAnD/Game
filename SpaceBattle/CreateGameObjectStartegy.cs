namespace SpaceBattle
{
    public class CreateTorpedoStrategy
    {
        public static object Create()
        {
            var torpedo = new Dictionary<string, object>();
            var id = Guid.NewGuid().ToString();
            torpedo["type"] = "torpedo";
            torpedo["id"] = id;

            return torpedo;
        }
    }
}
