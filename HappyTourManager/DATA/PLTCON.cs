//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLTCON
    {
        public int PLTCONID { get; set; }
        public int TourID { get; set; }
        public int PlaceID { get; set; }
    
        public virtual Place Place { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
