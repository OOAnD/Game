namespace SpaceBattle
{
    public class GameLoop
    {
        private readonly IQueueCount _queueCount;

        public GameLoop(IQueueCount queueCount)
        {
            _queueCount = queueCount;
        }

        public bool ShouldLoopRun()
        {
            return _queueCount.Count() > 0;
        }
    }
}
