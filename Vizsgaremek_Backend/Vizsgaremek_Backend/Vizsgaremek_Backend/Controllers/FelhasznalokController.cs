using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FelhasznalokController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public FelhasznalokController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Felhasznalok>> GetFelhasznalok()
        {
            return _context.Felhasznaloks.ToList();
        }

        [HttpGet("{id}"), Authorize("Admin")]
        public ActionResult<Felhasznalok> GetFelhasznalo(Guid id)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            return felhasznalo;
        }

        //[HttpPost]
        //public ActionResult<FelhasznaloDto> CreateFelhasznalo(FelhasznaloDto felhasznalo)
        //{
        //    // ehhez csinálni kell egy DTO-t mert ebben a formában ha megnézed az objektumot akkor olyan dolgokat is
        //    // bekér (pl.: EsemenyHozzaszolasok) amiket nem kell POST-nál létrehozni, csak akkor ha az adatb. lekéri onnan
        //    //(Tehát csináj egy olyan DTO-t ami a LikeSzamlaloig tartalmazza a mezőadatokat, a kötések bekérésének elkerülése érdekében)

        //    _context.Felhasznaloks.Add(felhasznalo);
        //    _context.SaveChanges();

        //    return CreatedAtAction(nameof(GetFelhasznalo), new { id = felhasznalo.FelhasznaloId }, felhasznalo);
        //}

        [HttpPut("{id}"), Authorize("Admin")]
        public IActionResult UpdateFelhasznalo(Guid id, Felhasznalok updatedFelhasznalo)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            felhasznalo.Felhasznalonev = updatedFelhasznalo.Felhasznalonev;
            felhasznalo.Email = updatedFelhasznalo.Email;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult DeleteFelhasznalo(Guid id)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            _context.Felhasznaloks.Remove(felhasznalo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
