using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.Network
{
    class Server
    {


        #region methods

        public void StartServer()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2")
            { Port = 12345 };

            NetServer server = new NetServer(config);
            server.Start();
        }
        

        #endregion

    }
}
