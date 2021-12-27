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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Category>> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory.IsNull())
                return new Response<Category>("Categoria não encontrada.");

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new Response<Category>(existingCategory);
            }
            catch (Exception ex)
            {
                return new Response<Category>($"Erro ao remover categoria: {ex.Message}");
            }
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<Response<Category>> SaveAsync(Category entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                await _categoryRepository.AddAsync(entity);
                await _unitOfWork.CompleteAsync();

                return new Response<Category>(entity);
            }
            catch (Exception ex)
            {
                return new Response<Category>($"Erro ao salvar categoria: {ex.Message}");
            }
        }

        public async Task<Response<Category>> UpdateAsync(int id, Category entity)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory.IsNull())
                return new Response<Category>("Categoria não encontrada.");
            
            existingCategory.Name = entity.Name;
            existingCategory.UpdateDate = DateTime.Now;

            try
            {
                _categoryRepository.update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new Response<Category>(existingCategory);
            }
            catch (Exception ex)
            {
                return new Response<Category>($"Erro ao atualizar categoria: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Category>> FindByNameAsync(string name)
        {
            return await _categoryRepository.FindByNameAsync(name);
        }
    }
}