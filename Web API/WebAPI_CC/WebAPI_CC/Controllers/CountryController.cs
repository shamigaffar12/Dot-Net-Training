using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_CC.Models;

namespace Web_API_CC.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countrylist = new List<Country>();

        // GET: api/Country
        public IHttpActionResult Get()
        {
            return Ok(countrylist);
        }

        // GET: 
        public IHttpActionResult Get(int id)
        {
            var country = countrylist.FirstOrDefault(c => c.ID == id);
            if (country == null)
            return NotFound();
            return Ok(country);
        }

        // POST: add
        public IHttpActionResult Post([FromBody] Country country)
        {
            countrylist.Add(country);
            return CreatedAtRoute("DefaultApi", new { id = country.ID }, country);
        }

        // PUT update
        public IHttpActionResult Put(int id, [FromBody] Country updatedCountry)
        {
            var country = countrylist.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            country.CountryName = updatedCountry.CountryName;
            country.Capital = updatedCountry.Capital;

            return Ok(country);
        }

        // DELETE
        public IHttpActionResult Delete(int id)
        {
            var country = countrylist.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            countrylist.Remove(country);
            return Ok();
        }
    }
}