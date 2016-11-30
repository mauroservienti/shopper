using NServiceBus;
using System;
using System.Threading.Tasks;
using Warehouse.StockItems.Events;

namespace Marketing.API.Host.Handlers
{
    public class StockItemCreatedHandler : IHandleMessages<IStockItemCreatedEvent>
    {
        public Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
