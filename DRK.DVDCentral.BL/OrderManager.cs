using DRK.DVDCentral.BL.Models;
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
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);
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

                    (from s in dc.tblOrders
                     where s.CustomerId == CustomerId || CustomerId == null
                     select new
                     {
                         s.Id,
                         s.CustomerId,
                         s.OrderDate,
                         s.UserId,
                         s.ShipDate,

                     })
                     .ToList()
                     .ForEach(order => list.Add(new Order
                     {
                         Id = order.Id,
                         CustomerId = order.CustomerId,
                         OrderDate = order.OrderDate,
                         UserId = order.UserId,
                         ShipDate = order.ShipDate,

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
