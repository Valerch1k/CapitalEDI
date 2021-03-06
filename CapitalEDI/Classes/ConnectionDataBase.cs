﻿using Microsoft.Data.ConnectionUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CapitalEDI.Classes
{
    /// <summary>
    /// Содержит методы для изменения , удаления и возврата строки подключения 
    /// </summary> 
    class ConnectionDataBase
    {
       
        private static object sync = new object();

        /// <summary>
        ///  Перезаписывает строку подключения к базе данных
        /// </summary>
        /// <param name="fullText"> строка подключения </param>
        public static void Write(string connectDateBase)
        {
            Properties.Settings App = new Properties.Settings();
            App.ConnectionDataBase = connectDateBase;
            App.Save(); 
        }
        /// <summary>
        /// Удаляет строку подключения
        /// </summary>
        public static void Delete()
        {
            Properties.Settings App = new Properties.Settings();
            App.ConnectionDataBase = "";
            App.Save();
        }

        /// <summary>
        /// Возвращает строку подключения 
        /// </summary>
        /// <returns> строка подключения</returns>
        public static string Read()
        {
            Properties.Settings App = new Properties.Settings();
            return App.ConnectionDataBase;
        }

        /// <summary>
        /// метод вызываюзщий диалоговое окно подключения к базе данных и изменяет строку подключения 
        /// </summary>
        public static void ChangeConnect()
        {
            DataConnectionDialog dcd = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);

            if (DataConnectionDialog.Show(dcd) == System.Windows.Forms.DialogResult.OK)
            {
                // load tables
                using (SqlConnection connection = new SqlConnection(dcd.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables", connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.HasRows);
                        }
                    }
                }
            }
            else
            {
                Environment.Exit(0);
            }
            dcs.SaveConfiguration(dcd);
            Write(dcd.ConnectionString);
          
        }





    }
}
