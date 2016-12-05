using Marketing.Data.Models;
using Marketing.ProductDrafts.Events;
using NServiceBus;
using Shipping.ShippingDetails.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.StockItems.Events;
using Marketing.API.Host.Commands;
using Sales.ProposedPrice.Events;

namespace Marketing.API.Host.Sagas
{
    class ProductDraftSaga
        : Saga<ProductDraftSaga.ProductDraftSagaState>,
        IAmStartedByMessages<IStockItemCreatedEvent>,
        IAmStartedByMessages<IShippingDetailsDefinedEvent>,
        IHandleMessages<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>,
        //IHandleTimeouts<IsPriceApprovedTimeout>,
        IHandleMessages<IProposedPriceAcceptedEvent>,
        IHandleMessages<PublishProductDraftCommand>
    {
        public class ProductDraftSagaState : ContainSagaData
        {
            public string StockItemId { get; set; }
            public bool StockItemReady { get; set; }
            public string ShippingDetailsId { get; set; }
            public bool ShippingDetailsReady { get; set; }
            public string ProductDraftId { get; set; }
            public bool DraftHasAllDetails { get; set; }
            public bool PriceApproved { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProductDraftSagaState> mapper)
        {
            mapper.ConfigureMapping<IStockItemCreatedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<IShippingDetailsDefinedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>(cmd => cmd.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<IProposedPriceAcceptedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<PublishProductDraftCommand>(cmd => cmd.StockItemId).ToSaga(s => s.StockItemId);
        }

        public async Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            Data.StockItemId = message.StockItemId;
            Data.StockItemReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft(context).ConfigureAwait(false);
            }
        }

        public async Task Handle(IShippingDetailsDefinedEvent message, IMessageHandlerContext context)
        {
            Data.ShippingDetailsId = message.ShippingDetailsId;
            Data.ShippingDetailsReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft(context).ConfigureAwait(false);
            }
        }

        public async Task Handle(HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand message, IMessageHandlerContext context)
        {
            var session = context.SynchronizedStorageSession.RavenSession();
            var draft = await session.LoadAsync<ProductDraft>(Data.ProductDraftId);
            draft.Description = message.Description;
            draft.Title = message.Title;
            await session.StoreAsync(draft);

            Data.DraftHasAllDetails = true;

            if (CanPublishProductDraft())
            {
                await PublishProductDraft(context);
            }
            //else
            //{
            //    await RequestTimeout<IsPriceApprovedTimeout>(context, TimeSpan.FromMinutes(10));
            //}
        }

        //public Task Timeout(IsPriceApprovedTimeout state, IMessageHandlerContext context)
        //{
        //    if (!Data.PriceApproved)
        //    {
        //        //Shame on me!
        //    }

        //    return Task.CompletedTask;
        //}

        public async Task Handle(IProposedPriceAcceptedEvent message, IMessageHandlerContext context)
        {
            Data.PriceApproved = true;

            if (CanPublishProductDraft())
            {
                await PublishProductDraft(context);
            }
        }

        bool CanPublishProductDraft()
        {
            return Data.DraftHasAllDetails && Data.PriceApproved;
        }

        async Task PublishProductDraft(IMessageHandlerContext context)
        {
            await context.SendLocal<PublishProductDraftCommand>(cmd => cmd.StockItemId = Data.StockItemId);
        }

        bool CanCreateProductDraft()
        {
            return Data.StockItemReady && Data.ShippingDetailsReady;
        }

        async Task CreateProductDraft(IMessageHandlerContext context)
        {
            var session = context.SynchronizedStorageSession.RavenSession();

            var draft = new ProductDraft()
            {
                StockItemId = Data.StockItemId,
            };

            await session.StoreAsync(draft).ConfigureAwait(false);

            Data.ProductDraftId = draft.Id;

            await context.Publish<IProductDraftCreatedEvent>(e =>
            {
                e.ProductDraftId = draft.Id;
                e.StockItemId = draft.StockItemId;
            })
            .ConfigureAwait(false);
        }

        public async Task Handle(PublishProductDraftCommand message, IMessageHandlerContext context)
        {
            var session = context.SynchronizedStorageSession.RavenSession();
            var draft = await session.LoadAsync<ProductDraft>(Data.ProductDraftId);
            var home = await session.LoadAsync<HomeStructure>("HomeStructure");

            var product = new Product()
            {
                Description = draft.Description,
                Title = draft.Title,
                StockItemId = draft.StockItemId
            };

            await session.StoreAsync(product);

            var lastIdx = home.ShowcaseStockItemIds.Length - 1;
            home.ShowcaseStockItemIds[lastIdx] = draft.StockItemId;
            await session.StoreAsync(home);

            session.Delete(draft);

            await context.Publish<IProductDraftPublishedEvent>(e =>
            {
                e.StockItemId = draft.StockItemId;
            });

            MarkAsComplete();
        }
    }

    class IsPriceApprovedTimeout
    {

    }
}