using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test.Model.Classes;
using Test.Model.DAL;

namespace Test.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPeople()
        {
            var data = new List<PersonColours>();
            var error = "";
            try
            {
                 data = Dal.GetPeople();
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return Json(new {err = error, data});
        }

        public JsonResult GetPerson(int personid)
        {
            var error = "";
            var vm = new PersonEditViewModel();

            try
            {
                vm.Person = Dal.GetPersonById(personid);
                vm.Colours = Dal.GetColoursByPerson(personid);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            
            return Json(new { vm, err = error});
        }

        public JsonResult SaveChanges(PersonEditViewModel personcolours)
        {
            var error = "";
            try
            {
                Dal.UpdatePeople(personcolours);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return Json(new { err = error });
        }

    }
}