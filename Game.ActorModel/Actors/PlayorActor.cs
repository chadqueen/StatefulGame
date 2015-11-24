using Akka.Actor;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class PlayorActor: ReceiveActor
    {
        private readonly string _playerName;

        private int _health;

        public PlayorActor(string playerName, int health)
        {
            this._playerName = playerName;
            this._health = health;

            Receive<AttackPlayerMessage>(
                message =>
                {
                    this._health -= 20;

                    Sender.Tell(new PlayerHealthChangedMessage(this._playerName, this._health));
                }    
            );

            Receive<RefreshPlayerStatusMessage>(
                message =>
                {
                    Sender.Tell(new PlayerStatusMessage(this._playerName, this._health));
                }
            );
        }
    }
}
