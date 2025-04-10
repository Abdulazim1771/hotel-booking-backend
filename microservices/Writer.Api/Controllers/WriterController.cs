using Microsoft.AspNetCore.Mvc;
using Writer.Api.Repositories.Interfaces;

namespace Writer.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WriterController : ControllerBase
{
    private readonly IWriterRepository _writerRepository;

    public WriterController(IWriterRepository writerRepository)
    {
        _writerRepository = writerRepository ?? throw new ArgumentNullException(nameof(writerRepository));
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var writers = await _writerRepository.GetAllAsync();

        return Ok(writers);
    }

    [HttpGet("{id:int:min(1)}", Name = nameof(GetById))]
    public async Task<ActionResult<Models.Writer>> GetById([FromRoute] int id)
    {
        var writer = await _writerRepository.GetByIdAsync(id);

        return Ok(writer);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Models.Writer writer)
    {
        var newWriter = await _writerRepository.CreateAsync(writer);

        return CreatedAtAction(nameof(GetById), new { id = writer.Id }, writer);
    }
}
