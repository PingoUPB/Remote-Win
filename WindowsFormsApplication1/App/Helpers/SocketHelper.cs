using System;
using SocketIOClient;
using SocketIOClient.Messages;
using WinRemote.App.Controllers;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Provides connection to websocket on PINGO server.
    /// </summary>
    internal class SocketHelper
    {
        #region fields
        /// <summary>
        /// The custom EventHandler to manage the SocketHelper's events (CountdownChanged and VotersChanged). Uses the custom EventArgs
        /// called SocketEventArgs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SocketEventHandler(object sender, SocketEventArgs e);
        /// <summary>
        /// This Event is called when Helper recieves a message of type "countdown".
        /// </summary>
        public event SocketEventHandler CountdownChanged;
        /// <summary>
        /// This Event is called when Helper recieves a message of type "voter_count"
        /// </summary>
        public event SocketEventHandler VotersChanged;
        /// <summary>
        /// Needed for every socket action.
        /// </summary>
        private Client _socket;
        #endregion

        /// <summary>
        /// Builds the socket connection. Handles different message types.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Starting TestSocketIOClient Example...");

            _socket = new Client(Settings.BaseSocketUrl); // url to the nodejs / socket.io instance
            //Add default listeners
            _socket.Opened += SocketOpened;
            _socket.Message += SocketMessage;
            _socket.SocketConnectionClosed += SocketConnectionClosed;
            _socket.Error += SocketError;
            _socket.HeartBeatTimerEvent += HeartBeat;

            _socket.On("message", data =>
            {
                //Deserialize or the cast below do somehow not work on the data object.
                var msg = PingoRemoteEvent.FromMsg(data);

                // cast message as JuggernautMessage - use type cast helper
                // JuggernautMessage msg = data.Json.GetFirstArgAs<JuggernautMessage>();
                

                if (msg.Type.Equals("countdown") && CountdownChanged != null)
                    CountdownChanged(this, new SocketEventArgs((Convert.ToDouble(msg.Payload) / 1000).ToString()));
                else if (msg.Type.Equals("voter_count") && VotersChanged != null)
                    VotersChanged(this, new SocketEventArgs(msg.Payload.ToString()));
            });

            // register for 'connect' event with io server
            _socket.On("connect", fn =>
            {
                Console.WriteLine("\r\nConnected event...\r\n");
                Console.WriteLine("Emit Subscribe object");
                JuggernautMessage msg = new JuggernautMessage { Channel = "sess" + Settings.Session.Token, Type = "subscribe" };

                Console.Out.Write("sending: ");
                Console.Out.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(msg, Newtonsoft.Json.Formatting.None));

                // emit Json Serializable object, anonymous types, or strings
                _socket.Emit("message", msg.ToJsonString());
                Console.Out.WriteLine("sent over the wire: " + Newtonsoft.Json.JsonConvert.SerializeObject(new JSONMessage(msg), Newtonsoft.Json.Formatting.None));
            });

            _socket.On("json", data =>
            {
                Console.WriteLine("recv [socket].[session/event] json event");
                Console.WriteLine("  raw message:      {0}", data.RawMessage);
                Console.WriteLine("  string message:   {0}", data.MessageText);
                Console.WriteLine("  json data string: {0}", data.Json.ToJsonString());
                Console.WriteLine("  json raw:         {0}", data.Json.Args[0]);

                // cast message as JuggernautMessage - use type cast helper
                JuggernautMessage msg = data.Json.GetFirstArgAs<JuggernautMessage>();
                Console.WriteLine(" Message/Event Type:   {0}\r\n", msg.Type);
            });

            // make the socket.io connection
            _socket.Connect();
        }

        #region default socket methods
        private void SocketError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("socket client error:");
            Console.WriteLine(e.Message);
        }

        private void SocketConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("WebSocketConnection was terminated!");
        }

        private void SocketMessage(object sender, MessageEventArgs e)
        {
            // uncomment to show any non-registered messages
            if (string.IsNullOrEmpty(e.Message.Event))
                Console.WriteLine("Generic SocketMessage: {0}", e.Message.MessageText);
            else
                Console.WriteLine("Generic SocketMessage: {0} : {1}", e.Message.Event, e.Message.Json.ToJsonString());
        }

        private void SocketOpened(object sender, EventArgs e)
        {
            Console.WriteLine("socket opened");
        }

        private void HeartBeat(object sender, EventArgs e)
        {
            Console.WriteLine("heartbeat event");
        }

        public void Close()
        {
            if (_socket != null)
            {
                _socket.Opened -= SocketOpened;
                _socket.Message -= SocketMessage;
                _socket.SocketConnectionClosed -= SocketConnectionClosed;
                _socket.Error -= SocketError;
                _socket.Dispose(); // close & dispose of socket client
            }
        }
        #endregion

        #region channel subscription
        /// <summary>
        /// Subscribe to a channel.
        /// </summary>
        /// <param name="channel">the channel to be subscribed to</param>
        public void Subscribe(string channel)
        {
            JuggernautMessage msg = new JuggernautMessage { Channel = channel, Type = "subscribe" };

            _socket.Emit("message", msg.ToJsonString());
        }

        /// <summary>
        /// Subscribes to a survey's countdown.
        /// </summary>
        /// <param name="surveyId">the survey's ID</param>
        public void SubscribeToSurveyCountdown(string surveyId)
        {
            Subscribe("s" + surveyId);
        }

        /// <summary>
        /// Subscribes to a survey's current voter count
        /// </summary>
        /// <param name="surveyId">the survey's ID</param>
        public void SubscribeToSurveyVoters(string surveyId)
        {
            Subscribe("v" + surveyId);
        }
        #endregion
    }
}