using CapitalEDI.Classes;
using CapitalEDI.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace CapitalEDI.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListInDocument.xaml
    /// </summary>
    public partial class ListInDocument : Window
    {
        public string partnerIln { get; set; }
        public string documentType { get; set; }
        public string documentVersion { get; set; } 

        public ListInDocument(string _partnerIln, string _documentType, string _documentVersion)
        {
            partnerIln = _partnerIln;
            documentType = _documentType;
            documentVersion = _documentVersion;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // выводим список входящих документов
            FillDataGridListMB();
            // выделяем первую строку датагрид
            if (dataGridListMB.SelectedIndex == -1 && dataGridListMB.Items.Count > 0)
            { dataGridListMB.SelectedIndex = 0; }
            // выводим выделенный документ 
            FillDataGridandLablesDocument();
            // выводим список направлений
            cbxDirection.ItemsSource = StoredProcedureItems.getDirection();
        }

        /// <summary>
        /// заполняем DataGridListMB  списком входящих документов
        /// </summary>
        private void FillDataGridListMB()
        {
                dataGridListMB.ItemsSource = SoapRequest.DataListMB(partnerIln,documentType,documentVersion);
        }

        /// <summary>
        ///  заполняем  dataGridItemsDocument and lables
        /// </summary>
        private void FillDataGridandLablesDocument() 
        {
            ComarchEDIGetListMBFromXml_Result item = dataGridListMB.SelectedItem as ComarchEDIGetListMBFromXml_Result;
            if (item != null)
            {
                ComarchEDIGetDocOrderHeaderFromXml_Result headerDocument = SoapRequest.HeaderDocument(partnerIln, item.document_type_id, item.tracking_id).First();
                lblВалюта.Content = headerDocument.Валюта;
                lblВремяДоставки.Content = headerDocument.ОжидаемоеВремяДоствки;
                lblДатаДоставки.Content = headerDocument.ОжидаемаяДатаДоставки;
                lblДатаЗаказа.Content = headerDocument.ДатаЗаказа;
                lblНомерЗаказа.Content = headerDocument.НомерЗаказа;
                lblЗаметка.Content = headerDocument.Remarks;
                lblКодПромоАкции.Content = headerDocument.КодПромоАкции;
                ComarchEDIGetDocOrderInfoBuyerFromXml_Result InfoBuyerDocument = SoapRequest.InfoBuyer(partnerIln, item.document_type_id, item.tracking_id).First();
                lblПокупатель.Content = InfoBuyerDocument.GLNПокупателя;
                lblОтправитель.Content = InfoBuyerDocument.GLNОтправителя;
                lblКодПокупателя.Content = InfoBuyerDocument.КодПокупателя;
                lblЗаказСформировал.Content = InfoBuyerDocument.ЗаказаСформировал;
                lblМестоДоставки.Content = InfoBuyerDocument.GLNМестаДоставки;
                lblПолучательНакладной.Content = InfoBuyerDocument.GLNПолучателяНакладной;
                lblПродавец.Content = InfoBuyerDocument.GLNпродавца;
                lblПунктДоставки.Content = InfoBuyerDocument.DeliveryPlace;
                ComarchEDIGetDocOrderSummaryFromXml_Result orderSummaryDocument = SoapRequest.OrderSummary(partnerIln, item.document_type_id, item.tracking_id).First();
                lblобщееКол.Content = orderSummaryDocument.ОбщееКолТовара;
                lblСуммаБезНДС.Content = orderSummaryDocument.СуммаБезНДС;
                lblСуммаСНДС.Content = orderSummaryDocument.СуммаЗПДВ;
                dataGridItemsDocument.ItemsSource = SoapRequest.LineItem(partnerIln, item.document_type_id, item.tracking_id);
            }
        }  

        /// <summary>
        /// Расширенный поиск документов
        /// </summary>
        private void btnSearchDocument_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDateFirst = datePickerFirst.SelectedDate;
            string dateFrom = Convert.ToDateTime(selectedDateFirst.Value.Date.ToShortDateString()).ToString("yyyy-MM-dd");

            DateTime? selectedDateLast = datePickerLast.SelectedDate;
            string dateTo = Convert.ToDateTime(selectedDateLast.Value.Date.ToShortDateString()).ToString("yyyy-MM-dd");

            dataGridListMB.ItemsSource = SoapRequest.DataListMBEx(partnerIln, documentType, documentVersion, dateFrom, dateTo, this.SelectedStatusDoc(cmbDocumentStatus.SelectedIndex));

        }


        private void dataGridListMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDataGridandLablesDocument();
        }

        private string SelectedStatusDoc(int index)
        {
            string status = "A";
            switch (index)
            {
                case 0:  // все документы
                    status = "A";
                    break;
                case 1: // Новые документы
                    status = "N";
                    break;
                case 2: //Прочитанные документы
                    status = "R";
                    break;
                case 3: //Последние документы
                    status = "L";
                    break;
            }
            return status;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string  xmldocument = StoredProcedureItems.getXMLDESADV(Guid.Parse("{A4AD92BA-90AA-4059-A03B-0BCDDC91AD21}"));
            string res = SoapRequest.Send(partnerIln, "DESADV", "ECOD.UA", Guid.NewGuid().ToString(), xmldocument, documentTest: "P");
            if (!String.IsNullOrEmpty(SoapRequest.ResultWebRequest(res)))
            {
                MessageBox.Show(SoapRequest.ResultWebRequest(res), "Подключения к WebService", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(SoapRequest.ResultWebRequest(res), "Документ передан !!!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
