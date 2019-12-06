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
    public class VaikaiController : ControllerBase
    {
        private readonly MokiniuContext _context;

        public VaikaiController(MokiniuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaikas>>> GetVaikai()
        {
            return await _context.Vaikai.FromSqlInterpolated($"SELECT * FROM dbo.Vaikai").ToListAsync();
            // return await _context.Vaikai.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Vaikas>> PostVaikas(Vaikas vaikas)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var cnt = _context.Rajonai.FromSqlInterpolated($"SELECT DISTINCT(dbo.Mokyklos.MokyklaID) FROM dbo.Mokyklos, dbo.Rajonai INNER JOIN dbo.Mokyklos as mokykla ON mokykla.RajonasId = dbo.Rajonai.RajonasId WHERE dbo.Mokyklos.MokyklaId = {vaikas.MokyklaId}");
                    int count = cnt.Count();

                    if (count == 0)
                    {
                        throw new Exception();
                    }


                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var pridejimoKodas = new String(stringChars);

                    _context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Vaikai (Vardas, MokyklaId,PridejimoKodas) VALUES ({vaikas.Vardas}, {vaikas.MokyklaId},{pridejimoKodas})");
                    //  _context.Vaikai.Add(vaikas);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }

                return StatusCode(200, "");
            }
        }
    }
}
