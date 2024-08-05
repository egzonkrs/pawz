using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IIdentityService
{
    Task RegisterAsync();
    Task LoginAsync();
}

