using CapitalEDI.Model;
using CapitalEDI.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalEDI.Classes
{
    /// <summary>
    ///  методы работы с SOAP WebService
    /// </summary>
    public static class SoapRequest
    {

       private static Properties.Settings App = new Properties.Settings();
        /// <summary>
        ///  Возвращает код ошибки при обращение к WebService , если операция успешна возвращает String.Empty
        /// </summary>
        /// <param name="xml_"></param>
        /// <returns>код ошибки</returns>
        public static string ResultWebRequest(string res) 
        {
            int result = Convert.ToInt32(res);
            string r;
            switch (result)
            {
                case 00000000:
                    r = String.Empty;
                    break;
                case 00000001:
                    r = "Ошибка авторизации, не правильно введен логин или пароль";
                    break;
                case 00000003:
                    r = "Внутренняя ошибка сервиса";
                    break;
                case 00000004:
                    r = "Внутренняя ошибка сервиса";
                    break;
                case 00000005:
                    r = "Ошибка: время вышло";
                    break;
                case 00000006:
                    r = "Ошибка WWW";
                    break;
                default:
                    r = "Внутренняя ошибка сервиса";
                    break;
            }

            return r;
        }

        public static List<RelationshipsFromXml_Result> Relationships(string documentType = "ORDER")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.Relationships(App.LoginWebService, App.PasswordWebService, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getDataRelationships(docXML, documentType);
        }

        public static List<ComarchEDIGetListMBFromXml_Result> DataListMB(string partnerIln, string documentType, string documentVersion, string documentStandard = "XML", string documentTest = "P", string documentStatus = "A")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.ListMB(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, documentVersion, documentStandard, documentTest, documentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getDataListMB(docXML);
        }

        public static List<ComarchEDIGetListMBFromXml_Result> DataListMBEx(string partnerIln, string documentType, string documentVersion,string dateFrom,  string dateTo, string documentStatus = "A", string itemFrom = "1", string itemTo = "300", string documentStandard = "XML", string documentTest = "P")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.ListMBEx(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, documentVersion, documentStandard, documentTest,dateFrom ,dateTo,itemFrom, itemTo, documentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getDataListMB(docXML);
        }

        public static List<ComarchEDIGetDocOrderHeaderFromXml_Result> HeaderDocument(string partnerIln, string documentType, string trackingId, string changeDocumentStatus = "R", string documentStandard = "XML")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.Receive(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, trackingId, documentStandard, changeDocumentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getHeaderDocument(docXML);
        }

        public static List<ComarchEDIGetDocOrderInfoBuyerFromXml_Result> InfoBuyer(string partnerIln, string documentType, string trackingId, string changeDocumentStatus = "R", string documentStandard = "XML")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.Receive(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, trackingId, documentStandard, changeDocumentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getInfoBuyer(docXML);
        }

        public static List<ComarchEDIGetDocOrderSummaryFromXml_Result> OrderSummary(string partnerIln, string documentType, string trackingId, string changeDocumentStatus = "R", string documentStandard = "XML")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.Receive(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, trackingId, documentStandard, changeDocumentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getOrderSummary(docXML);
        }

        public static List<ComarchEDIGetDocOrderLineItemFromXml_Result> LineItem(string partnerIln, string documentType, string trackingId, string changeDocumentStatus = "R", string documentStandard = "XML")
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string docXML = client.Receive(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, trackingId, documentStandard, changeDocumentStatus, 1000).Cnt;
            client.Close();
            return StoredProcedureItems.getLineItem(docXML);
        }

        public static string Send(string partnerIln, string documentType, string documentVersion, string controlNumber, string documentContent ,string documentStandard = "XML", string documentTest = "P" ) 
        {
            EDIWebServiceSoapClient client = new EDIWebServiceSoapClient("EDIWebServiceSoap");
            string res = client.Send(App.LoginWebService, App.PasswordWebService, partnerIln, documentType, documentVersion, documentStandard, documentTest, controlNumber, documentContent, 5000).Res;
            client.Close();
            return res;
        }

    }
}
