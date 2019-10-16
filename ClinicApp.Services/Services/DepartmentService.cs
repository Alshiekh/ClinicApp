using ClinicApp.Entitties.Models;
using ClinicApp.Repo.Repositories;
using Store.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            this._departmentRepository = departmentRepository;
            this._unitOfWork = unitOfWork;
        }

        public void AddDepartment(Department department)
        {
            _departmentRepository.Add(department);
            SaveDepartment();
        }

        public Department GetDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments;
        }

        public void SaveDepartment()
        {
            _unitOfWork.Commit();
        }
        public void DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            _departmentRepository.Delete(department);
            SaveDepartment();
        }
        public void UpdateDepartment(Department department)
        {
            _departmentRepository.Update(department);
            SaveDepartment();
        }
    }
    // operations you want to expose
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int id);
        void AddDepartment(Department department);
        void SaveDepartment();

        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);
    }
}
