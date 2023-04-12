namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public abstract class ServiceConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        BitPayPropertiesConfiguration.Execute(builder);
        BitPayClientConfiguration.Execute(builder);
        ContextConfiguration.Execute(builder);
        DependencyInjectionConfiguration.Execute(builder);
    }
}