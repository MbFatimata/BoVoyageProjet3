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
    [RoutePrefix("api/participants")]
    public class ParticipantsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Participants
        /// <summary>
        /// Retourne la liste des participants
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        public IQueryable<Participant> GetParticipants()
        {
            return db.Participants;
        }

        // GET: api/Participants/5
        /// <summary>
        /// Retourne un participant selon son identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        [Route("{id:int}")]
        public IHttpActionResult GetParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // GET: api/Participants/search
        /// <summary>
        /// Rechercher un participant par ses infos personnels(nom, prenom,...) sa reduction ou son dossier de réservation
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200"> </response>
        /// <returns></returns>
        [Route("api/Participants/search")]
        public IQueryable<Participant> GetSearch(string civilite = "", string nom = "", string prenom = "", string adresse = "", string telephone = "", DateTime? dateNaissance = null, double? reduction = null, int? dossierReservationID = null)
        {
            var query = db.Participants.Where(x => !x.Deleted);
            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(x => x.Nom.Contains(nom));
            }
            if (!string.IsNullOrEmpty(civilite))
            {
                query = query.Where(x => x.Civilite.Contains(civilite));
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                query = query.Where(x => x.Prenom.Contains(prenom));
            }
            if (!string.IsNullOrEmpty(adresse))
            {
                query = query.Where(x => x.Adresse.Contains(adresse));
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                query = query.Where(x => x.Telephone.Contains(telephone));
            }

            if (dateNaissance != null)
            {
                query = query.Where(x => x.DateNaissance == dateNaissance);
            }
            if (reduction != null)
            {
                query = query.Where(x => x.Reduction == reduction);
            }
            if (dossierReservationID != null)
            {
                query = query.Where(x => x.DossierReservationID == dossierReservationID);
            }
            return query;
        }
        
        // PUT: api/Participants/5
        /// <summary>
        /// Modifier un participant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participants
        /// <summary>
        /// Ajouter un participant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participants.Add(participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participant.ID }, participant);
        }

        // DELETE: api/Participants/5
        /// <summary>
        /// Supprimer un participant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            //db.Participants.Remove(participant);
            participant.Deleted = true;
            participant.DeletedAt = DateTime.Now;
            db.Entry(participant).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(int id)
        {
            return db.Participants.Count(e => e.ID == id) > 0;
        }
    }
}