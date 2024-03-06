using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(string firstName, string lastName, int userId, string address,
         string city, string state, string zip, string phone, ref int id, bool rollback = false)
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserId = userId,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip,
                    Phone = phone

                };
                int results = Insert(customer, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = customer.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer entity = new tblCustomer();
                    entity.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = customer.FirstName;
                    entity.LastName = customer.LastName;
                    entity.UserId = customer.UserId;
                    entity.Address = customer.Address;
                    entity.City = customer.City;
                    entity.State = customer.State;
                    entity.ZIP = customer.Zip;
                    entity.Phone = customer.Phone;




                    // IMPORTANT - BACK FILL THE ID
                    customer.Id = entity.Id;

                    dc.tblCustomers.Add(entity);
                    results = dc.SaveChanges();


                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == customer.Id);

                    if (entity != null)
                    {
                        entity.FirstName = customer.FirstName;
                        entity.LastName = customer.LastName;
                        entity.UserId = customer.UserId;
                        entity.Address = customer.Address;
                        entity.State = customer.State;
                        entity.ZIP = customer.Zip;
                        entity.Phone = customer.Phone;

                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;

            }
            catch (Exception)
            {

                throw;

            }

        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblCustomers.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;

            }
            catch (Exception)
            {

                throw;

            }

        }

        public static Customer LoadById(int id)
        {
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Customer
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            UserId = entity.UserId,
                            Address = entity.Address,
                            City = entity.City,
                            State = entity.State,
                            Zip = entity.ZIP,
                            Phone = entity.Phone

                        };
                    }
                    else
                    {
                        throw new Exception();
                    }


                }


            }

            catch (Exception)
            {

                throw;
            }
        }

        public static List<Customer> Load()
        {
            try
            {
                List<Customer> list = new List<Customer>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from s in dc.tblCustomers
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.UserId,
                         s.Address,
                         s.City,
                         s.State,
                         s.ZIP,
                         s.Phone
                     })
                     .ToList()
                     .ForEach(customer => list.Add(new Customer
                     {
                         Id = customer.Id,
                         FirstName = customer.FirstName,
                         LastName = customer.LastName,
                         UserId = customer.UserId,
                         Address = customer.Address,
                         City = customer.City,
                         State = customer.State,
                         Zip = customer.ZIP,
                         Phone = customer.Phone
                     }));
                }

                return list;
            }

            catch (Exception)
            {

                throw;
            }
        }

    }
}
