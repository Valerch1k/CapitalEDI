using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using CapitalEDI.ServiceReference;
using CapitalEDI.Classes;
using CapitalEDI.Windows;

namespace CapitalEDI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Properties.Settings App = new Properties.Settings();
            // провереям подключение к WebService
            var client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string result = client.Relationships(App.LoginWebService,App.PasswordWebService, 1000).Res;
            client.Close();
            if (!String.IsNullOrEmpty(SoapRequest.ResultWebRequest(result)))
            {
                MessageBox.Show(SoapRequest.ResultWebRequest(result), "Подключения к WebService", MessageBoxButton.OK, MessageBoxImage.Error);
                SettingWebSerice settingForm = new SettingWebSerice();
                settingForm.ShowDialog();
                // перезагружаем приложение
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }

            // проверяем подключения к бд
            if (!StatusApp.CheckConnection())
            {
                MessageBox.Show("Нет соединение с базой данных , проверьте параметры подключения или обратитесь к системному администратору ", "Cоединение с базой данных", MessageBoxButton.OK, MessageBoxImage.Hand);
                ConnectionDataBase.ChangeConnect();
            }


        }
    }
}
