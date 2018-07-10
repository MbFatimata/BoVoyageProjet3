using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class Client:Personne
    {
       // [RegularExpression("#^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]{2,}\.[a-z]{2,4}$#")]
        public string Email { get; set; }
    }
}