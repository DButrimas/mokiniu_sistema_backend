using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mokiniu_sistema.Models
{
    public class Vaikas
    {
        public int VaikasId { get; set; }

        [Required]
        public string Vardas { get; set; }
        public string PridejimoKodas { get; set; }

        public int? TevasId { get; set; }
        public Tevas Tevas { get; set; }

        [Required]
        public int? MokyklaId { get; set; }
        public Mokykla Mokykla { get; set; }
    }
}
