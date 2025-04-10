
namespace Writer.Api.Repositories.Interfaces;

public interface IWriterRepository
{
    public Task<List<Models.Writer>> GetAllAsync();
    public Task<Models.Writer> GetByIdAsync(int id);
    public Task<Models.Writer> CreateAsync(Models.Writer writer);
}
