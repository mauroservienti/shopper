using HttpHelpers;
using NServiceBus;
using Shipping.Data.Models;
using Shipping.ShippingDetails.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warehouse.StockItems.Events;

namespace Marketing.API.Host.Handlers
{
    public class StockItemCreatedHandler : IHandleMessages<IStockItemCreatedEvent>
    {
        public async Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            var client = new HttpClient();
            var warehouse = ConfigurationManager.AppSettings["warehouse/baseAddress"];
            var url = $"{warehouse}StockItems/Sizings?id={message.StockItemId}";

            var result = await client.GetAsync(url).ConfigureAwait(false);
            dynamic sizings = await result.Content.AsExpandoAsync();

            var details = new ShippingDetails()
            {
                StockItemId = message.StockItemId
            };

            if (sizings.Weight <= 5)
            {
                details.Weight = 5;
                details.Cost = 2;
            }
            else if (sizings.Weight > 5 && sizings.Weight <= 10)
            {
                details.Weight = 10;
                details.Cost = 4;
            }
            else if (sizings.Weight > 10 && sizings.Weight <= 50)
            {
                details.Weight = 50;
                details.Cost = 35;
            }
            else
            {
                details.Weight = 100;
                details.Cost = 50;
            }

            var session = context.SynchronizedStorageSession.RavenSession();
            await session.StoreAsync(details).ConfigureAwait(false);

            await context.Publish<IShippingDetailsDefinedEvent>(e =>
            {
                e.ShippingDetailsId = details.Id;
                e.StockItemId = details.StockItemId;
            }).ConfigureAwait(false);
        }
    }
}
