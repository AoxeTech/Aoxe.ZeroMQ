using System;
using Zaabee.ZeroMQ.Abstraction.Socket.Pipeline;
using Zaabee.ZeroMQ.Abstraction.Socket.PubSub;
using Zaabee.ZeroMQ.Abstraction.Socket.RequestReply;

namespace Zaabee.ZeroMQ.Abstraction
{
    public interface IZaabeeZeroMqHub : IScatterSocket, IGatherSocket, IRadioSocket, IDishSocket, IClientSocket,
        IServerSocket, IDisposable
    {

    }
}