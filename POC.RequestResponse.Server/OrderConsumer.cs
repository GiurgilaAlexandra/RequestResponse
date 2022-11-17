using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace POC.RequestResponse.Server
{
    internal class OrderConsumer : IConsumer<OrderRequest>
    {
        public async Task Consume(ConsumeContext<OrderRequest> context)
        {

            var response = new OrderResponse { Result = "The client input is " + context.Message.Name };
            Console.WriteLine("Received name " + context.Message.Name);
            await context.RespondAsync<OrderResponse>(response);
            Console.WriteLine("Response sent");
        }
    }
}
