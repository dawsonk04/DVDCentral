using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class OrderItemManager
    {
        public static int Insert(int orderId, int quanity, int movieId, float cost, ref int id, bool rollback = false)
        {
            try
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = orderId,
                    Quantity = quanity,
                    MovieId = movieId,
                    Cost = cost

                };
                int results = Insert(orderItem, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = orderItem.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = new tblOrderItem();
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(s => s.Id) + 1 : 1;
                    entity.OrderId = orderItem.OrderId;
                    entity.Quantity = orderItem.Quantity;
                    entity.MovieId = orderItem.MovieId;
                    entity.Cost = orderItem.Cost;




                    // IMPORTANT - BACK FILL THE ID
                    orderItem.Id = entity.Id;

                    dc.tblOrderItems.Add(entity);
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

        public static int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(s => s.Id == orderItem.Id);

                    if (entity != null)
                    {
                        entity.OrderId = orderItem.OrderId;
                        entity.Quantity = orderItem.Quantity;
                        entity.MovieId = orderItem.MovieId;
                        entity.Cost = orderItem.Cost;

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
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrderItems.Remove(entity);
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
        public static List<OrderItem> LoadbyOrderId(int orderId)
        {
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    #region 
                    //tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == orderId);
                    //if (entity != null)
                    //{
                    //    Order order = new Order
                    //    {
                    //        Id = entity.Id,
                    //        CustomerId = entity.CustomerId,
                    //        OrderDate = entity.OrderDate,
                    //        UserId = entity.UserId,
                    //        ShipDate = entity.ShipDate,
                    //        OrderItems = new List<OrderItem>()


                    //    };

                    //    List<OrderItem> orderItems = new List<OrderItem>();
                    //    var orderItemEntities = dc.tblOrderItems.Where(oi => oi.OrderId == orderId).ToList();
                    //    foreach (var item in orderItemEntities)
                    //    {
                    //        orderItems.Add(new OrderItem
                    //        {
                    //            Id = item.Id,
                    //            OrderId = item.OrderId,
                    //            MovieId = item.MovieId,
                    //            Cost = item.Cost,
                    //            Quantity = item.Quantity
                    //        });


                    //    }
                    //    return orderItems;
                    //}
                    //else
                    //{
                    //    throw new Exception();
                    //}

                    #endregion
                    List<OrderItem> orderItems = new List<OrderItem>();

                    var entites = (from oi in dc.tblOrderItems
                                   join m in dc.tblMovies on oi.MovieId equals m.Id
                                   where oi.OrderId == orderId
                                   select new
                                   {
                                       oi.Id,
                                       oi.OrderId,
                                       oi.MovieId,
                                       oi.Quantity,
                                       oi.Cost,
                                       m.Title,
                                       m.ImagePath
                                   }).ToList();

                    foreach (var entity in entites)
                    {
                        orderItems.Add(new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            MovieId = entity.Id,
                            Quantity = entity.Quantity,
                            Cost = entity.Cost,
                            MovieTitle = entity.Title,
                            ImagePath = entity.ImagePath,
                        });
                    }
                    return orderItems;
                }


            }

            catch (Exception)
            {

                throw;
            }
        }

        public static OrderItem LoadById(int id)
        {
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            Quantity = entity.Quantity,
                            MovieId = entity.MovieId,
                            Cost = (float)entity.Cost

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

        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from s in dc.tblOrderItems
                     join m in dc.tblMovies
                     on s.MovieId equals m.Id
                     select new
                     {
                         s.Id,
                         s.OrderId,
                         s.Quantity,
                         s.MovieId,
                         s.Cost,
                         m.Title,
                         m.ImagePath

                     })
                     .ToList()
                     .ForEach(orderItem => list.Add(new OrderItem
                     {
                         Id = orderItem.Id,
                         OrderId = orderItem.OrderId,
                         Quantity = orderItem.Quantity,
                         MovieId = orderItem.MovieId,
                         Cost = (float)orderItem.Cost,
                         MovieTitle = orderItem.Title,
                         ImagePath = orderItem.ImagePath
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
