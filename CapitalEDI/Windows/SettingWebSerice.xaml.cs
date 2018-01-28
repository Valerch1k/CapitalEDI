using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapitalEDI.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingWebSerice.xaml
    /// </summary>
    public partial class SettingWebSerice : Window
    {
        Properties.Settings App = new Properties.Settings();
        public SettingWebSerice()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
            txtLogin.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtLogin.Text = App.LoginWebService;
            txtPassword.Password = App.PasswordWebService;
        }

        private void checkActiveTextBox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkActiveTextBox.IsChecked)
            {
                txtLogin.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnSave.IsEnabled = true;
            }
        }

        private void checkActiveTextBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!(bool)checkActiveTextBox.IsChecked)
            {
                txtLogin.IsEnabled = false;
                txtPassword.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkActiveTextBox.IsChecked)
            {
                App.LoginWebService = txtLogin.Text;
                App.PasswordWebService = txtPassword.Password;
                App.Save();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                App.LoginWebService = txtLogin.Text;
                App.PasswordWebService = txtPassword.Password;
                App.Save();
                Close();
            }
        }
    }
}
