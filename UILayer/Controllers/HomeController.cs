using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BAL.Models;
//using BAL.Repository;
using System.Xml.Linq;

namespace UILayer.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmplyoeeRecord()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        // GET: Home
        //CrudRepo repo = null;
        //public HomeController()
        //{
        //    repo = new CrudRepo();
        //}
        //[HttpGet]
        //public JsonResult Listemp()
        //{
        //    List<EmployeeDetail> emp = new List<EmployeeDetail>();
        //    emp = repo.listemp();
        //    var jsonResult = Json(emp, JsonRequestBehavior.AllowGet);
        //    return jsonResult;
        //}

        //[HttpGet]
        //public JsonResult ListIndex()
        //{
        //    List<Object> indlist = new List<Object>();
        //    indlist = repo.listindex();
        //    var jsonResult = Json(indlist, JsonRequestBehavior.AllowGet);
        //    return jsonResult;
        //}
        //[HttpPost]
        //public ContentResult AddRecord(Object obj)
        //{
        //    EmployeeDetail emp = new EmployeeDetail();
        //    int i = repo.addrecord(emp);
        //    return Content("Succesfull");
        //}
    }
}