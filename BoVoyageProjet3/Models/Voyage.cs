using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class Voyage : BaseModel
    {
        public DateTime DateAller { get; set; }
        public DateTime DateRetour { get; set; }
        public int PlacesDisponibles { get; set; }
        public decimal TarifToutCompris { get; set; }

        public int DestinationID { get; set; }
        public int AgenceID { get; set; }


        [ForeignKey("DestinationID")]
        public Destination Destinations { get; set; }

        [ForeignKey("AgenceID")]
        public AgenceVoyage AgencesVoyage { get; set; }
    }
}