using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mokiniu_sistema.Models
{
    public class Tevas
    {
        public string Vardas { get; set; }
        public int TevasId { get; set; }
        public ICollection<Vaikas> Vaikai { get; set; }
    }
}
