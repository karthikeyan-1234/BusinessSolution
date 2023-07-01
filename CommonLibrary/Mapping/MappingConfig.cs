using AutoMapper;

using CommonLibrary.Models;
using CommonLibrary.Models.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PurchaseDTO, Purchase>();
            CreateMap<Purchase, PurchaseDTO>();

            CreateMap<PurchaseDetail, PurchaseDetailDTO>();
            CreateMap<PurchaseDetailDTO, PurchaseDetail>();
        }
    }
}
