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
using Newtonsoft.Json;
using auto_mechanic.Models;

namespace auto_mechanic.Controllers
{
    public class CarController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/Car
        public HttpResponseMessage GetCars()
        {
            var car = db.Car.ToList();

            return Tools.JsonResponse(car);
        }

        // GET api/Car/5
        public HttpResponseMessage GetCar(int id)
        {
            Car car = db.Car.Find(id);
            if (car == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Tools.JsonResponse(car);
        }

        // PUT api/Car/5
        public HttpResponseMessage PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != car.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(car).State = EntityState.Modified;

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

        // POST api/Car
        public HttpResponseMessage PostCar(Car car)
        {
            if (ModelState.IsValid)
            {
                ServiceBook sb = car.ServiceBook;
                car.ServiceBook = null;
                car.ServiceBookID = sb.ID;
                db.Car.Add(car);
                db.Entry(car.Brand).State = EntityState.Unchanged;
                db.SaveChanges();
                car.ServiceBook = sb;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, car);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = car.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Car/5
        public HttpResponseMessage DeleteCar(int id)
        {
            Car car = db.Car.Find(id);
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Car.Remove(car);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, car);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}