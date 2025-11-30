using WorkTrace.Data.Models;

namespace WorkTrace.Application.Repositories;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<List<Client>> GetClientsByFullNameAsync(string fullName);
    Task<Client> GetByDocumentNumberAsync(string documentNumber);
    Task<Client> GetByEmailAsync(string email);
    Task<Client> GetByPhoneNumberAsync(string phoneNumber);
}