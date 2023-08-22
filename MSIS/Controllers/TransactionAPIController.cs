using MSIS.Models;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSIS.Controllers
{
    [RoutePrefix("api/transactionAPI")]
    public class TransactionAPIController : ApiController
    {
        private DbModels db = new DbModels();
        [HttpGet]
        [Route("byCurrency")]
        public HttpResponseMessage ByCurrency(string code)
        {
            ReturnVM res = new ReturnVM();
            try
            {
                var data = db.TransactionDMs.Where(w => w.CurrencyCode == code).ToList();
                if (data.Count > 0)
                {
                    res.Message = "Data retrieved successfully";
                    res.Count = data.Count;
                    res.Data = data;
                }
                else
                {
                    res.Message = "No Data record!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, res);
                return response;
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, res);
                return response;
            }

        }
        [HttpGet]
        [Route("byDateRange")]
        public HttpResponseMessage ByDateRange(string fromDate, string toDate)
        {
            ReturnVM res = new ReturnVM();
            try
            {
                DateTime FromDate = Convert.ToDateTime(fromDate);
                DateTime ToDate = Convert.ToDateTime(toDate);
                var data = db.TransactionDMs.AsEnumerable().Where(w => w.TransactionDate.Date.ToString("yyyy-MM-dd").CompareTo(FromDate.ToString("yyyy-MM-dd")) >= 0 && w.TransactionDate.Date.ToString("yyyy-MM-dd").CompareTo(ToDate.ToString("yyyy-MM-dd")) <= 0).ToList();
                if (data.Count > 0)
                {
                    res.Message = "Data retrieved successfully";
                    res.Count = data.Count;
                    res.Data = data;
                }
                else
                {
                    res.Message = "No Data record!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, res);
                return response;
            }
            catch (Exception e)
            {
                res.Message = e.Message;
               var response = Request.CreateResponse(HttpStatusCode.InternalServerError, res);
                return response;
            }
        }
        [HttpGet]
        [Route("byStatus")]
        public HttpResponseMessage ByStatus(string status)
        {
            ReturnVM res = new ReturnVM();
            try
            {
                var data = db.TransactionDMs.Where(w => w.Status == status).ToList();
                if (data.Count > 0)
                {
                    res.Message = "Data retrieved successfully";
                    res.Count = data.Count;
                    res.Data = data;
                }
                else
                {
                    res.Message = "No Data record!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, res);
                return response;
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, res);
                return response;
            }
        }
    }
}