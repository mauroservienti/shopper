using NServiceBus;
using Shipping.ShippingDetails.Events;
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
        IAmStartedByMessages<IStockItemCreatedEvent>,
        IAmStartedByMessages<IShippingDetailsDefinedEvent>
    {
        public class State : ContainSagaData
        {
            public virtual int StockItemId { get; set; }
            public virtual bool StockItemReady { get; set; }
            public virtual int ShippingDetailsId { get; set; }
            public virtual bool ShippingDetailsReady { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<IStockItemCreatedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<IShippingDetailsDefinedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
        }

        public async Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            Data.StockItemId = message.StockItemId;
            Data.StockItemReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft().ConfigureAwait(false);
            }
        }

        public async Task Handle(IShippingDetailsDefinedEvent message, IMessageHandlerContext context)
        {
            Data.ShippingDetailsId = message.ShippingDetailsId;
            Data.ShippingDetailsReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft().ConfigureAwait(false);
            }
        }

        bool CanCreateProductDraft()
        {
            return Data.StockItemReady && Data.ShippingDetailsReady;
        }

        async Task CreateProductDraft()
        {
            //scrive sul db
            //pubblica evento
            await Task.CompletedTask;
        }
    }
}
