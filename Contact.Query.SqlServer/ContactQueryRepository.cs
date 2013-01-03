﻿using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Query.Contracts;
using Model = Contact.Query.Contracts.Model;

namespace Contact.Query.SqlServer
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        public void Save(Model.AccommodationLead accommodationLead)
        {
            using (var context = new ContactEntities())
            {
                var accLeadToSave =
                    context.AccommodationLeads.SingleOrDefault(
                        x => x.AccommodationLeadId == accommodationLead.AccommodationLeadId)
                    ?? new AccommodationLead();

                accLeadToSave.AccommodationLeadId = accommodationLead.AccommodationLeadId;
                accLeadToSave.Name = accommodationLead.Name;
                accLeadToSave.Email = accommodationLead.Email;
                accLeadToSave.Approved = accommodationLead.Approved;

                if (accLeadToSave.Id == 0)
                    context.AccommodationLeads.Add(accLeadToSave);

                context.SaveChanges();
            }
        }

        public void Save(Model.AccommodationSupplier accommodationSupplier)
        {

            using (var context = new ContactEntities())
            {
                var accSupplierToSave = context.AccommodationSuppliers.SingleOrDefault(
                    x => x.AccommodationSupplierId == accommodationSupplier.AccommodationSupplierId)
                                        ?? new AccommodationSupplier();

                accSupplierToSave.AccommodationSupplierId = accommodationSupplier.AccommodationSupplierId;
                accSupplierToSave.Name = accommodationSupplier.Name;
                accSupplierToSave.Email = accommodationSupplier.Email;

                if (accSupplierToSave.Id == 0)
                    context.AccommodationSuppliers.Add(accSupplierToSave);

                context.SaveChanges();
            }
        }

        public void Save(Model.Authentication authentication)
        {

            using (var context = new ContactEntities())
            {
                var authToSave = context.Authentications.SingleOrDefault(
                    x => x.AuthenticationId == authentication.AuthenticationId)
                                 ?? new Authentication();

                authToSave.AuthenticationId = authentication.AuthenticationId;
                authToSave.Email = authentication.Email;
                authToSave.HashedPassword = authentication.HashedPassword;

                if (authToSave.Id == 0)
                    context.Authentications.Add(authToSave);

                context.SaveChanges();
            }
        }

        public void Save(Model.User user)
        {

            using (var context = new ContactEntities())
            {
                var userToSave = context.Users.SingleOrDefault(
                    x => x.UserId == user.UserId)
                                 ?? new User();

                userToSave.UserId = user.UserId;
                userToSave.Name = user.Name;
                userToSave.Email = user.Email;

                if (userToSave.Id == 0)
                    context.Users.Add(userToSave);

                context.SaveChanges();
            }
        }

        public List<Model.AccommodationLead> ListAccommodationLeads()
        {

            using (var context = new ContactEntities())
            {
                return context.AccommodationLeads.Select(x =>
                                                         new Model.AccommodationLead
                                                             {
                                                                 AccommodationLeadId =
                                                                     x.AccommodationLeadId,
                                                                 Email = x.Email,
                                                                 Name = x.Name,
                                                                 Id = x.Id,
                                                                 Approved = x.Approved
                                                             }).ToList();
            }
        }

        public Model.AccommodationLead GetAccommodationLeadById(Guid id)
        {
            using (var context = new ContactEntities())
            {
                return context.AccommodationLeads.Where(x => x.AccommodationLeadId == id)
                              .Select(lead => new Model.AccommodationLead
                                  {
                                      Id = lead.Id,
                                      AccommodationLeadId = lead.AccommodationLeadId,
                                      Name = lead.Name,
                                      Email = lead.Email,
                                      Approved = lead.Approved
                                  }).SingleOrDefault();
            }
        }
    }
}
