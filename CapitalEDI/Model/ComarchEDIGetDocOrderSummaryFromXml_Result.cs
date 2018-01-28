using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    using System;
    public partial  class ComarchEDIGetDocOrderSummaryFromXml_Result
    {
        public string КоличествоПолей { get; set; }
        public string ОбщееКолТовара { get; set; }
        public string СуммаБезНДС { get; set; }
        public string СуммаЗПДВ { get; set; }
    }
}
