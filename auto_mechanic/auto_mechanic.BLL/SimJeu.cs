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
    
    public partial class SimJeu
    {
        public SimJeu()
        {
            this.SimIterJeu = new HashSet<SimIterJeu>();
        }
    
        public int ID { get; set; }
        public System.DateTime DateBegin { get; set; }
        public string Duration { get; set; }
        public string Param { get; set; }
        public string Init { get; set; }
    
        public virtual ICollection<SimIterJeu> SimIterJeu { get; set; }
    }
}
