using AutoMapper;
using ClinicApp.Entitties.Models;
using ClinicApp.Services.Services;
using ClinicApp.ViewModels;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ClinicApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }
        #region Index
        public ActionResult Index()
        {
            IEnumerable<DepartmentViewModel> viewModelDepartments;
            IEnumerable<Department> departments;
            departments = _departmentService.GetDepartments().ToList();
            viewModelDepartments = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(viewModelDepartments);
        }
        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))]
           IDataTablesRequest requestModel)
        {
            IQueryable<Department> query = _departmentService.GetDepartments().AsQueryable();
            var totalCount = query.Count();

            // searching and sorting
            query = SearchDepartments(requestModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(dep => new
            {
                Id = dep.Id,
                EnglishName = dep.EnglishName,
                ArabicName = dep.ArabicName,
            }).ToList();

            return Json(new DataTablesResponse
            (requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        private IQueryable<Department> SearchDepartments(IDataTablesRequest requestModel,
             IQueryable<Department> query)
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
            var model = new DepartmentViewModel();
            return View("_CreatePartial", model);
        }
        [HttpPost]
        public ActionResult Add(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View("_CreatePartial", departmentVM);
            else
            {
                var department = Mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentService.AddDepartment(department);
                TempData["Message"] = "Your Department has been Saved Successfully ";
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
            var department = _departmentService.GetDepartment(id);

            DepartmentViewModel departmentViewModel = Mapper.Map<Department, DepartmentViewModel>(department);

            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", departmentViewModel);

            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentViewModel departmentVM)
        {
            //assetVM.FacilitySitesSelectList = GetFacilitiySitesSelectList(assetVM.FacilitySiteID);
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", departmentVM);
            }
            else { 
              var department = Mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentService.UpdateDepartment(department);
                TempData["Message"] = "Your Department has been Updated Successfully ";
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");

        }
        #endregion

        #region  Delete
        public ActionResult Delete(int id)
        {
            var department = _departmentService.GetDepartment(id);
            TempData["Delete"] = "Deleted Successfully ";
            DepartmentViewModel departmentViewModel = Mapper.Map<Department, DepartmentViewModel>(department);

            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", departmentViewModel);
            return View(departmentViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteDepartment(int? id)
        {
            var department = _departmentService.GetDepartment((int)id);
            if (id != null)
            {
                _departmentService.DeleteDepartment((int)id);
            }
            else
            {
                ModelState.AddModelError("", "Unable to Delete the Department");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var departmentVM = Mapper.Map<Department, DepartmentViewModel>(department);
                return View(Request.IsAjaxRequest() ? "_DeletePartial" : "Delete", departmentVM);
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