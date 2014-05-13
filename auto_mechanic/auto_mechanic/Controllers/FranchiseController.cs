using System;
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
using auto_mechanic.Models;

namespace auto_mechanic.Controllers
{
    public class FranchiseController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Franchise
        public HttpResponseMessage GetFranchises()
        {
            return Tools.JsonResponse(db.Franchise.AsEnumerable());
        }

        // GET api/Franchise/5
        public HttpResponseMessage GetFranchise(int id)
        {
            Franchise franchise = db.Franchise.Find(id);
            if (franchise == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Tools.JsonResponse(franchise);
        }

        // PUT api/Franchise/5
        public HttpResponseMessage PutFranchise(int id, Franchise franchise)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != franchise.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(franchise).State = EntityState.Modified;

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

        // POST api/Franchise
        public HttpResponseMessage PostFranchise(Franchise franchise)
        {
            if (ModelState.IsValid)
            {
                db.Franchise.Add(franchise);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, franchise);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = franchise.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Franchise/5
        public HttpResponseMessage DeleteFranchise(int id)
        {
            Franchise franchise = db.Franchise.Find(id);
            if (franchise == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Franchise.Remove(franchise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, franchise);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}