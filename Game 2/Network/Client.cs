using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.Network
{
    class Client
    {
        #region fields

        private NetClient _client;

        #endregion


        #region methods

        public void startClient()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2");
            _client = new NetClient(config);
            _client.Start();
            _client.Connect(host: "127.0.0.1", port: 12345);
        }

        


        #endregion

    }
}
