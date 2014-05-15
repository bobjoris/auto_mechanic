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
    public class MechanicController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Default1
        public HttpResponseMessage GetMechanics()
        {
            var mechanic = db.Mechanic.Include(m => m.Franchise).Include(m => m.Mechanic_Service);
            return Tools.JsonResponse(mechanic.AsEnumerable());
        }

        // GET api/Default1/5
        public HttpResponseMessage GetMechanic(int id)
        {
            Mechanic mechanic = db.Mechanic.Find(id);
            if (mechanic == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Tools.JsonResponse(mechanic);
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
                db.Entry(mechanic.Franchise).State = EntityState.Unchanged;
                db.SaveChanges();

                foreach (Service service in db.Service)
                {
                    Mechanic_Service mechnanic_service = new Mechanic_Service();
                    mechnanic_service.MechanicID = mechanic.ID;
                    mechnanic_service.ServiceID = service.ID;
                    mechnanic_service.Duration = findServiceDuration(service);
                    db.Mechanic_Service.Add(mechnanic_service);
                }

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
            List<Mechanic_Service> mcList = db.Mechanic_Service.Where(x => x.MechanicID == mechanic.ID).ToList();
            foreach (Mechanic_Service mc in mcList)
            {
                db.Mechanic_Service.Remove(mc);
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

        private int findServiceDuration(Service service)
        {
            Mechanic_Service ms = db.Mechanic_Service.Where(x => x.ServiceID == service.ID).FirstOrDefault();

            return (ms == null) ? 1 : ms.Duration;
        }
    }
}