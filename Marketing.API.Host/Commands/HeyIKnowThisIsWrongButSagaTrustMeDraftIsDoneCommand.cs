using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.API.Host.Commands
{
    class HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand
    {
        public string Description { get; set; }
        public string ProductDraftId { get; set; }
        public string StockItemId { get; set; }
        public string Title { get; set; }
    }
}
