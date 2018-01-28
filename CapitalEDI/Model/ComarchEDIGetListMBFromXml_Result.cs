using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    public partial class ComarchEDIGetListMBFromXml_Result
    {
        public string partner_iln { get; set; }
        public string tracking_id { get; set; }
        public string document_type_id { get; set; } 
        public string document_type { get; set; }
        public string document_version { get; set; }
        public string document_standard { get; set; }
        public string document_test { get; set; }
        public string document_status { get; set; }
        public string document_number { get; set; }
        public string document_date { get; set; }
        public string document_control_number { get; set; }
        public string receive_date { get; set; }

    }
}
