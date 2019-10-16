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
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClinicService(IClinicRepository clinicRepository, IUnitOfWork unitOfWork)
        {
            this._clinicRepository = clinicRepository;
            this._unitOfWork = unitOfWork;
        }

        public void AddClinic(Clinic clinic)
        {
            _clinicRepository.Add(clinic);
            SaveClinic();
        }

        public Clinic GetClinic(int id)
        {
            var Clinic = _clinicRepository.GetById(id);
            return Clinic;
        }

        public IEnumerable<Clinic> GetClinics()
        {
            var Clinics = _clinicRepository.GetAllClinic();
            return Clinics;
        }

        public void SaveClinic()
        {
            _unitOfWork.Commit();
        }

        public void UpdateClinic(Clinic clinic)
        {
            _clinicRepository.Update(clinic);
            SaveClinic();
        }
        public void DeleteClinic(int id)
        {
            var clinic = _clinicRepository.GetById(id);
            _clinicRepository.Delete(clinic);
            SaveClinic();
        }
    }
    // operations you want to expose
    public interface IClinicService
    {
        IEnumerable<Clinic> GetClinics();
        Clinic GetClinic(int id);
        void AddClinic(Clinic clinic);
        void SaveClinic();
        void UpdateClinic(Clinic clinic);
        void DeleteClinic(int id);
    }

}
