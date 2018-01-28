using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    using System;
    public partial class ComarchEDIGetDocOrderLineItemFromXml_Result
    {
        public string EAN { get; set; }
        public string Товар { get; set; }
        public string КодПокупателя { get; set; }
        public string ОбратнаяТара { get; set; }
        public string Количество { get; set; }
        public string КолВУпаковке { get; set; }
        public string ЕИ { get; set; }
        public string ЦенаБезНДС { get; set; }
        public string СумаБезПДВ { get; set; }
        public string СтавкаНДС { get; set; }
        public string ЦенаСНДС { get; set; }
        public string СумаЗПДВ { get; set; }
    }
}
