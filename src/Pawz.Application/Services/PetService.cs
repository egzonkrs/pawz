using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreatePetAsync(Pet pet)
        {
            await _petRepository.AddAsync(pet);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int petId)
        {
            return await _petRepository.GetByIdAsync(petId);
        }

        public async Task<bool> UpdatePetAsync(Pet pet)
        {
            await _petRepository.UpdateAsync(pet);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePetAsync(int petId)
        {
            var pet = await _petRepository.GetByIdAsync(petId);
            if (pet is not null)
            {
                await _petRepository.DeleteAsync(pet);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}