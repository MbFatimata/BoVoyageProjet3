﻿using System;
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
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Clients
        /// <summary>
        /// Retourne la liste des Clients
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200"> </response>
        /// <returns></returns>
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/5
        /// <summary>
        /// Retourne le client selon son identifiant
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        [Route("{id:int}")]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        /// <summary>
        /// Retourne le client selon son nom
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        [Route("{nom}")]
        public IQueryable<Client> GetClient(string nom)
        {
            return db.Clients.Where(x => x.Nom.Contains(nom));
        }

        // GET: api/clients/search
        /// <summary>
        /// Retourne le client non supprimé selon le critère choisi
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        [Route("search")]
        public IQueryable<Client> GetSearch(string email = "", string civilite = "", string nom = "", string prenom = "", string adresse = "", string telephone = "" , DateTime? dateNaissance = null)
        {
            var query = db.Clients.Where(x => !x.Deleted);
            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(x => x.Nom.Contains(nom));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.Contains(email));
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
            return query;

        }

        // PUT: api/Clients/5
        /// <summary>
        /// Modifier les informations d'un client à l'aide de son identifiants
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [Route("{id:int}")]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        /// <summary>
        /// Ajouter un client
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Supprimer un client
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            //db.Clients.Remove(client);
            client.Deleted = true;
            client.DeletedAt = DateTime.Now;
            db.Entry(client).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ID == id) > 0;
        }
    }
}