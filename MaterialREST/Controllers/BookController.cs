using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/book")]
    [ApiController]
    public class BookController : ControllerBase
    {

        BookRepository bookRepo = new BookRepository();
        // GET: api/<BookController>
        [HttpGet]
        public List<book> Get()
        {
            List<book> books = null;
            try
            {
                books = (List<book>)bookRepo.GetAll();
                return books;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }

            return books;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public book Get(int id)
        {
            book book = null;
            try
            {
                book = bookRepo.GetAny(id);
                return book;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
            return book;
        }

        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] book book)
        {
            try
            {
                bookRepo.Save(book);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] book book)
        {
            try
            {
                bookRepo.Update(book);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(book book)
        {
            try
            {
                bookRepo.Delete(book);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
