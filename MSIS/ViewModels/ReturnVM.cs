using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSIS.ViewModels
{
    public class ReturnVM
    {
        public string Message { get; set; }
        public int Count { get; set; }
        public List<TransactionDM> Data { get; set; }
    }
}