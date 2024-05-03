using DRK.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static void Insert(int movieId, int genreId, bool rollback = false)
        {
            //again I made it like the student advisor manager but it did not work so I just did what we usually do
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction? transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = new tblMovieGenre();
                    entity.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(s => s.Id) + 1 : 1;

                    entity.GenreId = genreId;
                    entity.MovieId = movieId;

                    dc.tblMovieGenres.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction?.Rollback();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static void Delete(int movieId, int genreId, bool rollback = false)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblMovieGenre? tblMovieGenre = dc.tblMovieGenres.FirstOrDefault(sa => sa.MovieId == movieId && sa.GenreId == genreId);

                    if (tblMovieGenre != null)
                    {
                        dc.tblMovieGenres.Remove(tblMovieGenre);

                        //results = dc.SaveChanges();

                        dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                    if (rollback) transaction.Rollback();
                }
            }
            catch (Exception) { throw; }




        }
    }
}








