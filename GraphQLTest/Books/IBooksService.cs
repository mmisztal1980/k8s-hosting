namespace GraphQLTest.Books
{
    public interface IBooksService
    {
        IEnumerable<Book> GetBooks();
    }
}