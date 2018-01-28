using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    using System;
    public partial class ComarchEDIGetDocOrderInfoBuyerFromXml_Result
    {
        public string GLNПокупателя { get; set; }
        public string GLNОтправителя { get; set; }
        public string GLNпродавца { get; set; }
        public string КодПокупателя { get; set; }
        public string ЗаказаСформировал { get; set; }
        public string GLNМестаДоставки { get; set; }
        public string GLNПолучателяНакладной { get; set; }
        public string DeliveryPlace { get; set; }
    }

}
