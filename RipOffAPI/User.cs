//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RipOffAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Rentals = new HashSet<Rental>();
        }
    
        public int uid { get; set; }
        public string Full_Name { get; set; }
        public string ID_Number { get; set; }
        public string User_Name { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public string Permissions { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
