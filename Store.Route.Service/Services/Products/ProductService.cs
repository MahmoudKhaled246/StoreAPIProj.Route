using AutoMapper;
using Store.Route.Core;
using Store.Route.Core.Dtos.Products;
using Store.Route.Core.Entities;
using Store.Route.Core.Services.Contract;
using Store.Route.Core.Specifications;
using Store.Route.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Route.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var spec = new ProductSpecifications();
            return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec));
        }


        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);

            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetByIdWithSpecAsync(spec));
             
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IEnumerable<TypeBrandDto>>(types);
            return mappedTypes;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        }

    }
}
