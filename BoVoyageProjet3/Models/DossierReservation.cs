using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class DossierReservation :BaseModel
    {
        [Required(ErrorMessage = "Le champ NumeroCarteBancaire est obligatoire")]
        public string NumeroCarteBancaire { get; set; }
        public decimal PrixTotal { get; set; }
        public int NombreParticipant { get; set; }
        public bool Assurance { get; set; }

        public int ClientID { get; set; }
        public int VoyageID { get; set; }


        [ForeignKey("ClientID")]
        public Client Clients { get; set; }

        [ForeignKey("VoyageID")]
        public Voyage Voyages { get; set; }
    }
}