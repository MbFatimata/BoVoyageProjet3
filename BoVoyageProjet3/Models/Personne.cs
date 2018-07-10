using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public abstract class Personne:BaseModel
    {
        [Required(ErrorMessage = "Le champ Civilite est obligatoire")]
        public string Civilite { get; set; }
        [Required(ErrorMessage = "Le champ Nom est obligatoire")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le champ Prenom est obligatoire")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Le champ Adresse est obligatoire")]
        public string Adresse { get; set; }
        [Required(ErrorMessage = "Le champ Telephone est obligatoire")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Le champ DateNaissance est obligatoire")]
        public DateTime DateNaissance { get; set; }
        public int Age { get; set; }
        
    }
}