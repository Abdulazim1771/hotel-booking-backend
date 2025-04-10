using Writer.Api.Repositories.Interfaces;

namespace Writer.Api.Repositories;

internal sealed class WriterRepository : IWriterRepository
{
    private readonly static List<Models.Writer> _writers = Populate();
    private static int _nextId = _writers.Max(w => w.Id) + 1;

    private static List<Models.Writer> Populate()
    {
        return new List<Models.Writer>
        {
            new Models.Writer
            {
                Id = 1,
                Name = "John Doe",
            },
            new Models.Writer
            {
                Id = 2,
                Name = "Mark Tven",
            },
            new Models.Writer
            {
                Id = 3,
                Name = "Nicola Anderson",
            },
        };
    }

    public Task<List<Models.Writer>> GetAllAsync()
    {
        return Task.FromResult(_writers.ToList());
    }

    public Task<Models.Writer> GetByIdAsync(int id)
    {
        var writer = _writers.FirstOrDefault(w => w.Id == id);
        return Task.FromResult(writer)!;
    }

    public Task<Models.Writer> CreateAsync(Models.Writer writer)
    {
        writer.Id = _nextId++;
        _writers.Add(writer);

        return Task.FromResult(writer);
    }
}
