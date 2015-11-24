using Akka.Actor;
using Game.ActorModel.Messages;
using System.Collections.Generic;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;

        public GameControllerActor()
        {
            this._players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(
                message =>
                {
                    this.JoinGame(message);
                }
            );

            Receive<AttackPlayerMessage>(
                message =>
                {
                    this._players[message.PlayerName].Forward(message);
                }
            );
        }

        private void JoinGame(JoinGameMessage message)
        {
            var playerNeedsCreating = !this._players.ContainsKey(message.PlayerName);

            if (playerNeedsCreating)
            {
                IActorRef newPlayerActor =
                    Context.ActorOf(
                        Props.Create(() => new PlayorActor(message.PlayerName, 100)),
                        message.PlayerName
                    );

                this._players.Add(message.PlayerName, newPlayerActor);

                foreach (var player in this._players.Values)
                {
                    player.Tell(new RefreshPlayerStatusMessage(), Sender);
                }
            }
        }
    }
}
