using System;
using Zaabee.ZeroMQ.Abstractions.Socket.Pipeline;
using Zaabee.ZeroMQ.Abstractions.Socket.PubSub;
using Zaabee.ZeroMQ.Abstractions.Socket.RequestReply;

namespace Zaabee.ZeroMQ.Abstractions
{
    public interface IZaabeeZeroMqHub : IScatterSocket, IGatherSocket, IRadioSocket, IDishSocket, IClientSocket,
        IServerSocket, IDisposable
    {

    }
}