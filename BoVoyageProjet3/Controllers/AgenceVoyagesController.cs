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
    [RoutePrefix("api/agenceVoyages")]
    public class AgenceVoyagesController : ApiController
    {        
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/AgenceVoyages
        /// <summary>
        /// Retourne la liste des Agences
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        public IQueryable<AgenceVoyage> GetAgencesVoyage()
        {
            return db.AgencesVoyage;
        }

        // GET: api/AgenceVoyages/5
        /// <summary>
        /// Retourne la l'agence selon son identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        [Route("{id:int}")]
        public IHttpActionResult GetAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyage.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            return Ok(agenceVoyage);
        }

        /// <summary>
        /// Retourne la liste d'agence dont le nom contient le texte entré
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [Route("{nom}")]
        public IQueryable<AgenceVoyage> GetAgenceVoyage(string nom)
        {
            return db.AgencesVoyage.Where(x => x.Nom.Contains(nom));
        }

        /// <summary>
        /// Afficher une agence non supprimée selon son nom 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>

        // GET: api/AgenceVoyagesController/search 
        [Route("search")]
        public IQueryable<AgenceVoyage> GetSearch(string nom = "")
        {
            var query = db.AgencesVoyage.Where(x => !x.Deleted);
            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(x => x.Nom.Contains(nom));
            }
            return query;

        }

        // PUT: api/AgenceVoyages/5
        /// <summary>
        /// Modifie l'agence en fonction de son identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutAgenceVoyage(int id, AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenceVoyage.ID)
            {
                return BadRequest();
            }

            db.Entry(agenceVoyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenceVoyageExists(id))
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

        // POST: api/AgenceVoyages
        /// <summary>
        /// Ajouter une agence
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult PostAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AgencesVoyage.Add(agenceVoyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agenceVoyage.ID }, agenceVoyage);
        }

        // DELETE: api/AgenceVoyages/5
        /// <summary>
        /// Effacer l'agence selon son identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyage.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            //db.AgencesVoyage.Remove(agenceVoyage);
            agenceVoyage.Deleted = true;
            agenceVoyage.DeletedAt = DateTime.Now;
            db.Entry(agenceVoyage).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            db.SaveChanges();

            return Ok(agenceVoyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgenceVoyageExists(int id)
        {
            return db.AgencesVoyage.Count(e => e.ID == id) > 0;
        }
    }
}