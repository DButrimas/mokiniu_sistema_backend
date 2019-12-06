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
    public class MokyklosController : ControllerBase
    {
        private readonly MokiniuContext _context;

        public MokyklosController(MokiniuContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Mokykla>> PostMokykla(Mokykla mokykla)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var cnt = _context.Rajonai.FromSqlInterpolated($"SELECT dbo.Rajonai.rajonasId FROM dbo.Rajonai WHERE dbo.Rajonai.RajonasId = {mokykla.RajonasId}");
                    int count = cnt.Count();

                    if (count == 0)
                    {
                        throw new Exception();
                    }


                    _context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Mokyklos (Pavadinimas, RajonasId) VALUES ({mokykla.Pavadinimas},{mokykla.RajonasId})");

                    // _context.Mokyklos.Add(mokykla);
                    await _context.SaveChangesAsync();


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }
            }

            return StatusCode(200, "");
        }
    }
}
