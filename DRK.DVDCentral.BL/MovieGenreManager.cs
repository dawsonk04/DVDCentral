using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static int Insert(int movieId, int genreId, ref int id, bool rollback = false)
        {
            try
            {
                tblMovieGenre movieGenre = new tblMovieGenre();
                {
                    movieGenre.MovieId = movieId;
                    movieGenre.GenreId = genreId;

                };
                int results = Insert(movieGenre, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = movieGenre.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(tblMovieGenre movieGenre, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = new tblMovieGenre();

                    entity.MovieId = movieGenre.MovieId;
                    entity.GenreId = movieGenre.GenreId;



                    // IMPORTANT - BACK FILL THE ID
                    movieGenre.Id = entity.Id;

                    dc.tblMovieGenres.Add(entity);
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
                    tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblMovieGenres.Remove(entity);
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






    }

}

