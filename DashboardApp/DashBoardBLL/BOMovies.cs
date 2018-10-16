using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoardBLL
{
    public class BOMovies
    {
        DashBoardDataClassesDataContext context  = new DashBoardDataClassesDataContext();


        public List<Movie> GetMovieList(int typeId, int year, int month, int review)
        {
            var movies = (from o in context.Movies
                          select o);
            if (typeId != 0)
                movies = (from o in movies
                          where o.MovieType1.TypeID == typeId
                          select o);
            if (year != 0)
                movies = (from o in movies
                          where o.MovieDate.Value.Year == year
                          select o);
            if (month != 0)
                movies = (from o in movies
                          where o.MovieDate.Value.Month == month
                          select o);
            if (review != 0)
                movies = (from o in movies
                          where o.Review != null && o.Review == review
                          select o);

            return movies.ToList();

        }

        public List<TypeMovie> GetMoviesGraph()
        {
            var typeMovies = (from tm in context.TypeMovies
                              select tm).ToList();

            return typeMovies;
        }

        public List<TypeMovie> GetMoviesGraph(string startDate, string endDate)
        {
            DateTime? fromDate, toDate;
            if (startDate != string.Empty)
                fromDate = Convert.ToDateTime(startDate);
            else
                fromDate = null;

            if (endDate != string.Empty)
                toDate = Convert.ToDateTime(endDate);
            else
                toDate = null;

            var movies = (from o in context.DB_GetTypeMoviesFiltered(fromDate, toDate)
                          select new TypeMovie { TypeID = o.TypeID, TypeName = o.TypeName, MoviesCount = o.MoviesCount }).ToList();



            return movies;
        }
        

        public List<DB_GetTypeMoviesResult> GetTypeMovies(int typeId)
        {
            var typeMovies = (from o in context.DB_GetTypeMovies(typeId)
                              select o).ToList();
            return typeMovies;
        }

        public MovieType GetMovieTypeByID(int typeId)
        {
            var type = (from o in context.MovieTypes
                        where o.TypeID == typeId
                        select o).FirstOrDefault();
            return type;
        }

        public List<MovieType> GetMovieTypes()
        {
            var types = (from o in context.MovieTypes
                         select o).ToList();

            return types;
        }

        public Movie GetMovieByID(int movieId)
        {
            var movie = (from o in context.Movies
                         where o.MovieID == movieId
                         select o).FirstOrDefault();
            return movie;
        }

        public List<MovieReport> GetMoviesReport()
        {
            var movies = (from o in context.Movies
                          select new MovieReport
                          {
                              MovieID = o.MovieID,
                              MovieName = o.MovieName,
                              MovieDate = o.MovieDate,
                              TypeName = o.MovieType1.TypeName,
                              MovieComment = o.MovieComment,
                              Rating = o.Review
                          }).ToList();
            return movies;
        }

        public List<MovieReport> GetMoviesReport(string searchValue)
        {
            var movies = (from o in context.Movies
                          where o.MovieName.Contains(searchValue) || o.MovieComment.Contains(searchValue)
                          select new MovieReport
                          {
                              MovieID = o.MovieID,
                              MovieName = o.MovieName,
                              MovieDate = o.MovieDate,
                              TypeName = o.MovieType1.TypeName,
                              MovieComment = o.MovieComment,
                              Rating = o.Review
                          }).ToList();
            return movies;
        }

        public bool AddMovie(string movieName, int movieTypeId, string movieDate, string movieComment, int review)
        {
            try
            {
                Movie newMovie = new Movie();
                newMovie.MovieName = movieName;
                if (movieTypeId == 0)
                    newMovie.MovieType1 = null;
                else
                    newMovie.MovieType1 = (from o in context.MovieTypes
                                       where o.TypeID == movieTypeId
                                       select o).FirstOrDefault();
                newMovie.MovieDate = DateTime.Parse(movieDate);
                newMovie.MovieComment = movieComment;
                newMovie.Review = review;

                context.Movies.InsertOnSubmit(newMovie);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateMovie(int movieId, string movieName, int movieTypeId, string movieDate, string movieComment, int review)
        {
            try
            {
                Movie movie = (from o in context.Movies
                               where o.MovieID == movieId
                               select o).FirstOrDefault();


                movie.MovieName = movieName;
                if (movieTypeId == 0)
                    movie.MovieType1 = null;
                else
                    movie.MovieType1 = (from o in context.MovieTypes
                                           where o.TypeID == movieTypeId
                                           select o).FirstOrDefault();
                movie.MovieDate = DateTime.Parse(movieDate);
                movie.MovieComment = movieComment;
                movie.Review = review;

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class MovieReport
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string TypeName { get; set; }
        public DateTime? MovieDate { get; set; }
        public string MovieComment { get; set; }
        public int? Rating { get; set; }
    }
}
