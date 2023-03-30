using DrillBlockApp.Data;
using DrillBlockApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolePointsController : ControllerBase
    {
        private readonly DataContext _context;

        public HolePointsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<HolePoints> GetHolePoints()
        {
            return Ok(_context.HolePoints.ToList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HolePoints> GetHolePoint(int id)
        {
            if (id == 0)
                return BadRequest();

            var holePoint = _context.HolePoints.FirstOrDefault(hp => hp.Id == id);

            if (holePoint == null)
                return NotFound();

            return Ok(holePoint);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HolePoints> CreatrHolePoints([FromBody] HolePoints holePointsCreate)
        {
            if (holePointsCreate == null)
                return BadRequest(holePointsCreate);

            if (_context.Holes.FirstOrDefault(h => h.Id == holePointsCreate.HoleId) == null)
                ModelState.AddModelError("", "Скважина не существует");

            HolePoints newHolePoint = new()
            {
                Id = holePointsCreate.Id,
                HoleId = holePointsCreate.HoleId,
                X = holePointsCreate.X,
                Y = holePointsCreate.Y,
                Z = holePointsCreate.Z
            };

            _context.HolePoints.Add(newHolePoint);
            _context.SaveChanges();

            return Ok("Точка скважины добавлена");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHolePoint(int id, [FromBody] HolePoints holePointsUpdate)
        {
            if (holePointsUpdate == null || id != holePointsUpdate.Id)
                return BadRequest();

            HolePoints holePoint = new()
            {
                Id = holePointsUpdate.Id,
                HoleId = holePointsUpdate.HoleId,
                X = holePointsUpdate.X,
                Y = holePointsUpdate.Y,
                Z = holePointsUpdate.Z
            };

            _context.HolePoints.Update(holePoint);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHolePoint(int id)
        {
            if (id == 0)
                return BadRequest();

            var deleteHolePoint = _context.HolePoints.FirstOrDefault(dh => dh.Id == id);

            if (deleteHolePoint == null)
                return NotFound();

            _context.HolePoints.Remove(deleteHolePoint);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
