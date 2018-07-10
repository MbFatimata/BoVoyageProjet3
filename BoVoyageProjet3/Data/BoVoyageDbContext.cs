﻿using BoVoyageProjet3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoVoyageProjet3.Data
{
    public class BoVoyageDbContext : DbContext
    {
        public BoVoyageDbContext(): base("BoVoyage")
        {
        }
        public DbSet<AgenceVoyage> AgencesVoyage { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<DossierReservation> DossiersReservation { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Voyage> Voyages { get; set; }
    }
}