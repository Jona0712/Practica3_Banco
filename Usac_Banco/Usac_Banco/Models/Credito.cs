using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Usac_Banco.Models
{
    public class Credito
    {
        [Required]
        public float monto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int cuenta { get; set; }

        [Required]
        public int estado { get; set; }
    }
}