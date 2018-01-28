using CapitalEDI.Classes;
using CapitalEDI.Model;
using CapitalEDI.ServiceReference;
using CapitalEDI.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapitalEDI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Properties.Settings App = new Properties.Settings();
        public MainWindow()
        {
            InitializeComponent();
            #region StatusBarElements
            lblStatus.Content = "WebService OK";
            EllipseStatus.Fill = Brushes.GreenYellow;
            lblConnectionString.Content = ConnectionDataBase.Read();
            #endregion
            // выводим список направлений
            cbxDirection.ItemsSource = StoredProcedureItems.getDirection();
            // выводим список партнеров в EDI 
            cbxPartnerILN.ItemsSource = SoapRequest.Relationships("DESADV");

        }

        private void menuSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingWebSerice setting = new SettingWebSerice();
            setting.ShowDialog();
            RestartApp();
        }

        private void menuChangeConnection_Click(object sender, RoutedEventArgs e)
        {
            ConnectionDataBase.ChangeConnect();
            RestartApp();
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGridRelationships();
            FillDataGridЖурналРасходныНакладных(App.DefaultDirection);
        }

        /// <summary>
        ///  кнопка обновить DataGrid
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridRelationships();
            FillDataGridЖурналРасходныНакладных(Convert.ToInt32(cbxDirection.SelectedValue));
        }

        /// <summary>
        ///  изменить размер шрифта в dataGrid
        /// </summary>
        private void txtFontSize_LostFocus(object sender, RoutedEventArgs e)
        {
            double newSize;

            if (Double.TryParse(this.txtFontSize.Text, out newSize))
            {
                if (newSize > 5)
                {
                    this.dataGridRelationships.FontSize = newSize;
                    this.dataGridЖурналРасхода.FontSize = newSize;
                }
            }
        }

        private void dataGridRelationships_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RelationshipsFromXml_Result item = dataGridRelationships.SelectedItem as RelationshipsFromXml_Result;
            if (item != null)
            {
                ListInDocument form = new ListInDocument(item.partner_iln, item.document_type_id, item.document_version);
                form.Show();
            }
           
        }

        private void btnЗаполнитьЖурналРасхода_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridЖурналРасходныНакладных(Convert.ToInt32(cbxDirection.SelectedValue));
        }

        private void btnExportDESADV_Click(object sender, RoutedEventArgs e)
        {
            ExportDocument("DESADV");
            FillDataGridЖурналРасходныНакладных(Convert.ToInt32(cbxDirection.SelectedValue));
        }

        private void BtnExportOrderSP_Click(object sender, RoutedEventArgs e)
        {
            ExportDocument("ORDRSP");
            FillDataGridЖурналРасходныНакладных(Convert.ToInt32(cbxDirection.SelectedValue));
        }

        /// <summary> 
        /// заполняет DataGridRelationships данными  заказы
        /// </summary>
        private void FillDataGridRelationships()
        {
            dataGridRelationships.ItemsSource = SoapRequest.Relationships();
        }
        /// <summary>
        ///  заполняет DataGridRelationships данными  уведомление об отгрузке
        /// </summary>
        private void FillDataGridЖурналРасходныНакладных( int Direction)
        {
            DateTime? selectedDateFirst = datePickerFirst.SelectedDate;
            string dateFrom = Convert.ToDateTime(selectedDateFirst.Value.Date.ToShortDateString()).ToString("dd.MM.yyyy");

            DateTime? selectedDateLast = datePickerLast.SelectedDate;
            string dateTo = Convert.ToDateTime(selectedDateLast.Value.Date.ToShortDateString()).ToString("dd.MM.yyyy");

            dataGridЖурналРасхода.ItemsSource = StoredProcedureItems.getЖурналРасходныхНакладных(dateFrom, dateTo, Direction);

        }
        private void cbxPartnerILN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RelationshipsFromXml_Result PartnerILN = cbxPartnerILN.SelectedItem as RelationshipsFromXml_Result;
            txtblockPartnerName.Text = PartnerILN.description;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.DefaultDirection = Convert.ToInt32(cbxDirection.SelectedValue);
            App.Save();
        }

        /// <summary>
        /// Обновляет статус елипса в статус баре
        /// </summary>
        /// <param name="resSoapRequest">код ошибки</param>
        private void SetEllipseStatus(string resSoapRequest)
        {
            if (String.IsNullOrEmpty(SoapRequest.ResultWebRequest(resSoapRequest)))
            {
                EllipseStatus.Fill = Brushes.GreenYellow;
                lblStatus.Content = "WebService : Документ передан успешно";
            }
            else
            {
                EllipseStatus.Fill = Brushes.Red;
                lblStatus.Content = "WebService :" + SoapRequest.ResultWebRequest(resSoapRequest);
            }
        }
        /// <summary>
        /// Формирует документ в формате XMl 
        /// </summary>
        /// <param name="documentType">тип документа</param>
        /// <param name="id">Guid в БД</param>
        /// <returns></returns>
        private string GetDocumentEDI(string documentType, Guid id)
        {
            string doc = "";
            switch (documentType)
            {
                case "DESADV":
                    doc = StoredProcedureItems.getXMLDESADV(id);
                    break;
                case "ORDRSP":
                    doc = StoredProcedureItems.getXMLORDRSP(id);
                    break;
            }
            return doc;
        }
        /// <summary>
        /// Возращает сообщения для MessageBox в зависимости от documentType
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        private string GetMessageDocumentType(string documentType) 
        {
            string res = ""; 
            switch (documentType)
            {
                case "DESADV":
                    res =  "Создать документы об отгрузке в системе EDI ";
                    break;
                case "ORDRSP":
                    res = "Создать документы подтверждения заказа в системе EDI ";
                    break;
            }
            return res;
        }

        /// <summary>
        /// Експортирует выбранные документы из журнала расхода   в систему EDI 
        /// </summary>
        /// <param name="documentType">тип документа</param>
        private void ExportDocument(string documentType)
        {
            // загружаем из DataGrid данные
            List<ComarchEDIЖурналРасходныхНакладных> ЖурналРасхода = (dataGridЖурналРасхода.ItemsSource as IEnumerable<ComarchEDIЖурналРасходныхНакладных>).ToList();
            // проверяем выбран ли партнер EDI
            if (cbxPartnerILN.SelectedValue != null)
            {
                RelationshipsFromXml_Result PartnerILN = cbxPartnerILN.SelectedItem as RelationshipsFromXml_Result;
                if (MessageBox.Show(GetMessageDocumentType(documentType) + PartnerILN.partner_name + " ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var IsexportCount = (from m in ЖурналРасхода where (m.isExport == true) select m).Count();
                    // проверяем пуст ли журнал расхода 
                    if (IsexportCount > 0)
                    {
                        foreach (var row in ЖурналРасхода)
                        {
                            // выбраны ли накладные для експорта 
                            if (row.isExport)
                            {
                                string xmldocument = GetDocumentEDI(documentType, row.ID_Операции);
                                // если нет ошибок и документ не пуст, то отправляем в систему EDI 
                                if (!String.IsNullOrEmpty(xmldocument))
                                {
                                    string res = SoapRequest.Send(cbxPartnerILN.SelectedValue.ToString(), documentType, PartnerILN.document_version, Guid.NewGuid().ToString(), xmldocument, documentTest: "P");
                                    StoredProcedureItems.SetTypeОперации(row.ID_Операции, documentType);
                                    SetEllipseStatus(res);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите документы для експорта !!!", "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Не выбран партнер EDI !!!", "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// перезагружаем приложение
        /// </summary>
        private void RestartApp()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

       
    }
}
