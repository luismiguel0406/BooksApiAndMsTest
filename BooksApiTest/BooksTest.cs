using BooksApi.Models;
using BooksApi.Services;
using Microsoft.Extensions.Configuration;

namespace BooksApiTest
{

    [TestClass]
    public class BooksTest
    {
        private IConfiguration configuration;

        public BooksTest()
        {
            //Simulo mis datos iniciales de configuracion,aquellos que estan en Appsetting.json
            var basicConfig = new Dictionary<string, string?>
            {
                {"baseUrlApi", "https://fakerestapi.azurewebsites.net/api/v1"}
            };
            var configurationBuilder = new ConfigurationBuilder().AddInMemoryCollection(basicConfig);
            configuration = configurationBuilder.Build();
        }
        [TestMethod]
        public void GetAll_NotNullReturn()
        {
            BookService bookService = new(configuration);

            var result = bookService.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById_NotNullReturn() {

            int id = 2;
            BookService bookService = new(configuration);
            var result = bookService.GetById(id);
            Assert.IsNotNull(result); 
        }
        [TestMethod]
        public void AddBook_NotNullReturn()
        {

            BookService bookService = new(configuration);
            var newBook = new Book
            {
                Id = new Random().Next(1,100),
                Title = "Book 1",
                Description = "Quod at takimata magna sed at lorem sit esse amet.",
                PageCount = 100,
                Excerpt = "Aliquyam nisl diam vel delenit est sea vulputate elitr amet consetetur kasd gubergren.",
                PublishDate = "2024-11-25T14:38:05.7819261+00:00"
            };

            var result = bookService.Add(newBook);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void DeleteBook_TrueReturn() { 

            int id = 2; 
            BookService bookService = new(configuration);
            var result = bookService.Delete(id);
            Assert.IsTrue(result.Result);
        
        }
        [TestMethod]
        public void UpdateBook_NotNullReturn() {

            BookService bookService = new(configuration);
            int id = 2;
            var book = new Book
            {
                Id = new Random().Next(1, 100),
                Title = "Book 1",
                Description = "Quod at takimata magna sed at lorem sit esse amet.",
                PageCount = 100,
                Excerpt = "Aliquyam nisl diam vel delenit est sea vulputate elitr amet consetetur kasd gubergren.",
                PublishDate = "2024-11-25T14:38:05.7819261+00:00"
            };

            var result = bookService.Update(id,book);
            Assert.IsNotNull(result);
        }
    }
}