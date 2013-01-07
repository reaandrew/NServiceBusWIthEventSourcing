using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Contact.Query.Contracts;
using log4net;

namespace Contact.Query.SqlServer
{
    public class SqlContactQueryRepository : IContactQueryRepository
    {
        public void Save(Contracts.Model.AccommodationLead accommodationLead)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.RepeatableRead
                                                              }))
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
                    transaction.Complete();
                }
                
            }
        }

        public void Save(Contracts.Model.AccommodationSupplier accommodationSupplier)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.RepeatableRead
                                                              }))
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
                    transaction.Complete();
                }
                
            }
        }

        public void Save(Contracts.Model.Authentication authentication)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.RepeatableRead
                                                              }))
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
                    transaction.Complete();
                }
            }
        }

        public void Save(Contracts.Model.User user)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.RepeatableRead
                                                              }))
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
                    transaction.Complete();
                }
                
            }
        }

        public List<Contracts.Model.AccommodationLead> ListAccommodationLeads()
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.ReadUncommitted
                                                              }))
            {
                using (var context = new ContactEntities())
                {
                    var leads = context.AccommodationLeads.Select(x =>
                                                             new Contracts.Model.AccommodationLead
                                                                 {
                                                                     AccommodationLeadId =
                                                                         x.AccommodationLeadId,
                                                                     Email = x.Email,
                                                                     Name = x.Name,
                                                                     Approved = x.Approved
                                                                 }).ToList();
                    transaction.Complete();
                    return leads;
                }
            }
        }

        public Contracts.Model.AccommodationLead GetAccommodationLeadById(Guid id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                                                          new TransactionOptions
                                                              {
                                                                  IsolationLevel = IsolationLevel.RepeatableRead
                                                              }))
            {
                using (var context = new ContactEntities())
                {
                    var returnlead = context.AccommodationLeads.Where(x => x.AccommodationLeadId == id)
                                  .Select(lead => new Contracts.Model.AccommodationLead
                                      {
                                          AccommodationLeadId = lead.AccommodationLeadId,
                                          Name = lead.Name,
                                          Email = lead.Email,
                                          Approved = lead.Approved
                                      }).SingleOrDefault();
                    transaction.Complete();
                    if (returnlead == null)
                    {
                        LogManager.GetLogger(this.GetType())
                                  .Error("No accommodation lead exists with id " + id.ToString());
                    }
                    return returnlead;
                }
            }
        }
    }
}