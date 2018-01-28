using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Classes
{
    /// <summary>
    ///  Проверяет приложение на работоспособность
    /// </summary>
    class StatusApp
    {
        /// <summary>
        ///  Проверка подключения к БД
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDataBase.Read()))
                {
                    int dircount = 0;
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT IsNull(Object_id('ComarchEDIGetDirection'),0)", connection); //count(*) FROM sys.Tables
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dircount = Convert.ToInt32(reader[0]);
                        }
                    }
                    if (dircount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
