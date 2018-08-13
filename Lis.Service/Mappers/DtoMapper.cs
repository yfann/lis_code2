using AutoMapper;
using Lis.Domain;
using Lis.Domain.Entities;
using Lis.Service.Dtos;

namespace Lis.Service.Mappers
{
    public class DtoMapper : Profile
    {
        public override string ProfileName
        {
            get { return GetType().Name; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Entity, MutableDto>()
                .Include<Crisis, CrisisDto>()
                .Include<Department, DepartmentDto>()
                .Include<Employee, EmployeeDto>()
                .Include<LabCategory, LabCategoryDto>()
                .Include<LabInfo, LabInfoDto>()
                .Include<LabItem, LabItemDto>()
                .Include<LabItemSet, LabItemSetDto>()
                .Include<LabResult, LabResultDto>()
                .Include<LabSample, LabSampleDto>()
                .Include<Logistics, LogisticsDto>()
                .Include<MedicalInstitution, MedicalInstitutionDto>()
                .Include<QcValue, QcValueDto>()
                .Include<Requests, RequestDto>()
                .Include<Role, RoleDto>()
                .Include<Report, ReportDto>()
                .Include<Patient, PatientDto>()
                .Include<SampleType, SampleTypeDto>();

            Mapper.CreateMap<MutableDto, Entity>()
                .Include<CrisisDto, Crisis>()
                .Include<DepartmentDto, Department>()
                .Include<EmployeeDto, Employee>()
                .Include<LabCategoryDto, LabCategory>()
                .Include<LabInfoDto, LabInfo>()
                .Include<LabItemDto, LabItem>()
                .Include<LabItemSetDto, LabItemSet>()
                .Include<LabResultDto, LabResult>()
                .Include<LabSampleDto, LabSample>()
                .Include<LogisticsDto, Logistics>()
                .Include<MedicalInstitutionDto, MedicalInstitution>()
                .Include<QcValueDto, QcValue>()
                .Include<RequestDto, Requests>()
                .Include<RoleDto, Role>()
                .Include<ReportDto, Report>()
                .Include<PatientDto, Patient>()
                .Include<SampleTypeDto, SampleType>();


            Mapper.CreateMap<Crisis, CrisisDto>();
            Mapper.CreateMap<CrisisDto, Crisis>();
            Mapper.CreateMap<Department, DepartmentDto>();
            Mapper.CreateMap<DepartmentDto, Department>();
            Mapper.CreateMap<Employee, EmployeeDto>();
            Mapper.CreateMap<EmployeeDto, Employee>();
            Mapper.CreateMap<LabCategory, LabCategoryDto>();
            Mapper.CreateMap<LabCategoryDto, LabCategory>();
            Mapper.CreateMap<LabInfo, LabInfoDto>();
            Mapper.CreateMap<LabInfoDto, LabInfo>();
            Mapper.CreateMap<LabItem, LabItemDto>();
            Mapper.CreateMap<LabItemDto, LabItem>();
            Mapper.CreateMap<LabItemSet, LabItemSetDto>();
            Mapper.CreateMap<LabItemSetDto, LabItemSet>();
            Mapper.CreateMap<LabItemSetDetail, LabItemSetDetailDto>();
            Mapper.CreateMap<LabItemSetDetailDto, LabItemSetDetail>();
            Mapper.CreateMap<LabResult, LabResultDto>();
            Mapper.CreateMap<LabResultDto, LabResult>();
            Mapper.CreateMap<LabSample, LabSampleDto>();
            Mapper.CreateMap<LabSampleDto, LabSample>();
            Mapper.CreateMap<Logistics, LogisticsDto>();
            Mapper.CreateMap<LogisticsDto, Logistics>();
            Mapper.CreateMap<MedicalInstitution, MedicalInstitutionDto>();
            Mapper.CreateMap<MedicalInstitutionDto, MedicalInstitution>();
            Mapper.CreateMap<QcValue, QcValueDto>();
            Mapper.CreateMap<QcValueDto, QcValue>();
            Mapper.CreateMap<Requests, RequestDto>();
            Mapper.CreateMap<RequestDto, Requests>();
            Mapper.CreateMap<Role, RoleDto>();
            Mapper.CreateMap<RoleDto, Role>();
            Mapper.CreateMap<SampleType, SampleTypeDto>();
            Mapper.CreateMap<SampleTypeDto, SampleType>();
            Mapper.CreateMap<Patient, PatientDto>();
            Mapper.CreateMap<PatientDto, Patient>();
            Mapper.CreateMap<Report, ReportDto>();
            Mapper.CreateMap<ReportDto, Report>();
            Mapper.CreateMap<ReportCrisis, ReportCrisisDto>();
            Mapper.CreateMap<ReportCrisisDto, ReportCrisis>();
            Mapper.CreateMap<EmployeeMi, EmployeeMiDto>();
            Mapper.CreateMap<EmployeeMiDto, EmployeeMi>();
        }
    }
}
