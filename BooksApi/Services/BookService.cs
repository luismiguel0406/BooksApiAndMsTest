using BooksApi.Interfaces;
using BooksApi.Models;
using System.Text;
using System.Text.Json;

namespace BooksApi.Services
{
    public class BookService : IBook
    {
        static readonly HttpClient _httpClient = new();
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl = string.Empty;

        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
           _baseUrl = _configuration.GetValue<string>("baseUrlApi")!;
        }
        public async Task<IEnumerable<Book>> GetAll()
        {
              var response =  await _httpClient.GetAsync($"{_baseUrl}/books");
              response.EnsureSuccessStatusCode();
              var result = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
              return result!;
        }
        public async Task<Book> GetById(int id)
        {
                var response = await _httpClient.GetAsync($"{_baseUrl}/books/{id}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<Book>();
                return result!;
        }

        public async Task<Book> Add(Book book)
        {
            string bookSerialized = JsonSerializer.Serialize(book);
            var body = new StringContent(bookSerialized, Encoding.UTF8, "application/json");

            var response =  await _httpClient.PostAsync($"{_baseUrl}/books", body );
            var newBook = await response.Content.ReadFromJsonAsync<Book>();
            var result = newBook;
            return result!;
        }

        public async Task<bool> Delete(int id)
        {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/books/{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode) return true;
                return false;
        }

        public async Task<Book> Update(int id, Book book)
        {       
            string bookSerialized = JsonSerializer.Serialize(book);
            var body = new StringContent(bookSerialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/books/{id}", body);
            var updatedBook = await response.Content.ReadFromJsonAsync<Book>();
            var result = updatedBook;
            return result!;
        }
    }
}
