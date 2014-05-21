using auto_mechanic.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace auto_mechanic.Controllers
{
    public class MechanicServiceController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/mechanicservice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/mechanicservice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/mechanicservice
        public void Post([FromBody]string value)
        {
            string[] val = value.Split(':');

            int idM = int.Parse(val[0]);
            int idS = int.Parse(val[1]);
            int duration = int.Parse(val[2]);

            Mechanic_Service ms = db.Mechanic_Service.Where(x => x.MechanicID == idM && x.ServiceID == idS).FirstOrDefault();

            if (ms != null)
                ms.Duration = duration;

            db.SaveChanges();
        }

        // PUT api/mechanicservice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mechanicservice/5
        public void Delete(int id)
        {
        }
    }
}
