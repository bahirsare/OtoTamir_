using AutoMapper;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.DTOs.MechanicDTOs;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;

namespace OtoTamir.CORE.Mapping
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<CreateClientDTO, Client>().ReverseMap();
            CreateMap<EditClientDTO, Client>().ReverseMap();
            CreateMap<CreateVehicleDTO, Vehicle>().ReverseMap();
            CreateMap<EditVehicleDTO, Vehicle>().ReverseMap();
            CreateMap<EditProfileDTO, Mechanic>().ReverseMap();
            CreateMap<SymptomDTO, Symptom>().ReverseMap();
            CreateMap<EditServiceRecordDTO, ServiceRecord>().ReverseMap();
            CreateMap<ServiceWorkflowLogDTO, RepairComment>().ReverseMap();
            CreateMap<AddBankCardDTO, BankCard>().ReverseMap();
            CreateMap<AddBankDTO, Bank>().ReverseMap();
            CreateMap<BankDetailsDTO, Bank>().ReverseMap();
            CreateMap<CardDetailsDTO, BankCard>().ReverseMap();
            CreateMap<ClientStatementDTO, Client>().ReverseMap();
            CreateMap<ServiceRecord, StatementItem>()
             .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CompletedDate ?? src.CreatedDate))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $" {src.Name} - {src.Description} (Plaka: {src.Vehicle.Plate})"))
             .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.ReferenceId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(s => "DEBT"));
            CreateMap<TreasuryTransaction, StatementItem>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $" {src.Description} ({src.PaymentSource})"))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ReferenceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(s => "PAYMENT"));
            CreateMap<AddPosTerminalDTO, PosTerminal>().ReverseMap();
            CreateMap<EditPosTerminalDTO, PosTerminal>().ReverseMap();
            CreateMap<PosTerminal, PosTerminalSummaryDTO>()
    .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Bank.BankName));
            CreateMap<ServiceCompletionDTO, TreasuryTransaction>()
            .ForMember(dest => dest.PaymentSource, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(s => DateTime.Now))
            .ReverseMap();
        }
    }
}
