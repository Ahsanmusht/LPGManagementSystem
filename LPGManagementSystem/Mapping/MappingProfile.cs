using AutoMapper;
using LPGManagementSystem.Models;
using LPGManagementSystem.DTO;

namespace LPGManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PartyType Mappings
            CreateMap<PartyType, PartyTypeDto>();

            // Party Mappings
            CreateMap<Party, PartyDto>()
                .ForMember(dest => dest.PartyTypeName,
                    opt => opt.MapFrom(src => src.PartyType != null ? src.PartyType.PartyTypeName : ""));

            CreateMap<CreatePartyDto, Party>();
            CreateMap<UpdatePartyDto, Party>();

            // Item Mappings
            CreateMap<Item, ItemDto>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();

            // Purchase Mappings
            CreateMap<Purchase, PurchaseDto>()
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.PartyName : ""));

            CreateMap<CreatePurchaseDto, Purchase>();
            CreateMap<UpdatePurchaseDto, Purchase>();

            // Invoice Mappings
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.PartyName : ""))
                .ForMember(dest => dest.CustomerPhone,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Phone : ""))
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.InvoiceItems));

            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<UpdateInvoiceDto, Invoice>();

            // InvoiceItem Mappings
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(dest => dest.ItemName,
                    opt => opt.MapFrom(src => src.Item != null ? src.Item.ItemName : ""));

            CreateMap<CreateInvoiceItemDto, InvoiceItem>();

            // PettyCash Mappings
            CreateMap<PettyCash, PettyCashDto>()
                .ForMember(dest => dest.PartyName,
                    opt => opt.MapFrom(src => src.Party != null ? src.Party.PartyName : ""))
                .ForMember(dest => dest.PartyTypeName,
                    opt => opt.MapFrom(src => src.Party != null && src.Party.PartyType != null 
                        ? src.Party.PartyType.PartyTypeName : ""))
                .ForMember(dest => dest.PaymentTypeName,
                    opt => opt.MapFrom(src => src.PaymentType == 1 ? "IN" : "OUT"));

            CreateMap<CreatePettyCashDto, PettyCash>();
            CreateMap<UpdatePettyCashDto, PettyCash>();

            // Assets Mappings
            CreateMap<Assets, AssetsDto>();
            CreateMap<CreateAssetsDto, Assets>();
            CreateMap<UpdateAssetsDto, Assets>();
        }
    }
}