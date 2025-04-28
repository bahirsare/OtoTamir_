using AutoMapper;
using OtoTamir.CORE.DTOs.Client;
using OtoTamir.CORE.DTOs.Vehicle;
using OtoTamir.CORE.Entities;
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
        }
    }
}
