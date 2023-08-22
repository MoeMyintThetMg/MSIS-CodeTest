using MSIS.Models;
using MSIS.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MSIS.Controllers
{
    public class HomeController : Controller
    {
        private DbModels db = new DbModels();
        public ActionResult Index()
        {
            var currency = db.TransactionDMs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file)
        {
            try
            {
                var currencyList = GetCurrency();
                string jsonContent;
                List<TransactionDM> dataList = new List<TransactionDM>();
                if (file.ContentType == "text/xml")
                {
                   
                    using (var reader = new StreamReader(file.InputStream))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(reader);
                        jsonContent = JsonConvert.SerializeXmlNode(xmlDoc);
                        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                        var properties = jsonObj["Transactions"]["Transaction"];
                        foreach (var item in properties)
                        {
                            TransactionDM result = new TransactionDM();
                            result.TransactionId = item["@id"];
                            result.TransactionDate = (DateTime)item["TransactionDate"];
                            result.Status = GetStatus(item["Status"].ToString());
                            result.Amount =(decimal)item["PaymentDetails"]["Amount"];
                            result.CurrencyCode = item["PaymentDetails"]["CurrencyCode"];
                            if (!currencyList.Contains(result.CurrencyCode)) 
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Currency!");
                            }
                            dataList.Add(result);
                        }                        
                        db.TransactionDMs.AddRange(dataList);
                        db.SaveChanges();
                    }
                }
                else if (file.ContentType == "application/vnd.ms-excel")
                {
                    using (var reader = new StreamReader(file.InputStream))
                    {
                        string dateFormat = "dd/MM/yyyy HH:mm:ss";
                        List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>();
                        string[] headers = reader.ReadLine().Split(',');
                        while (!reader.EndOfStream)
                        {
                            TransactionDM result = new TransactionDM();
                            string[] fields = reader.ReadLine().Split(',');
                            List<string> record = new List<string>();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                record.Add(fields[i]);
                            }
                            result.TransactionId = record[0];
                            result.Amount =Convert.ToDecimal(record[1]);
                            if (!currencyList.Contains(record[2]))
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Currency!");
                            }
                            result.CurrencyCode = record[2];
                            result.TransactionDate = DateTime.ParseExact(record[3], dateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            result.Status = GetStatus(record[4]);
                            dataList.Add(result);
                        }
                    }
                    db.TransactionDMs.AddRange(dataList);
                    db.SaveChanges();
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Success!");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unknown format");
            }
        }
        public List<string> GetCurrency()
        {
            List<string> currencyList = new List<string>();
            string path = Server.MapPath("~/Content/CurrencyCode.txt");
            var myJsonString = System.IO.File.ReadAllText(path);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(myJsonString);
            foreach (var item in jsonObj["CurrencyCode"])
            {
                string currency = item["Currency Code"];
                currencyList.Add(currency);
            }
            return currencyList;
        }
        public string GetStatus(string code)
        {
            string res = string.Empty;
            if (code == "Approved" || code == "Approved")
            {
                res = "A";
            }
            else if (code == "Failed" || code == "Rejected")
            {
                res = "R";
            }
            else if (code == "Finished" || code == "Done")
            {
                res = "D";
            }
            return res;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}