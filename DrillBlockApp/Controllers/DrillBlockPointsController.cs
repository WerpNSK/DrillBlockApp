using DrillBlockApp.Data;
using DrillBlockApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockApp.Controllers
{
    [Route("api/[controller]")]
    public class DrillBlockPointsController : ControllerBase
    {
        private readonly DataContext _context;

        public DrillBlockPointsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DrillBlockPoints> GetDrillBlocksPoint()
        {
            return Ok(_context.DrillBlockPoints.ToList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DrillBlockPoints> GetDrillBlockPoint(int id)
        {
            if (id == 0)
                return BadRequest();

            var drillBlockPoint = _context.DrillBlockPoints.FirstOrDefault(d => d.Id == id);

            if (drillBlockPoint == null)
                return NotFound();

            return Ok(drillBlockPoint);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrillBlockPoints> CreateDrillBlockPoint([FromBody] DrillBlockPoints drillBlockPointCreate)
        {
            if (drillBlockPointCreate == null)
                return BadRequest(drillBlockPointCreate);

            if (_context.DrillBlocks.FirstOrDefault(d => d.Id == drillBlockPointCreate.DrillBlockId) == null)
                ModelState.AddModelError("", "Блок обуривания не существует");

            DrillBlockPoints newDrillBlockPoint = new()
            {
                Id = drillBlockPointCreate.Id,
                DrillBlockId = drillBlockPointCreate.DrillBlockId,
                Sequence = drillBlockPointCreate.Sequence,
                X = drillBlockPointCreate.X,
                Y = drillBlockPointCreate.Y,
                Z = drillBlockPointCreate.Z
            };

            _context.DrillBlockPoints.Add(newDrillBlockPoint);
            _context.SaveChanges();

            return Ok("Точка блока добавлена");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDrillBlockPoint(int id, [FromBody] DrillBlockPoints drillBlockPointsUpdate)
        {
            if (drillBlockPointsUpdate == null || id != drillBlockPointsUpdate.Id)
                return NotFound();

            DrillBlockPoints drillBlockPoint = new()
            {
                Id = drillBlockPointsUpdate.Id,
                DrillBlockId = drillBlockPointsUpdate.Id,
                Sequence = drillBlockPointsUpdate.Sequence,
                X = drillBlockPointsUpdate.X,
                Y = drillBlockPointsUpdate.Y,
                Z = drillBlockPointsUpdate.Z
            };

            _context.DrillBlockPoints.Update(drillBlockPointsUpdate);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDrillBlockPoint(int id)
        {
            if (id == 0)
                return BadRequest();

            var deleteDrillBlockPoint = _context.DrillBlockPoints.FirstOrDefault(dd => dd.Id == id);

            if (deleteDrillBlockPoint == null)
                return NotFound();

            _context.DrillBlockPoints.Remove(deleteDrillBlockPoint);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
