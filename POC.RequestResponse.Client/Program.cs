// See https://aka.ms/new-console-template for more information

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using POC.RequestResponse.Server;

var x = new ServiceCollection();
x.AddMassTransit(x =>
{
    ConfigureRabbitmq(x);
    x.AddRequestClient<OrderRequest>(new Uri("exchange:OrderRequest"));
});
var serviceProvider = x.BuildServiceProvider();
IRequestClient<OrderRequest> client = serviceProvider.GetService<IRequestClient<OrderRequest>>();
var input = Console.ReadLine();
var response = await client.GetResponse<OrderResponse>(new OrderRequest { Name = input });
Console.WriteLine("Received: " + response.Message.Result);


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
