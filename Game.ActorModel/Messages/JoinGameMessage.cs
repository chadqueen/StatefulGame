namespace Game.ActorModel.Messages
{
    public class JoinGameMessage
    {
        public string PlayerName { get; private set; }

        public JoinGameMessage(string playerName)
        {
            this.PlayerName = playerName;
        }
    }
}
