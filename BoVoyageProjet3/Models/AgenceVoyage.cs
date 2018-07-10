using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class AgenceVoyage : BaseModel
    {
        [Required(ErrorMessage = "Le champ Nom est obligatoire")]
        public string Nom { get; set; }
    }
}