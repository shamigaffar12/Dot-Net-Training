using MVC_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CodeFirst.Repositories
{
   
        public interface IMovie
        {
            IEnumerable<Movies> GetAll();
            Movies GetById(int id);
            void Add(Movies movie);
            void Update(Movies movie);
            void Delete(int id);
            IEnumerable<Movies> GetByYear(int year);
            IEnumerable<Movies> GetByDirector(string director);
            void Save();
        }
    }

}
