using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using auto_mechanic.BLL;
using auto_mechanic.Models;
using System.Threading;

namespace auto_mechanic.Controllers
{
    public class SimulationController : ApiController
    {
        private AutomechanicsEntities db = new AutomechanicsEntities();

        // GET api/simulation
        public HttpResponseMessage Get()
        {
            String param = "2014-05-19,100;1:2,2:0,4:0,11:0,10:0;6:0,7:0,14:0,27:12,28:0;1,3";

            DateTime dt = new DateTime();
            int count = 0;
            Dictionary<int, int> Brands = new Dictionary<int, int>();
            Dictionary<int, int> Cars = new Dictionary<int, int>();
            List<int> Mechanics = new List<int>();

            splitString(param, ref dt, ref count, ref Brands, ref Cars, ref Mechanics);

            String res = performSimulation(dt, count, Brands, Cars, Mechanics);
            return new HttpResponseMessage()
            {
                Content = new StringContent(res, Encoding.UTF8, "text/plain")
            };
        }

        // GET api/simulation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/simulation
        public void Post([FromBody]string value)
        {
            DateTime dt = new DateTime();
            int count= 0;
            Dictionary<int, int> Brands = new Dictionary<int, int>();
            Dictionary<int, int> Cars = new Dictionary<int, int>();
            List<int> Mechanics = new List<int>();

            splitString(value, ref dt, ref count, ref Brands, ref Cars, ref Mechanics);

            this.performSimulation(dt, count, Brands, Cars, Mechanics);

        }

        // PUT api/simulation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/simulation/5
        public void Delete(int id)
        {
        }

        private void splitString(string value, ref DateTime dt, ref int count, ref Dictionary<int, int> Brands,ref Dictionary<int, int> Cars,ref List<int> Mechanics)
        {
            String[] values = value.Split(';');

            //Date
            String[] date = values[0].Split(',');
            dt = DateTime.ParseExact(date[0], "yyyy-mm-dd", null);
            count = int.Parse(date[1]);

            //Marque
            String[] brands = values[1].Split(',');

            foreach (String b in brands)
            {
                Brands.Add(int.Parse(b.Split(':')[0]), int.Parse(b.Split(':')[1]));
            }

            // Modele
            String[] cars = values[2].Split(',');

            foreach (String b in cars)
            {
                Cars.Add(int.Parse(b.Split(':')[0]), int.Parse(b.Split(':')[1]));
            }

            // Garagiste
            String[] mechanics = values[3].Split(',');

            foreach (String m in mechanics)
            {
                Mechanics.Add(int.Parse(m));
            }
        }

