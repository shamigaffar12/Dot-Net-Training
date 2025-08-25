using System.Collections.Generic;
using System.Linq;

using MVC_CodeFirst.Models;

namespace MVC_CodeFirst.Repositories
{
    public class MovieRepo : IMovie
    {
        private MoviesContext context = new MoviesContext();

        public MovieRepo()
        {
        }

        public IEnumerable<Movies> GetAll()
        {
            return context.Movies.ToList();
        }

        public Movies GetById(int id)
        {
            return context.Movies.Find(id);
        }

        public void Add(Movies movie)
        {
            context.Movies.Add(movie);
        }

        void IMovie.Update(Movies movie)
        {
            throw new System.NotImplementedException();
        }

        void IMovie.Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Movies> IMovie.GetByYear(int year)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Movies> IMovie.GetByDirector(string director)
        {
            throw new System.NotImplementedException();
        }

        void IMovie.Save()
        {
            throw new System.NotImplementedException();
        }
    }



}