//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contact.Query.SqlServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Authentication
    {
        public int Id { get; set; }
        public System.Guid AuthenticationId { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}