using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class Participant:Personne
    {        
        public double Reduction { get; set; }

        [ForeignKey("DossierReservationID")]
        public DossierReservation DossierReservation { get; set; }
        
    }
}