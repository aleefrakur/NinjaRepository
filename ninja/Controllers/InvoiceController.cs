using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        // GET: Invoice/Details/5
        public ActionResult Details()
        {
            try
            {
                //Hago un getAll de facturas
                var result = (new ninja.model.Manager.InvoiceManager()).GetAll();

                //Retorno un json, con todas las facturas para exponerlas en la grilla
                return (new JsonResult() { Data = result, ContentType = "application/json" , JsonRequestBehavior = JsonRequestBehavior.AllowGet});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Invoice/Create
        public ActionResult New(ninja.model.Entity.Invoice obj)
        {
            try
            {
                //Inserto una factura
                (new ninja.model.Manager.InvoiceManager()).Insert(obj);

                //Si fué exitoso, retorno en el Data un json con valor "result : ok"
                return new JsonResult { Data = "{ result : ok }" };
            }
            catch (Exception)
            {
                //Sie fué exitoso, retorno en el Data un json con valor "result : fail"
                return new JsonResult { Data = "{ result : fail }" };
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
