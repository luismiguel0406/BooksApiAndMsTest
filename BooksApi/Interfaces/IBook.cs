using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Task<bool> Delete(int id);
        Task<Book> Update(int id, Book book);
    }
}
