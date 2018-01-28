using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    using System;
    public partial class RelationshipsFromXml_Result
    {
        public string relation_id { get; set; }
        public string partner_iln { get; set; }
        public string partner_name { get; set; }
        public string document_type_id { get; set; }
        public string document_type { get; set; }
        public string document_version { get; set; }
        public string document_standard { get; set; }
        public string document_test { get; set; }
        public string description { get; set; }
        public string test { get; set; }
        public string form { get; set; }
        public string direction { get; set; }
    }
}
