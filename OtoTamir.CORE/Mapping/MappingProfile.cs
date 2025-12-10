using AutoMapper;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.DTOs.MechanicDTOs;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Mapping
{
    public class MappingProfile:Profile

    {
        public MappingProfile()
        {
            CreateMap<CreateClientDTO,Client>().ReverseMap();
            CreateMap<EditClientDTO,Client>().ReverseMap();
            CreateMap<CreateVehicleDTO, Vehicle>().ReverseMap();
            CreateMap<EditVehicleDTO, Vehicle>().ReverseMap();
            CreateMap<EditProfileDTO, Mechanic>().ReverseMap();
            CreateMap<SymptomDTO, Symptom>().ReverseMap();
            CreateMap<EditServiceRecordDTO, ServiceRecord>().ReverseMap();
            CreateMap<ServiceWorkflowLogDTO, RepairComment>().ReverseMap();
            CreateMap<AddBankCardDTO, BankCard>().ReverseMap();
            CreateMap<AddBankDTO, Bank>().ReverseMap();
        }
    }
}
