using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {

            IEnumerable<PersonalModel> personalList;
            HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.GetAsync(Shared.Global.Config.Personal).Result;
            personalList = response.Content.ReadAsAsync<IEnumerable<PersonalModel>>().Result;

            return View(personalList);
        }
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return View(new PersonalModel());
            else
            {
                HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.GetAsync(Shared.Global.Config.Personal + "/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<PersonalModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult Edit(PersonalModel personal)
        {
            HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.PutAsJsonAsync(Shared.Global.Config.Personal + "/" + personal.Id, personal).Result;
            TempData["SuccessMessage"] = "Updated Successfully";

            return RedirectToAction("Index");

        }

        public ActionResult Add(int id = 0)
        {
            if (id == 0)
                return View(new PersonalModel());
            else
            {
                HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.GetAsync(Shared.Global.Config.Personal + "/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<PersonalModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult Add(PersonalModel personal)
        {
            if (personal.Id == 0)
            {
                HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.PostAsJsonAsync(Shared.Global.Config.Personal, personal).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = Shared.Global.GlobalVariables.WebApiClient.DeleteAsync(Shared.Global.Config.Personal+"/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

