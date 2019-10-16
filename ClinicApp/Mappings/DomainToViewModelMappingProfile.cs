using AutoMapper;
using ClinicApp.Entitties.Models;
using ClinicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicApp.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Department, DepartmentViewModel>();
            Mapper.CreateMap<Clinic, ClinicViewModel>();
        }
    }
}