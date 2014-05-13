﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using auto_mechanic.BLL;

namespace auto_mechanic.Controllers
{
    public class MechanicController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Default1
        public IEnumerable<Mechanic> GetMechanics()
        {
            var mechanic = db.Mechanic.Include(m => m.Franchise);
            return mechanic.AsEnumerable();
        }

        // GET api/Default1/5
        public Mechanic GetMechanic(int id)
        {
            Mechanic mechanic = db.Mechanic.Find(id);
            if (mechanic == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return mechanic;
        }

        // PUT api/Default1/5
        public HttpResponseMessage PutMechanic(int id, Mechanic mechanic)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != mechanic.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(mechanic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Default1
        public HttpResponseMessage PostMechanic(Mechanic mechanic)
        {
            if (ModelState.IsValid)
            {
                db.Mechanic.Add(mechanic);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mechanic);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = mechanic.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Default1/5
        public HttpResponseMessage DeleteMechanic(int id)
        {
            Mechanic mechanic = db.Mechanic.Find(id);
            if (mechanic == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Mechanic.Remove(mechanic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, mechanic);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}