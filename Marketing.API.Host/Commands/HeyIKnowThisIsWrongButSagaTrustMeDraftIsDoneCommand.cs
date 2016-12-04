using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.API.Host.Commands
{
    class HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand
    {
        public dynamic Description { get; internal set; }
        public dynamic ProductDraftId { get; internal set; }
        public string StockItemId { get; set; }
        public dynamic Title { get; internal set; }
    }
}
