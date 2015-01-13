using System;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Custom EventArgs with payload attribute.
    /// </summary>
    internal class SocketEventArgs : EventArgs
    {   
        /// <summary>
        /// The message's payload is given to the event's listeners.
        /// </summary>
        public string Payload;

        /// <summary>
        /// Sets the payload.
        /// </summary>
        /// <param name="payload">the message's payload</param>
        public SocketEventArgs(string payload)
        {
            Payload = payload;
        }
    }
}