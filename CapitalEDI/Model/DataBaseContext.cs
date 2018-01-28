using CapitalEDI.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Model
{
    class DataBaseContext : DbContext
    {
        public DataBaseContext() : base(ConnectionDataBase.Read())
        {

        }

    }
}
