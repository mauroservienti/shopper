using NServiceBus;
using System.Threading.Tasks;
using Sales.ProposedPrice.Events;
using Marketing.ProductDrafts.Events;
using Sales.API.Host.Commands;
using System;
using Sales.Data.Models;

namespace Sales.API.Host.Sagas
{
    class ProposedPriceSaga
        : Saga<ProposedPriceSaga.ProposedPriceSagaState>,
        IAmStartedByMessages<ProposePriceCommand>,
        IHandleMessages<IProductDraftPublishedEvent>
    {
        public class ProposedPriceSagaState : ContainSagaData
        {
            public string StockItemId { get; set; }
            public string ItemPriceId { get; set; }
            public double ProposedPrice { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProposedPriceSagaState> mapper)
        {
            mapper.ConfigureMapping<ProposePriceCommand>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<IProductDraftPublishedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
        }

        public async Task Handle(ProposePriceCommand message, IMessageHandlerContext context)
        {
            Data.ProposedPrice = message.ProposedPrice;

            ItemPrice itemPrice = null;
            var session = context.SynchronizedStorageSession.RavenSession();
            if (String.IsNullOrWhiteSpace(Data.ItemPriceId))
            {
                itemPrice = new ItemPrice()
                {
                    StockItemId = message.StockItemId,
                    StreetPrice = message.ProposedPrice
                };
                await session.StoreAsync(itemPrice);

                Data.ItemPriceId = itemPrice.Id;
            }
            else
            {
                itemPrice = await session.LoadAsync<ItemPrice>(Data.ItemPriceId);
                itemPrice.StreetPrice = message.ProposedPrice;
                await session.StoreAsync(itemPrice);
            }

            await context.Publish<IProposedPriceAcceptedEvent>(e => e.StockItemId = itemPrice.StockItemId);
        }

        public Task Handle(IProductDraftPublishedEvent message, IMessageHandlerContext context)
        {
            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}