        private string performSimulation(DateTime date, int dayCount, Dictionary<int,int> inputBrands, Dictionary<int,int> inputCars, List<int> inputMechanic)
        {
            StringBuilder res = new StringBuilder();

            List<SimCar> listCars = new List<SimCar>(); // Liste de toutes les voitures qui roulent
            List<SimCar> waitCar = new List<SimCar>(); // Liste des voitures en attentes de révision
            List<SimMechanic> mechanics = new List<SimMechanic>();

            /**
             *  Chargement des voitures
             */

            // Chargement par marque
            foreach (var pair in inputBrands)
            {
                List<Car> carsBrands = db.Car.Where(x => x.BrandID == pair.Key).ToList();
                int limit = carsBrands.Count - 1;
                for (int i = 0; i < pair.Value; i++)
                {
                    Random r = new Random();
                    listCars.Add(new SimCar(carsBrands[r.Next(0, limit)]));
                }
            }
            
            // Chargement par model
            foreach (var pair in inputCars)
            {
                Car car = db.Car.Where(x => x.ID == pair.Key).FirstOrDefault();
                for (int i = 0; i < pair.Value; i++)
                {
                    listCars.Add(new SimCar(car));
                }
            }

            // Chargement des mécanicien
            foreach (int mechanicID in inputMechanic)
            {
                Mechanic mechanic = db.Mechanic.Where(x => x.ID == mechanicID).FirstOrDefault();
                mechanics.Add(new SimMechanic(mechanic, date, dayCount));
            }
                

            res.AppendLine(mechanicsOutput(mechanics));

            // Début de la simulation
            for (int i = 0; i < dayCount; i++)
            {
                StringBuilder dayString = new StringBuilder();
                //Assignation des voitures au garagiste que les jours de semaine
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    dayString.AppendLine("** Week-end pas de réparation");
                }
                else
                {
                    for (int j = 0; j < waitCar.Count; j++)
                    {
                        SimCar sCar = waitCar[j];
                        if (!sCar.NextDelivery.HasValue)
                        {
                            SimMechanic sm = mechanics.Where(x => x.WorkingTime[i].TimeRemaining > 0).FirstOrDefault();
                            if (sm != null)
                            {
                                sm.SetService(i, ref sCar, date.AddDays(1).DayOfWeek == DayOfWeek.Saturday);
                            }
                        }
                    }


                    foreach (SimMechanic sm in mechanics.Where(x => x.WorkingTime[i].TimeRemaining < 8))
                    {
                        dayString.AppendLine(String.Format
                        ("**Planning de {0} ({1})", sm.Mechanic.Name, sm.Mechanic.Franchise.Name));
                        dayString.AppendLine(sm.WorkingTime[i].ToString());
                    }
                }

                #region Parcours de KM
                // Récupération de la liste des voitures qui roulent
                List<SimCar> tempDriveCars = listCars.ToList();
                List<SimCar> carsToRevision = new List<SimCar>(); // Liste temp des voitures à ajouter en révision
                dayString.AppendLine(String.Format("** Distance parcourue ({0} voitures)", tempDriveCars.Count));
                foreach (SimCar sCar in tempDriveCars)
                {
                    // On ajout le nombre de KM
                    int km = sCar.GetDailyKm(date);
                    dayString.Append(String.Format("\t - {0} {1} : {2} ({3}) == Révision : ", sCar.Car.Brand.Name, sCar.Car.Name, km, sCar.Km));
                    // Vérification d'une révision
                    bool rev = false;
                    if (sCar.IsNeedService())
                    {
                        rev = true;
                        carsToRevision.Add(sCar);
                        listCars.Remove(sCar);
                    }
                    dayString.AppendLine(String.Format("{0} {1} - {2} Km", (rev) ? "Oui" : "Non", sCar.NextService.Label, sCar.NextService.KM));
                }

                #endregion
                waitCar.AddRange(carsToRevision);

                // On remets les voitures traités dans la liste des voitures qui roulent
                List<SimCar> revisedCars = waitCar.Where(x => x.IsDeliverable(i)).ToList();

                foreach (SimCar rCar in revisedCars)
                {
                    waitCar.Remove(rCar);
                    rCar.SetNextService();
                    listCars.Add(rCar);
                }

                // On ajoute le jour que s'il y a des faits marquants
                if (dayString.Length > 0)
                {
                    res.AppendLine("==========================================");
                    res.AppendLine(date.ToShortDateString());
                    res.AppendLine(dayString.ToString());
                }

                // Passage au lendemain
                date = date.AddDays(1);
            }

            return res.ToString();
        }

        #region Output
        private string mechanicsOutput(List<SimMechanic> list)
        {
            StringBuilder res = new StringBuilder();

            res.AppendLine(String.Format("/****** Mécaniciens ********/"));
            res.AppendLine(String.Format("Nombre de mécaniciens : {0}", list.Count));
            res.AppendLine("Liste : ");
            foreach(SimMechanic sm in list)
                res.AppendLine(String.Format("\t {0} ({1})", sm.Mechanic.Name, sm.Mechanic.Franchise.Name));
            res.AppendLine(String.Format("/******             ********/"));

            return res.ToString();
        } 
        #endregion

