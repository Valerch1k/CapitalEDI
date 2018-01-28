using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    using System;
   public  class ComarchEDIGetDocOrderHeaderFromXml_Result
    {
        public string НомерЗаказа { get; set; }
        public string ДатаЗаказа { get; set; }
        public string ОжидаемаяДатаДоставки { get; set; }
        public string ОжидаемоеВремяДоствки { get; set; }
        public string НомерДоговора { get; set; }
        public string Валюта { get; set; }
        public string ТипДокумента { get; set; }
        public string КодПромоАкции { get; set; }
        public string Remarks { get; set; } 
    }
}
