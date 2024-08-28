using Pawz.Domain.Entities;

namespace Pawz.Domain.Interfaces;

/// <summary>
/// Interface for the repository that manages pet images.
/// </summary>
public interface IPetImageRepository : IGenericRepository<PetImage, int> { }

