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
    [RoutePrefix("api/dossierReservations")]
    public class DossierReservationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/DossierReservations
        /// <summary>
        /// Retourne la liste des dossiers de réservation
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200"> </response>
        /// <returns></returns>
        public IQueryable<DossierReservation> GetDossiersReservation()
        {
            return db.DossiersReservation;
        }

        // GET: api/DossierReservations/5
        /// <summary>
        /// Retourne la liste des dossiers de réservation selon l'identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        [Route("{id:int}")]
        public IHttpActionResult GetDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservation.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            return Ok(dossierReservation);
        }


        // GET: api/DossierReservations/search
        /// <summary>
        /// Retourne la liste des dossiers de réservation selon l'identifiant du client ou l'identifiant du voyage
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [Route("search")]
        public IQueryable<DossierReservation> GetSearch(int? clientId = null, int? voyageId = null)
        {
            var query = db.DossiersReservation.Where(x => !x.Deleted);
            if (clientId != null)
            {
                query = query.Where(x => x.ClientID == clientId);
            }
            if (voyageId != null)
            {
                query = query.Where(x => x.VoyageID == voyageId);
            }
            return query;

        }

        // PUT: api/DossierReservations/5
        /// <summary>
        /// Modifier le dossier de reservation associé à l'identifiant choisi
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutDossierReservation(int id, DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossierReservation.ID)
            {
                return BadRequest();
            }

            db.Entry(dossierReservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierReservationExists(id))
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

        // POST: api/DossierReservations
        /// <summary>
        /// Ajouter un dossier de réservation
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult PostDossierReservation(DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DossiersReservation.Add(dossierReservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dossierReservation.ID }, dossierReservation);
        }

        // DELETE: api/DossierReservations/5
        /// <summary>
        /// Effacer un dossier de réservation
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservation.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            //db.DossiersReservation.Remove(dossierReservation);
            dossierReservation.Deleted = true;
            dossierReservation.DeletedAt = DateTime.Now;
            db.Entry(dossierReservation).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(dossierReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DossierReservationExists(int id)
        {
            return db.DossiersReservation.Count(e => e.ID == id) > 0;
        }
    }
}