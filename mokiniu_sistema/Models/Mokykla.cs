using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mokiniu_sistema.Models
{
    public class Mokykla
    {
        public int MokyklaId { get; set; }

        [Required]
        public string Pavadinimas { get; set; }

        public ICollection<Vaikas> Vaikai { get; set; }

        [Required]
        public int? RajonasId { get; set; }
        public Rajonas Rajonas { get; set; }
    }
}
