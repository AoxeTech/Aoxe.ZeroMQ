namespace Aoxe.ZeroMQ;

public static class AoxeZeroMqServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<AoxeZeroMqOptions> optionsFunc
    ) => services.AddSingleton<IAoxeZeroMessageBus>(new AoxeZeroMessageBus(optionsFunc.Invoke()));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        AoxeZeroMqOptions options
    ) => services.AddSingleton<IAoxeZeroMessageBus>(new AoxeZeroMessageBus(options));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        IBytesSerializer serializer,
        string? serverBindAddress = null,
        string? clientConnectAddress = null,
        string? scatterBindAddress = null,
        string? gatherConnectAddress = null,
        string? radioBindAddress = null,
        string? dishConnectAddress = null
    ) =>
        services.AddSingleton<IAoxeZeroMessageBus>(
            new AoxeZeroMessageBus(
                serializer,
                serverBindAddress,
                clientConnectAddress,
                scatterBindAddress,
                gatherConnectAddress,
                radioBindAddress,
                dishConnectAddress
            )
        );
}
