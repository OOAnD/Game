namespace SpaceBattle
{
    public class GameState
    {
        private bool _isRunning;

        public GameState()
        {
            _isRunning = true;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
