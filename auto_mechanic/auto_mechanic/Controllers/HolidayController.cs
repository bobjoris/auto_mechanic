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
    public class HolidayController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Holiday
        public HttpResponseMessage GetHolidays()
        {
            var holiday = db.Holiday.Include("Mechanic");
            return Tools.JsonResponse(holiday.AsEnumerable());
        }

        // GET api/Holiday/5
        public HttpResponseMessage GetHoliday(int id)
        {
            Holiday holiday = db.Holiday.Find(id);
            if (holiday == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Tools.JsonResponse(holiday);
        }

        // PUT api/Holiday/5
        public HttpResponseMessage PutHoliday(int id, Holiday holiday)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != holiday.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(holiday).State = EntityState.Modified;

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

        // POST api/Holiday
        public HttpResponseMessage PostHoliday(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.Holiday.Add(holiday);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, holiday);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = holiday.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Holiday/5
        public HttpResponseMessage DeleteHoliday(int id)
        {
            Holiday holiday = db.Holiday.Find(id);
            if (holiday == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Holiday.Remove(holiday);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, holiday);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}