using BooksApi.Interfaces;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBook _bookService;
        public BooksController(IBook bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            try
            {
                var books = await _bookService.GetAll();             

                if (books is null) return NotFound();
                return Ok(books);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            try
            {
                var book = await _bookService.GetById(id);
              
                if(book is null) return NotFound();
                return Ok(book);
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Book>> Add(Book book)
        {
            try
            {
                var result = await _bookService.Add(book);
                return Ok(result);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _bookService.Delete(id);
                if (!response) throw new Exception("Task failed");
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Update(int id, Book book)
        {
            try
            {
                var result = await _bookService.Update(id, book);             
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
