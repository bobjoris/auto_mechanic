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
    public class ServiceController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Service
        public HttpResponseMessage GetServices()
        {
            return Tools.JsonResponse(db.Service.AsEnumerable());
        }

        // GET api/Service/5
        public Service GetService(int id)
        {
            Service service = db.Service.Find(id);
            if (service == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return service;
        }

        // PUT api/Service/5
        public HttpResponseMessage PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != service.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(service).State = EntityState.Modified;

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

        // POST api/Service
        public HttpResponseMessage PostService(Service service)
        {
            if (ModelState.IsValid)
            {
                // Récupération de la durée
                string[] splitLabel = service.Label.Split('#');

                service.Label = splitLabel[0];
                int duration = Int32.Parse(splitLabel[1]);

                db.Service.Add(service);
                db.SaveChanges();

                foreach (Mechanic mechanic in db.Mechanic)
                {
                    Mechanic_Service mechnanic_service = new Mechanic_Service();
                    mechnanic_service.MechanicID = mechanic.ID;
                    mechnanic_service.ServiceID = service.ID;
                    mechnanic_service.Duration = duration;
                    db.Mechanic_Service.Add(mechnanic_service);
                }

                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, service);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = service.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Service/5
        public HttpResponseMessage DeleteService(int id)
        {
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            List<Mechanic_Service> lms = db.Mechanic_Service.Where(x => x.ServiceID == id).ToList();
            foreach (var ms in lms)
            {
                db.Mechanic_Service.Remove(ms);
            }
            db.Service.Remove(service);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, service);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}