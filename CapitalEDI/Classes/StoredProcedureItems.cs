using CapitalEDI.Model;
using CapitalEDI.ServiceReference;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace CapitalEDI.Classes
{
    /// <summary>
    /// класс запускает хранимые процедуры  
    /// </summary>
    public static class  StoredProcedureItems
    {
        private static DataBaseContext db = new DataBaseContext();
        private static Properties.Settings App = new Properties.Settings();


        /// <summary>
        /// возвращает список связей в системе EDI
        /// </summary>
        /// <returns></returns>
        public static  List<RelationshipsFromXml_Result> getDataRelationships(string docXML, string documentType) 
        {
            try {
                return db.Database.SqlQuery<RelationshipsFromXml_Result>("exec ComarchEDIGetRelationshipsFromXml @xmlData, @documentType",
                                                                                                                                            new SqlParameter("@xmlData", docXML), 
                                                                                                                                            new SqlParameter("@documentType", documentType)).ToList();
            }
            catch (SqlException ex) {
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// Відображає  документи присланих до скриньки даного користувача в системі EDI
        /// </summary>
        public static List<ComarchEDIGetListMBFromXml_Result> getDataListMB(string docXML)
        {
            try {
                return db.Database.SqlQuery<ComarchEDIGetListMBFromXml_Result>("exec ComarchEDIGetListMBFromXml @xmlData", new SqlParameter("@xmlData", docXML)).ToList();
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<ComarchEDIGetDocOrderHeaderFromXml_Result>getHeaderDocument(string docXML) 
        {
            try{
                return db.Database.SqlQuery<ComarchEDIGetDocOrderHeaderFromXml_Result>("exec ComarchEDIGetDocOrderHeaderFromXml @xmlData", new SqlParameter("@xmlData", docXML)).ToList();
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<ComarchEDIGetDocOrderInfoBuyerFromXml_Result> getInfoBuyer(string docXML)
        {
            try {
                return db.Database.SqlQuery<ComarchEDIGetDocOrderInfoBuyerFromXml_Result>("exec ComarchEDIGetDocOrderInfoBuyerFromXml @xmlData", new SqlParameter("@xmlData", docXML)).ToList();
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<ComarchEDIGetDocOrderSummaryFromXml_Result> getOrderSummary(string docXML)
        {
            try {
                return db.Database.SqlQuery<ComarchEDIGetDocOrderSummaryFromXml_Result>("exec ComarchEDIGetDocOrderSummaryFromXml @xmlData", new SqlParameter("@xmlData", docXML)).ToList();
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// товары заказа 
        /// </summary>
        public static List<ComarchEDIGetDocOrderLineItemFromXml_Result> getLineItem(string docXML)
        {
            try {
                return db.Database.SqlQuery<ComarchEDIGetDocOrderLineItemFromXml_Result>("exec ComarchEDIGetDocOrderLineItemFromXml @xmlData", new SqlParameter("@xmlData", docXML)).ToList();
            }
            catch (SqlException ex) {
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw;
            }
        }

        /// <summary>
        /// список направлений 
        /// </summary>
        public static List<ComarchEDIGetDirection> getDirection()
        {
            try {
                return db.Database.SqlQuery<ComarchEDIGetDirection>("exec ComarchEDIGetDirection").ToList();
            }
            catch (SqlException ex) {
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }


        public static List<ComarchEDIЖурналРасходныхНакладных> getЖурналРасходныхНакладных(  string FirstDate , string LastDate ,int direction )
        {
            try{
                return db.Database.SqlQuery<ComarchEDIЖурналРасходныхНакладных>("exec ComarchEDIЖурналРасходныхНакладных @direction, @FirstDate, @LastDate",
                                                                                                                                                            new SqlParameter("@direction", direction),
                                                                                                                                                            new SqlParameter("@FirstDate", Convert.ToDateTime(FirstDate)),
                                                                                                                                                            new SqlParameter("@LastDate", Convert.ToDateTime(LastDate))).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }

        /// <summary>
        /// Подтверждение заказа  формат XML
        /// </summary>
        public static string getXMLORDRSP( Guid idOperation) 
        {
            try {
                return db.Database.SqlQuery<string>("exec ComarchEDIGetXMLORDRSP @idOperation", new SqlParameter("@idOperation", idOperation)).ToList().First();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Error);
                return String.Empty;
            }
        }

        /// <summary>
        ///  Уведомление об отгрузке формат XML
        /// </summary>
        public static string getXMLDESADV(Guid idOperation)
        {
            try{
                return db.Database.SqlQuery<string>("exec ComarchEDIGetXMLDESADV @idOperation", new SqlParameter("@idOperation", idOperation)).ToList().First();
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Error);
                return String.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOperation"></param>
        /// <param name="documentType"></param>
        public static void SetTypeОперации(Guid idOperation, string documentType)
        {
            try{
                 db.Database.ExecuteSqlCommand("exec ComarchEDISetTypeОперации @idOperation, @documentType", new SqlParameter("@idOperation", idOperation), new SqlParameter("@documentType", documentType));
            }
            catch (SqlException ex){
                MessageBox.Show(ex.Message.ToString(), "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
