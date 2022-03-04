using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace BillApplication.Controllers
{
    public class BillController : Controller
    {
        BillDBEntities dbContext = new BillDBEntities();
        public ActionResult Index()
        {
            return View(dbContext.ContactBill.OrderByDescending(o=>o.ID).ToList());
        }
       
    }
}