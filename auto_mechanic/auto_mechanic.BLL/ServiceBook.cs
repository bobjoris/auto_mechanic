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
    
    public partial class ServiceBook
    {
        public ServiceBook()
        {
            this.Service = new HashSet<Service>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Service> Service { get; set; }
    }
}
