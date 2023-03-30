using DrillBlockApp.Data;
using DrillBlockApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrillBlockController : ControllerBase
    {
        private readonly DataContext _context;

        public DrillBlockController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DrillBlock>> GetDrillBlocks()
        {
            return Ok(_context.DrillBlocks.ToList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DrillBlock> GetDrillBlock(int id)
        {
            if (id == 0)
                return BadRequest();

            var drilBlock = _context.DrillBlocks.FirstOrDefault(dB => dB.Id == id);
            if (drilBlock == null)
                return NotFound();

            return Ok(drilBlock);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrillBlock> CreateDrillBlock([FromBody] DrillBlock DrillBlockCreate)
        {
            if (DrillBlockCreate == null)
                return BadRequest(DrillBlockCreate);

            if (_context.DrillBlocks.FirstOrDefault(d => d.Name.ToLower() == DrillBlockCreate.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Блок обуривания существует");
            }

            if (DrillBlockCreate.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            DrillBlock newDrillBlock = new()
            {
                Id = DrillBlockCreate.Id,
                Name = DrillBlockCreate.Name,
                UpdateDate = DateTime.UtcNow
            };

            _context.DrillBlocks.Add(newDrillBlock);
            _context.SaveChanges();

            return Ok("Блок обуривания добавлен");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDrillBlock(int id, [FromBody] DrillBlock drillBlockUpdate)
        {
            if (drillBlockUpdate == null || id != drillBlockUpdate.Id)
                return BadRequest();

            DrillBlock drillBlock = new()
            {
                Id = drillBlockUpdate.Id,
                Name = drillBlockUpdate.Name,
                UpdateDate = DateTime.UtcNow
            };

            _context.DrillBlocks.Update(drillBlock);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDrillBlocl(int id)
        {
            if (id == 0)
                return BadRequest();

            var deleteDrillBlock = _context.DrillBlocks.FirstOrDefault(d => d.Id == id);

            if (deleteDrillBlock == null)
                return NotFound();

            _context.DrillBlocks.Remove(deleteDrillBlock);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
