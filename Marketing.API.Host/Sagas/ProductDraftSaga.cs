using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.StockItems.Events;

namespace Marketing.API.Host.Sagas
{
    class ProductDraftSaga 
        : Saga<ProductDraftSaga.State>,
        IAmStartedByMessages<IStockItemCreatedEvent>
    {
        public class State : ContainSagaData
        {
            public virtual int StockItemId { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<IStockItemCreatedEvent>(e => e.Id).ToSaga(s => s.StockItemId);
        }

        public Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
