using Application.Dtos.Product;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<ProductCreationDto, Product>();
            CreateMap<Product, ProductCreationDto>();

        }
    }
}
