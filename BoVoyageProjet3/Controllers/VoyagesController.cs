using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BoVoyageProjet3.Data;
using BoVoyageProjet3.Models;

namespace BoVoyageProjet3.Controllers
{
    [RoutePrefix("api/voyages")]

    public class VoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Voyages
        /// <summary>
        /// Afficher les voyages
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200"> </response>
        /// <returns></returns>
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages;
        }

        // GET: api/Voyages/5
        /// <summary>
        /// Afficher un voyage selon son ID
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        [Route("{id:int}")]
        public IHttpActionResult GetVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            return Ok(voyage);
        }

        // GET: api/Voyages/search
        /// <summary>
        /// Rechercher un voyage selon sa date d'aller, sa date de retour ou sa destination
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [Route("search")]
        public IQueryable<Voyage> GetSearch(int? destinationId = null, DateTime? dateAller = null, DateTime? dateRetour = null)
        {
            var query = db.Voyages.Where(x => !x.Deleted);
            if (destinationId != null)
            {
                query = query.Where(x => x.DestinationID == destinationId);
            }
            if (dateAller != null)
            {
                query = query.Where(x => x.DateAller == dateAller);
            }
            if (dateRetour != null)
            {
                query = query.Where(x => x.DateRetour == dateRetour);
            }
            return query;

        }

        // PUT: api/Voyages/5
        /// <summary>
        /// Modifier un voyage
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutVoyage(int id, Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyage.ID)
            {
                return BadRequest();
            }

            db.Entry(voyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Voyages
        /// <summary>
        /// Ajouter un voyage
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult PostVoyage(Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voyages.Add(voyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voyage.ID }, voyage);
        }

        // DELETE: api/Voyages/5
        /// <summary>
        /// Supprimer un voyage
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            //db.Voyages.Remove(voyage);
            voyage.Deleted = true;
            voyage.DeletedAt = DateTime.Now;
            db.Entry(voyage).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(voyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyageExists(int id)
        {
            return db.Voyages.Count(e => e.ID == id) > 0;
        }
    }
}