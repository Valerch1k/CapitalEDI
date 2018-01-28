using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    public partial class ComarchEDIЖурналРасходныхНакладных
    {
        public short ID_ВидОперации { get; set; }
        public Nullable<int> НомерОперации { get; set; }
        public string Наименование { get; set; }
        public string ДатаТекст { get; set; }
        public Nullable<decimal> СуммаКоплате2 { get; set; }
        public Nullable<decimal> СуммаКоплате1 { get; set; }
        public Nullable<decimal> СуммаТары2 { get; set; }
        public Nullable<decimal> СуммаТары1 { get; set; }
        public Nullable<decimal> СуммаВсего1 { get; set; }
        public Nullable<decimal> СуммаВсего2 { get; set; }
        public string p30 { get; set; }
        public byte AccessLevel { get; set; }
        public short Locked { get; set; }
        public System.Guid ID_Операции { get; set; }
        public System.Guid ID_Registr { get; set; }
        public byte ID_ВидАналитики { get; set; }
        public short ID_ВидТДокумента { get; set; }
        public System.DateTime ДатаОперации { get; set; }
        public decimal СуммаТовара1 { get; set; }
        public decimal СуммаТовара2 { get; set; }
        public decimal СуммаУслуг1 { get; set; }
        public decimal СуммаУслуг2 { get; set; }
        public string Содержание { get; set; }
        public short ФлагПроводки { get; set; }
        public short f1 { get; set; }
        public Nullable<int> НомерРейса { get; set; }
        public Nullable<decimal> СуммаНДСтовара { get; set; }
        public Nullable<decimal> СуммаНДСтары { get; set; }
        public int ЕстьНалоговая { get; set; }
        public string Direction { get; set; }
        public string Кратко { get; set; }
        public int WHNumber { get; set; }
        public Nullable<int> НомерОперацииМаршрута { get; set; }
        public Nullable<System.Guid> ID_ОперацииМаршрута { get; set; }
        public Boolean isExport { get; set; }
        public Boolean IsDESADV { get; set; }
        public Boolean IsORDRSP { get; set; } 
    }
}