        #region Select
        private List<SimCar> SelectCarsForBrand(int idBrand, int count)
        {
            List<SimCar> res = new List<SimCar>();
            List<Car> cars = db.Car.Where(x => x.BrandID == idBrand).ToList();
            Random random = new Random();
            int rdmLimit = cars.Count - 1;

            for (int i = 0; i < count; i++)
            {
                res.Add(new SimCar(cars[random.Next(0, rdmLimit)]));
            }

            return res;
        } 
        #endregion
    }

    public class SimMechanic
    {
        public Mechanic Mechanic { get; set; }
        public List<SimSchedule> WorkingTime { get; set; }

        public SimMechanic(Mechanic mechanic, DateTime dt, int count)
        {
            this.Mechanic = mechanic;
            WorkingTime = new List<SimSchedule>();
            this.FillWorkingTime(dt, count);
        }

        private void FillWorkingTime(DateTime dt, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    WorkingTime.Add(new SimSchedule(0));
                else
                    WorkingTime.Add(new SimSchedule(8));

                dt = dt.AddDays(1);
            }
        }

        public Holiday GetCurrentHoliday(DateTime dt)
        {
            List<Holiday> holiday = Mechanic.Holiday.Where(x => x.StartDate >= dt.Date && dt.Date >= x.EndDate).ToList();

            return (holiday.Count > 0) ? holiday[0] : null;
        }

        public bool IsInHoliday(DateTime dt)
        {
            return this.GetCurrentHoliday(dt) != null;
        }

        public void SetService(int iteration, ref SimCar ref_car, bool weekend)
        {
            SimCar car = ref_car;
            this.WorkingTime[iteration].Cars.Add(car);
            int serviceDuration = 5;
            Mechanic_Service mecha_service = Mechanic.Mechanic_Service.Where(x => x.ServiceID == car.NextService.ID).FirstOrDefault();

            if (mecha_service != null)
                serviceDuration = mecha_service.Duration;

            if (serviceDuration <= this.WorkingTime[iteration].TimeRemaining)
            {
                this.WorkingTime[iteration].TimeRemaining -= serviceDuration;
                ref_car.NextDelivery = iteration;
            }
            else
            {
                int timeremain = serviceDuration - this.WorkingTime[iteration].TimeRemaining;
                this.WorkingTime[iteration].TimeRemaining = 0;

                int offset = (weekend) ? 3 : 1;
                if (offset + iteration < this.WorkingTime.Count - 1)
                {
                    this.WorkingTime[iteration + offset].TimeRemaining -= timeremain;
                    this.WorkingTime[iteration + offset].Cars.Add(car);
                    ref_car.NextDelivery = iteration + offset;
                }

            }
        }
    }

    public class SimSchedule
    {
        public int TimeRemaining { get; set; }
        public List<SimCar> Cars { get; set; }

        public SimSchedule(int time)
        {
            this.TimeRemaining = time;
            this.Cars = new List<SimCar>();
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.AppendLine("** Planning réparation **");
            res.AppendLine(String.Format("Temps restant : {0}", this.TimeRemaining));
            res.AppendLine("Véhicules : ");

            foreach (SimCar c in Cars)
                res.Append(String.Format("{0} [{1} - {2}] | ", c.Car.Name, c.NextService.Label, c.NextService.KM));

            return res.ToString();
        }
    }

    public class SimCar
    {
        public Car Car { get; set; }
        public int Km { get; set; }
        public Service NextService { get; set; }
        public Nullable<int> NextDelivery { get; set; }

        public SimCar(Car car)
        {
            this.Car = car;
            Thread.Sleep(1);
            this.Km = new Random().Next(20,2000);
            this.SetNextService();
        }

        public int GetDailyKm(DateTime dt)
        {
            // Nécessaire pour avoir des nombres aléatoires
            Thread.Sleep(1);
            Random rd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int res;

            if(dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                res = rd.Next(50, 100);
            else
                res = rd.Next(20,50);

            this.Km += res;

            return res;
        }

        public bool IsDeliverable(int iteration)
        {

            return (this.NextDelivery.HasValue && this.NextDelivery == iteration);
        }

        public bool IsNeedService()
        {
            return (this.NextService != null && this.Km >= this.NextService.KM);
        }

        public void SetNextService()
        {
            Service res = this.Car.ServiceBook.Service.Where(x => x.KM > this.Km).OrderBy(x => x.KM).FirstOrDefault();

            this.NextService = res;
            this.NextDelivery = null;
        }
    }
}
