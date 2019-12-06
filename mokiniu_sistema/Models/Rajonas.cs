using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mokiniu_sistema.Models
{
    public class Rajonas
    {
        public int RajonasId { get; set; }

        [Required]
        public string Pavadinimas { get; set; }
        public ICollection<Mokykla> Mokyklos { get; set; }
    }
}
