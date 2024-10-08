namespace Aoxe.ZeroMQ;

public static class AoxeZeroMqServiceProviderExtensions
{
    public static IServiceCollection AddAoxeZeroMq(
        this IServiceCollection services,
        Func<AoxeZeroMqOptions> optionsFactory
    ) => services.AddSingleton<IAoxeZeroMessageBus>(new AoxeZeroMessageBus(optionsFactory));

    public static IServiceCollection AddAoxeZeroMq(
        this IServiceCollection services,
        AoxeZeroMqOptions options
    ) => services.AddSingleton<IAoxeZeroMessageBus>(new AoxeZeroMessageBus(options));

    public static IServiceCollection AddAoxeZeroMq(
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
