using System.Diagnostics;
using App;

namespace SpaceBattle
{
    public class TimeLimitCommand : ICommand
    {
        private readonly Stopwatch _stopwatch;
        private readonly GameState _gameState;
        private readonly int _quantum;

        public TimeLimitCommand(GameState gameState, int quantum)
        {
            _gameState = gameState;
            _quantum = quantum;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Execute()
        {
            if (_stopwatch.ElapsedMilliseconds >= _quantum)
            {
                _gameState.Stop();
            }
        }
    }
}
