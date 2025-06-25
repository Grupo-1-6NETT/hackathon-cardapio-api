using Application.Commands;
using Application.Services;
using MassTransit;

namespace CardapioApi;

public static class ConfigExtensions
{
    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(c =>
            c.RegisterServicesFromAssembly(typeof(AddItemCommand).Assembly)
        );

        return services;
    }

    public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration config)
    {
        var servidor = config.GetSection("RabbitMQ")["Hostname"] ?? string.Empty;
        var usuario = config.GetSection("RabbitMQ")["Username"] ?? string.Empty;
        var senha = config.GetSection("RabbitMQ")["Password"] ?? string.Empty;

        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {                    
                    h.Username(usuario);
                    h.Password(senha);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
