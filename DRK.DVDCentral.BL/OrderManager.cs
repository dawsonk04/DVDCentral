﻿using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class OrderManager
    {
        public static int Insert(int customerId, DateTime orderDate, int userId, DateTime shipDate, ref int id, bool rollback = false)
        {
            try
            {
                Order order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = orderDate,
                    UserId = userId,
                    ShipDate = shipDate

                };
                int results = Insert(order, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = order.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = new tblOrder();
                    entity.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(s => s.Id) + 1 : 1;
                    entity.CustomerId = order.CustomerId;
                    entity.OrderDate = order.OrderDate;
                    entity.UserId = order.UserId;
                    entity.ShipDate = order.ShipDate;



                    // IMPORTANT - BACK FILL THE ID
                    order.Id = entity.Id;

                    dc.tblOrders.Add(entity);
                    results = dc.SaveChanges();

                    // Insert OrderItems
                    foreach (var item in order.OrderItems)
                    {
                        item.OrderId = order.Id;

                        OrderItemManager.Insert(item, rollback);
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

        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == order.Id);

                    if (entity != null)
                    {
                        entity.CustomerId = order.CustomerId;
                        entity.OrderDate = order.OrderDate;
                        entity.UserId = order.UserId;
                        entity.ShipDate = order.ShipDate;


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
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrders.Remove(entity);
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

        public static Order LoadById(int id)
        {
            #region oldCode
            // using (DVDCentralEntities dc = new DVDCentralEntities())
            //{
            // Have to change this to do database joins 
            //    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);
            //    if (entity != null)
            //    {
            //        return new Order
            //        {
            //            Id = entity.Id,
            //            CustomerId = entity.CustomerId,
            //            OrderDate = entity.OrderDate,
            //            UserId = entity.UserId,
            //            ShipDate = entity.ShipDate,
            //            OrderItems = OrderItemManager.LoadbyOrderId(id),

            //        };
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }


            //}
            #endregion

            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    var entity = (from o in dc.tblOrders
                                  join oi in dc.tblOrderItems on o.Id equals oi.OrderId
                                  join c in dc.tblCustomers on o.CustomerId equals c.Id
                                  join u in dc.tblUsers on o.UserId equals u.Id
                                  where o.Id == id
                                  select new
                                  {
                                      o.Id,
                                      o.CustomerId,
                                      o.OrderDate,
                                      o.UserId,
                                      o.ShipDate,

                                      c.FirstName,
                                      c.LastName,

                                      OrderItems = OrderItemManager.LoadbyOrderId(id),

                                      UserFirstName = u.FirstName,
                                      UserLastName = u.LastName,



                                  }).FirstOrDefault();
                    if (entity != null)
                    {
                        return new Order
                        {
                            Id = entity.Id,
                            CustomerId = entity.CustomerId,
                            OrderDate = entity.OrderDate,
                            UserId = entity.UserId,
                            ShipDate = entity.ShipDate,
                            OrderItems = OrderItemManager.LoadbyOrderId(id),
                            CustomerFirstName = entity.FirstName,
                            CustomerLastName = entity.LastName,
                            UserName = entity.UserFirstName,
                            UserFirstName = entity.UserFirstName,
                            UserLastName = entity.LastName,

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

        public static List<Order> Load(int? CustomerId = null)
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {

                    (from o in dc.tblOrders
                     join oi in dc.tblOrderItems on o.Id equals oi.OrderId
                     join c in dc.tblCustomers on o.CustomerId equals c.Id
                     join u in dc.tblUsers on o.UserId equals u.Id
                     where o.CustomerId == CustomerId || CustomerId == null
                     select new
                     {
                         o.Id,
                         o.CustomerId,
                         o.OrderDate,
                         o.UserId,
                         o.ShipDate,

                         c.FirstName,
                         c.LastName,

                         Username = u.UserId,
                         UserFirstName = u.FirstName,
                         UserLastName = u.LastName,

                         //Total = OrderItemManager.LoadbyOrderId(c.Id)

                     })
                     .ToList()
                     .ForEach(order => list.Add(new Order
                     {
                         Id = order.Id,
                         CustomerId = order.CustomerId,
                         OrderDate = order.OrderDate,
                         UserId = order.UserId,
                         ShipDate = order.ShipDate,


                         CustomerFirstName = order.FirstName,
                         CustomerLastName = order.LastName,
                         UserName = order.Username,
                         UserFirstName = order.UserFirstName,
                         UserLastName = order.LastName,




                     })); ;
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
