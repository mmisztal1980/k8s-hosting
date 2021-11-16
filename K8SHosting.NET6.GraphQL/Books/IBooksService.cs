namespace K8SHosting.NET6.GraphQL.Books
{
    public interface IBooksService
    {
        IEnumerable<Book> GetBooks();
    }
}