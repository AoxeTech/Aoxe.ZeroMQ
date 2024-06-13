namespace Aoxe.ZeroMQ.Abstractions;

public interface IAoxeZeroMessageBus
    : IScatterSocket,
        IGatherSocket,
        IRadioSocket,
        IDishSocket,
        IClientSocket,
        IServerSocket,
        IDisposable { }
