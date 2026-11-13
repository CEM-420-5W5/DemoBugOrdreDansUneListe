using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoBugOrdeDeListe.Models;

[Route("api/[controller]")]
[ApiController]
public class ParentsController : ControllerBase
{
    private readonly DemoBugOrdeDeListeContext _context;
    public ParentsController(DemoBugOrdeDeListeContext context)
    {
        _context = context;
    }

    // GET: api/Parent
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Parent>>> GetParent()
    {
        return await _context.Parent.ToListAsync();
    }

    // GET: api/Parent/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Parent>> GetParent(int id)
    {
        var parent = await _context.Parent.FindAsync(id);

        if (parent == null)
        {
            return NotFound();
        }

        return parent;
    }

    // PUT: api/Parent/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParent(int? id, Parent parent)
    {
        if (id != parent.Id)
        {
            return BadRequest();
        }

        _context.Entry(parent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Parent
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Parent>> PostParent(Parent parent)
    {
        _context.Parent.Add(parent);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetParent", new { id = parent.Id }, parent);
    }

    // DELETE: api/Parent/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParent(int? id)
    {
        var parent = await _context.Parent.FindAsync(id);
        if (parent == null)
        {
            return NotFound();
        }

        _context.Parent.Remove(parent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ParentExists(int? id)
    {
        return _context.Parent.Any(e => e.Id == id);
    }
}
