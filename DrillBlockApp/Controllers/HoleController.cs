using DrillBlockApp.Data;
using DrillBlockApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleController : ControllerBase
    {
        private readonly DataContext _context;

        public HoleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Hole>> GetDrillBlocks()
        {
            return Ok(_context.Holes.ToList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Hole> GetHole(int id)
        {
            if (id == 0)
                return BadRequest();

            var hole = _context.Holes.FirstOrDefault(h => h.Id == id);

            if (hole == null)
                return NotFound();

            return Ok(hole);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Hole> CreateHole([FromBody] Hole holeCreate)
        {
            if (holeCreate == null)
                return BadRequest(holeCreate);

            if (_context.Holes.FirstOrDefault(h => h.Name.ToLower() == holeCreate.Name.ToLower()) != null)
                ModelState.AddModelError("", "Скважина существует");


            Hole newHole = new()
            {
                Id = holeCreate.Id,
                Name = holeCreate.Name,
                Depth = holeCreate.Depth,
                DrillBlockId = holeCreate.DrillBlockId,
            };

            _context.Holes.Add(newHole);
            _context.SaveChanges();

            return Ok("Скважина добавлена");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHole(int id, [FromBody] Hole holeUpdate)
        {
            if (holeUpdate == null || id != holeUpdate.Id)
                return BadRequest();

            Hole hole = new()
            {
                Id = holeUpdate.Id,
                Name = holeUpdate.Name,
                Depth = holeUpdate.Depth,
                DrillBlockId = holeUpdate.DrillBlockId,
            };

            _context.Holes.Update(hole);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHole(int id)
        {
            if (id == 0)
                return BadRequest();

            var deletelHole = _context.Holes.FirstOrDefault(dh => dh.Id == id);

            if (deletelHole == null)
                return NotFound();

            _context.Holes.Remove(deletelHole);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
