// See https://aka.ms/new-console-template for more information
using MassTransit;
using Microsoft.Extensions.Hosting;
using POC.RequestResponse.Server;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddMassTransit(x =>
        {
            ConfigureRabbitmq(x);
            x.AddConsumer<OrderConsumer>();
        });

    })
    .Build();

host.Run();

static void ConfigureRabbitmq( IBusRegistrationConfigurator busRegistrationConfigurator)
{
    busRegistrationConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
}