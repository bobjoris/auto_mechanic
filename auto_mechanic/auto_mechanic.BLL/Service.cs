//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace auto_mechanic.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public Service()
        {
            this.Mechanic_Service = new HashSet<Mechanic_Service>();
            this.ServiceBook = new HashSet<ServiceBook>();
        }
    
        public int ID { get; set; }
        public string Label { get; set; }
        public int KM { get; set; }
    
        public virtual ICollection<Mechanic_Service> Mechanic_Service { get; set; }
        public virtual ICollection<ServiceBook> ServiceBook { get; set; }
    }
}
