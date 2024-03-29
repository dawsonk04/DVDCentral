using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class MovieManager
    {
        public static int Insert(string title
                                , string description,
                                 int formatId,
                                 int directorId,
                                 int ratingId,
                                 float cost,
                                 int instkQty,
                                 string imgPath,
                                ref int id
                                , bool rollback = false)
        {
            try
            {
                Movie movie = new Movie
                {
                    Title = title,
                    Description = description,
                    FormatId = formatId,
                    DirectorId = directorId,
                    RatingId = ratingId,
                    Cost = cost,
                    InStkQty = instkQty,
                    ImagePath = imgPath

                };
                int results = Insert(movie, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = movie.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = new tblMovie();
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(s => s.Id) + 1 : 1;
                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.RatingId = movie.RatingId;
                    entity.Cost = movie.Cost;
                    entity.InStkQty = movie.InStkQty;
                    entity.ImagePath = movie.ImagePath;




                    // IMPORTANT - BACK FILL THE ID
                    movie.Id = entity.Id;

                    dc.tblMovies.Add(entity);
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

        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblMovie entity = dc.tblMovies.FirstOrDefault(s => s.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.Title = movie.Title;
                        entity.Description = movie.Description;
                        entity.FormatId = movie.FormatId;
                        entity.DirectorId = movie.DirectorId;
                        entity.RatingId = movie.RatingId;
                        entity.Cost = movie.Cost;
                        entity.InStkQty = movie.InStkQty;
                        entity.ImagePath = movie.ImagePath;



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
                    tblMovie entity = dc.tblMovies.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblMovies.Remove(entity);
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

        public static Movie LoadById(int id)
        {
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            FormatId = entity.FormatId,
                            DirectorId = entity.DirectorId,
                            RatingId = entity.RatingId,
                            Cost = (float)entity.Cost,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath

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

        public static List<Movie> Load(int? genreId = null)
        {
            try
            {
                List<Movie> list = new List<Movie>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // This is what I think how I am supposed to join the tables togather
                    (from s in dc.tblMovies
                     join mg in dc.tblMovieGenres on s.Id equals mg.MovieId
                     join g in dc.tblGenres on mg.GenreId equals g.Id
                     join r in dc.tblRatings on s.RatingId equals r.Id
                     join d in dc.tblDirectors on s.DirectorId equals d.Id
                     join f in dc.tblFormats on s.FormatId equals f.Id

                     select new
                     {
                         s.Id,
                         s.Title,
                         s.Description,
                         RatingDescription = r.Description,
                         FormatDescription = f.Description,
                         FullName = d.FirstName + " " + d.LastName,
                         s.FormatId,
                         s.DirectorId,
                         s.RatingId,
                         s.Cost,
                         s.InStkQty,
                         s.ImagePath,


                     })
                     .ToList()
                     .ForEach(movie => list.Add(new Movie
                     {
                         Id = movie.Id,
                         RatingDescription = movie.RatingDescription,
                         FormatDescription = movie.FormatDescription,
                         FullName = movie.FullName,
                         Title = movie.Title,
                         Description = movie.Description,
                         FormatId = movie.FormatId,
                         DirectorId = movie.DirectorId,
                         RatingId = movie.RatingId,
                         Cost = (float)movie.Cost,
                         InStkQty = movie.InStkQty,
                         ImagePath = movie.ImagePath
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

