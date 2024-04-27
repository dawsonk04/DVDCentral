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
                    //tblMovie entity = dc.tblMovies.FirstOrDefault(s => s.Id == id);
                    var entity = (from m in dc.tblMovies
                                  join mg in dc.tblMovieGenres on m.Id equals mg.MovieId
                                  join r in dc.tblRatings on m.RatingId equals r.Id
                                  join f in dc.tblFormats on m.FormatId equals f.Id
                                  join d in dc.tblDirectors on m.DirectorId equals d.Id
                                  where m.Id == id
                                  select new
                                  {
                                      m.Id,
                                      m.Title,
                                      m.Description,
                                      m.Cost,
                                      m.InStkQty,
                                      Rating = r.Description,
                                      Format = f.Description,
                                      DirectorFullName = d.FirstName + " " + d.LastName,
                                      m.ImagePath,
                                      Genres = GenreManager.Load(id)
                                 })
                              .FirstOrDefault();

                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            Cost = entity.Cost,
                            InStkQty = entity.InStkQty,
                            RatingDescription = entity.Description,
                            FormatDescription = entity.Format,
                            DirectorFullName = entity.DirectorFullName,
                            ImagePath = entity.ImagePath,
                            Genres = GenreManager.Load(id)

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
                    (from m in dc.tblMovies
                     join r in dc.tblRatings on m.RatingId equals r.Id
                     join f in dc.tblFormats on m.FormatId equals f.Id
                     join d in dc.tblDirectors on m.DirectorId equals d.Id
                     join mg in dc.tblMovieGenres on m.Id equals mg.MovieId
                     join g in dc.tblGenres on mg.GenreId equals g.Id
                     where mg.GenreId == genreId || genreId == null
                     select new
                     {
                         // creating a record set from the tblMovie fields
                         m.Id,
                         m.Title,
                         m.Description,
                         m.FormatId,
                         FormatDescription = f.Description,
                         m.DirectorId,
                         DirectorFullName = d.FirstName + " " + d.LastName,
                         m.RatingId,
                         RatingDescription = r.Description,
                         m.Cost,
                         m.InStkQty,
                         m.ImagePath
                     })
                    .Distinct()
                    .ToList()
                    .ForEach(movie => list.Add(new Movie
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        FormatId = movie.FormatId,
                        FormatDescription = movie.FormatDescription,
                        DirectorId = movie.DirectorId,
                        DirectorFullName = movie.DirectorFullName,
                        RatingId = movie.RatingId,
                        RatingDescription = movie.RatingDescription,
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

