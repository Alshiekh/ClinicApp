using AutoMapper;
using ClinicApp.Entitties.Models;
using ClinicApp.Services.Services;
using ClinicApp.ViewModels;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClinicApp.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly IClinicService _clinicService;
        private readonly IDepartmentService _departmentService;
        public ClinicsController(IClinicService clinicService , IDepartmentService departmentService)
        {
            _clinicService = clinicService;
            _departmentService = departmentService;
        }
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))]
           IDataTablesRequest requestModel)
        {
            IQueryable<Clinic> query = _clinicService.GetClinics().AsQueryable();
            var totalCount = query.Count();

            // searching and sorting
            query = SearchClinics(requestModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(dep => new
            {
                Id = dep.Id,
                EnglishName = dep.EnglishName,
                ArabicName = dep.ArabicName,
                Title = dep.Title,
                PhoneNumber = dep.PhoneNumber,
                Status = dep.Status,
                Department = dep.Department.EnglishName
            }).ToList();

            return Json(new DataTablesResponse
            (requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        private IQueryable<Clinic> SearchClinics(IDataTablesRequest requestModel,
             IQueryable<Clinic> query)
        {
            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.EnglishName.Contains(value) ||
                                         p.ArabicName.Contains(value)
                                   );
            }

            /***** Advanced Search Ends ******/

            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) +
                (column.SortDirection == Column.OrderDirection.Ascendant ?
                " asc" : " desc");
            }

            query = query.OrderBy(x => x.Id);
            ;

            return query;
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            var model = new ClinicViewModel();
            ViewBag.DepartmentId = new SelectList(_departmentService.GetDepartments(), "Id", "EnglishName").ToList();
            return View("_CreatePartial", model);
        }
        [HttpPost]
        public ActionResult Add(ClinicViewModel clinicVM)
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.GetDepartments(), "Id", "EnglishName").ToList();
            if (!ModelState.IsValid)
                return View("_CreatePartial", clinicVM);
            else
            {
                var clinic = Mapper.Map<ClinicViewModel, Clinic>(clinicVM);
                _clinicService.AddClinic(clinic);
                TempData["Message"] = "Your Clinic has been Saved Successfully ";
                if (Request.IsAjaxRequest())
                {
                    return Content("success");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public ActionResult Edit(int id)
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.GetDepartments(), "Id", "EnglishName").ToList();
            var clinic = _clinicService.GetClinic(id);
            ClinicViewModel clinicViewModel = Mapper.Map<Clinic, ClinicViewModel>(clinic);
            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", clinicViewModel);
            return View(clinicViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ClinicViewModel clinicVM)
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.GetDepartments(), "Id", "EnglishName", clinicVM.DepartmentId).ToList();
            //assetVM.FacilitySitesSelectList = GetFacilitiySitesSelectList(assetVM.FacilitySiteID);
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", clinicVM);
            }
            else
            {
                var clinic = Mapper.Map<ClinicViewModel, Clinic>(clinicVM);
                _clinicService.UpdateClinic(clinic);
                TempData["Message"] = "Your Clinic has been Updated Successfully ";
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }
            ViewBag.DepartmentId = new SelectList(_departmentService.GetDepartments(), "Id", "EnglishName", clinicVM.DepartmentId).ToList();
            return RedirectToAction("Index");

        }
        #endregion

        #region  Delete
        public ActionResult Delete(int id)
        {
            var clinic = _clinicService.GetClinic(id);
          
            ClinicViewModel clinicViewModel = Mapper.Map<Clinic, ClinicViewModel>(clinic);
            var depart = _departmentService.GetDepartment((int)clinicViewModel.DepartmentId);
            if (depart != null ) { clinicViewModel.DepartmentName = depart.EnglishName; }
            
            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", clinicViewModel);
            return View(clinicViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteClinic(int? id)
        {
            var clinic = _clinicService.GetClinic((int)id);
            if (id != null)
            {
                _clinicService.DeleteClinic((int)id);
                TempData["Delete"] = "Deleted Successfully ";
            }
            else
            {
                ModelState.AddModelError("", "Unable to Delete the Clinic");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var clinicVM = Mapper.Map<Clinic, ClinicViewModel>(clinic);
                return View(Request.IsAjaxRequest() ? "_DeletePartial" : "Delete", clinicVM);
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}