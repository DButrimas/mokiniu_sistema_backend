using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mokiniu_sistema.Models;

namespace mokiniu_sistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RajonaiController : ControllerBase
    {
        private readonly MokiniuContext _context;

        public RajonaiController(MokiniuContext context)
        {
            _context = context;
        }

        // GET: api/Rajonai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rajonas>>> GetRajonai()
        {
            return await _context.Rajonai.FromSqlInterpolated($"SELECT * FROM dbo.Rajonai").ToListAsync();
        }



        // POST: api/Rajonai
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rajonas>> PostRajonas(Rajonas rajonas)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(404, "");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Rajonai (Pavadinimas) VALUES ({rajonas.Pavadinimas})");

                    // _context.Mokyklos.Add(mokykla);
                    await _context.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    // TODO: Handle failure
                    return StatusCode(666, e.Message);
                }
            }
            await _context.SaveChangesAsync();

            return StatusCode(200, "");
        }
    }
}
