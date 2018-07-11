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
    [RoutePrefix("api/Continents")]

    public class DestinationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Destinations
        /// <summary>
        /// Retourne la liste des destinations
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        public IQueryable<Destination> GetDestinations()
        {
            return db.Destinations;
        }

        // GET: api/Destinations/5
        /// <summary>
        /// Retourne une destination selon son identifiant
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200"> </response> 
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        [Route("{id:int}")]
        public IHttpActionResult GetDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        /// <summary>
        /// Retoune la destination selon le critère choisi
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        // GET: api/destinations/search
        [Route("search")]
        public IQueryable<Destination> GetSearch(string continent = "", string pays = "", string region = "")
        {
            var query = db.Destinations.Where(x => !x.Deleted);
            if (!string.IsNullOrEmpty(continent))
            {
                query = query.Where(x => x.Continent.Contains(continent));
            }
            if (!string.IsNullOrEmpty(pays))
            {
                query = query.Where(x => x.Continent.Contains(pays));
            }
            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(x => x.Continent.Contains(region));
            }
            return query;

        }

        // PUT: api/Destinations/5*
        /// <summary>
        /// Modifier une destination
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutDestination(int id, Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destination.ID)
            {
                return BadRequest();
            }

            db.Entry(destination).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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


        // POST: api/Destinations
        /// <summary>
        /// Ajouter une destination
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]       
        public IHttpActionResult PostDestination(Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = destination.ID }, destination);
        }

        // DELETE: api/Destinations/5
        /// <summary>
        /// Supprimer une destination
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            //db.Destinations.Remove(destination);
            destination.Deleted = true;
            destination.DeletedAt = DateTime.Now;
            db.Entry(destination).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(destination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.ID == id) > 0;
        }
    }
}