namespace Zaabee.ZeroMQ.Abstractions;

public interface IZaabeeZeroMessageBus : IScatterSocket, IGatherSocket, IRadioSocket, IDishSocket, IClientSocket,
    IServerSocket, IDisposable
{
}