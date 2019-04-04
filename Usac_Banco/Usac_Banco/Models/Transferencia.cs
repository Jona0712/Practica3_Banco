using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Usac_Banco.Models
{
    public class Transferencia
    {
        [Required]
        public int cuenta1 { get; set; }

        [Required]
        public int cuenta2 { get; set; }

        [Required]
        public float monto { get; set; }
    }
}