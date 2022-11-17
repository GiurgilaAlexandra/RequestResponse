// See https://aka.ms/new-console-template for more information

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using POC.RequestResponse.Server;

var serviceCollection = new ServiceCollection();
serviceCollection.AddMassTransit(ConfigureRabbitmq);

var serviceProvider = serviceCollection.BuildServiceProvider();
serviceProvider.GetService<IBusControl>().Start();
var client = serviceProvider.GetService<IRequestClient<OrderRequest>>();

var input = Console.ReadLine();
var response = await client.GetResponse<OrderResponse>(new OrderRequest { Name = input });
Console.WriteLine("Received: " + response.Message.Result);
Console.ReadKey();

static void ConfigureRabbitmq(IBusRegistrationConfigurator busRegistrationConfigurator)
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
