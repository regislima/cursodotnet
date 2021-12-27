using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Util.Comunication;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Domain.Services;
using api.Util.Extensions;

namespace api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Product>> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct.IsNull())
                return new Response<Product>("Produto não encontrada.");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                return new Response<Product>($"Erro ao remover produto: {ex.Message}");
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> FindByNameAsync(string name)
        {
            return await _productRepository.FindByNameAsync(name);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<Response<Product>> SaveAsync(Product entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                await _productRepository.AddAsync(entity);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(entity);
            }
            catch (Exception ex)
            {
                return new Response<Product>($"Erro ao salvar produto: {ex.Message}");
            }
        }

        public async Task<Response<Product>> UpdateAsync(int id, Product entity)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct.IsNull())
                return new Response<Product>("Produto não encontrada.");
            
            existingProduct.Name = entity.Name;
            existingProduct.UpdateDate = DateTime.Now;

            try
            {
                _productRepository.update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                return new Response<Product>($"Erro ao atualizar Produto: {ex.Message}");
            }
        }
    }
}