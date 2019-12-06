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
    public class TevaiController : ControllerBase
    {
        private readonly MokiniuContext _context;

        public TevaiController(MokiniuContext context)
        {
            _context = context;
        }

        [HttpGet("tevaibyrajonas/{rajonasId}")]
        public async Task<ActionResult<IEnumerable<Tevas>>> GetTevaiByRajonas(int rajonasId)
        {

            return await _context.Tevai.FromSqlInterpolated($"SELECT DISTINCT dbo.Tevai.TevasId,dbo.Tevai.Vardas FROM (((dbo.Vaikai RIGHT JOIN dbo.Mokyklos ON dbo.Vaikai.MokyklaId = dbo.Mokyklos.MokyklaId) INNER JOIN dbo.Tevai ON dbo.Vaikai.TevasId = dbo.Tevai.TevasId) INNER JOIN dbo.Rajonai ON dbo.Mokyklos.RajonasId = dbo.Rajonai.RajonasId) WHERE dbo.Rajonai.RajonasId = {rajonasId}").ToListAsync();

        }

        [HttpPost("pridejimokodas/{pridejimoKodas}")]
        public async Task<ActionResult<Tevas>> PostTevas(Tevas tevas, string pridejimoKodas)
        {
            if (!ModelState.IsValid || pridejimoKodas == "")
            {
                return StatusCode(400, "");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Tevai.Add(tevas);
                    //  int a = _context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Tevai Default VALUES SELECT TOP 1 ('dbo.TevasId') FROM dbo.Tevai ");
                    _context.SaveChanges();


                    var cnt = _context.Vaikai.FromSqlInterpolated($"SELECT dbo.Vaikai.VaikasId FROM dbo.Vaikai INNER JOIN dbo.Mokyklos as mokykla ON mokykla.MokyklaId = Vaikai.MokyklaId INNER JOIN dbo.Rajonai AS rajonas ON mokykla.RajonasId = rajonas.RajonasId  WHERE dbo.Vaikai.PridejimoKodas = {pridejimoKodas}");
                    int count = cnt.Count();

                    if (count == 0)
                    {
                        throw new Exception();
                    }

                    _context.Database.ExecuteSqlInterpolated($"UPDATE dbo.Vaikai SET dbo.Vaikai.TevasId = {tevas.TevasId} WHERE dbo.Vaikai.PridejimoKodas = {pridejimoKodas}");
                    _context.SaveChanges();


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
