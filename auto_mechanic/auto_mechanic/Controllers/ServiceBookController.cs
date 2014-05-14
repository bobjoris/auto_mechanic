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
    public class ServiceBookController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/ServiceBook
        public HttpResponseMessage GetServiceBooks()
        {
            return Tools.JsonResponse(db.ServiceBook.AsEnumerable());
        }

        // GET api/Default1/5
        public ServiceBook GetServiceBook(int id)
        {
            ServiceBook servicebook = db.ServiceBook.Find(id);
            if (servicebook == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return servicebook;
        }

        // PUT api/Default1/5
        public HttpResponseMessage PutServiceBook(int id, ServiceBook servicebook)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != servicebook.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(servicebook).State = EntityState.Modified;

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
        public HttpResponseMessage PostServiceBook(ServiceBook servicebook)
        {
            if (ModelState.IsValid)
            {
                List<Service> serv = servicebook.Service.ToList();
                servicebook.Service.Clear();
                foreach (var s in serv)
                {
                    Service service = db.Service.Where(x => x.ID == s.ID).FirstOrDefault();
                    servicebook.Service.Add(service);
                }
                db.ServiceBook.Add(servicebook);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, servicebook);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = servicebook.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Default1/5
        public HttpResponseMessage DeleteServiceBook(int id)
        {
            ServiceBook servicebook = db.ServiceBook.Find(id);
            if (servicebook == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            servicebook.Service.Clear();
            db.SaveChanges();
            db.ServiceBook.Remove(servicebook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, servicebook);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}