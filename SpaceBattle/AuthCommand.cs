using App;

namespace SpaceBattle
{
    public class AuthCommand : ICommand
    {
        public string _senderId;
        public string _receiverId;
        public AuthCommand(string senderId, string receiverId)
        {
            _senderId = senderId;
            _receiverId = receiverId;
        }
        public void Execute()
        {
            if (_senderId != _receiverId)
            {
                throw new Exception();
            }
        }
    }
}