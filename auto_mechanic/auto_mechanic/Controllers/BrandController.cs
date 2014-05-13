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
    public class BrandController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Brand
        public HttpResponseMessage GetBrands()
        {
            return Tools.JsonResponse(db.Brand.AsEnumerable());
        }

        // GET api/Brand/5
        public HttpResponseMessage GetBrand(int id)
        {
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Tools.JsonResponse(brand);
        }

        // PUT api/Brand/5
        public HttpResponseMessage PutBrand(int id, Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != brand.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(brand).State = EntityState.Modified;

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

        // POST api/Brand
        public HttpResponseMessage PostBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brand.Add(brand);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, brand);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = brand.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Brand/5
        public HttpResponseMessage DeleteBrand(int id)
        {
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Brand.Remove(brand);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, brand);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}