﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Models
{
    public class Destination :BaseModel
    {
        public string Continent { get; set; }
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }

    }
}