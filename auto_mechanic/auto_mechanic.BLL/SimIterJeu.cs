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
    
    public partial class SimIterJeu
    {
        public int ID { get; set; }
        public int SimID { get; set; }
        public string Repair { get; set; }
        public string Drive { get; set; }
        public string Planning { get; set; }
    
        public virtual SimJeu SimJeu { get; set; }
    }
}
