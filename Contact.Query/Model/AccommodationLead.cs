﻿using System;

namespace Contact.Query.Model
{
    public class AccommodationLead
    {
        public int Id { get; set; }
        public Guid AccommodationLeadId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
    }
}