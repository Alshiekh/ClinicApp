using AutoMapper;
using ClinicApp.Entitties.Models;
using ClinicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicApp.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<DepartmentViewModel, Department>()
                 .ForMember(g => g.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(g => g.ArabicName, map => map.MapFrom(vm => vm.ArabicName))
                .ForMember(g => g.EnglishName, map => map.MapFrom(vm => vm.EnglishName));


            Mapper.CreateMap<ClinicViewModel, Clinic>()
            .ForMember(g => g.Id, map => map.MapFrom(vm => vm.Id))
            .ForMember(g => g.ArabicName, map => map.MapFrom(vm => vm.ArabicName))
            .ForMember(g => g.EnglishName, map => map.MapFrom(vm => vm.EnglishName))
            .ForMember(g => g.Title, map => map.MapFrom(vm => vm.Title))
            .ForMember(g => g.PhoneNumber, map => map.MapFrom(vm => vm.PhoneNumber))
            .ForMember(g => g.Status, map => map.MapFrom(vm => vm.Status))
            .ForMember(g => g.DepartmentId, map => map.MapFrom(vm => vm.DepartmentId));
            //.ForMember(g => g.Department.EnglishName, map => map.MapFrom(vm => vm.DepartmentName))
            //.ForMember(g => g.Status.Name, map => map.MapFrom(vm => vm.StatusName));
        }
    }
}