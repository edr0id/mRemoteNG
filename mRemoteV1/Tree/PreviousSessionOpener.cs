﻿using System.Linq;
using mRemoteNG.Connection;
using mRemoteNG.Container;
using mRemoteNG.UI.Controls;


namespace mRemoteNG.Tree
{
    public class PreviousSessionOpener : IConnectionTreeDelegate
    {
        private readonly IConnectionInitiator _connectionInitiator = new ConnectionInitiator();


        public void Execute(ConnectionTree connectionTree)
        {
            if (!Settings.Default.OpenConsFromLastSession || Settings.Default.NoReconnect) return;
            var connectionInfoList = connectionTree.GetRootConnectionNode().GetRecursiveChildList().Where(node => !(node is ContainerInfo));
            var previouslyOpenedConnections = connectionInfoList.Where(item => item.PleaseConnect);
            foreach (var connectionInfo in previouslyOpenedConnections)
            {
                _connectionInitiator.OpenConnection(connectionInfo);
            }
        }
    }
}