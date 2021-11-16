namespace K8SHosting.NET6.GraphQL.Books
{
    public class BooksService : IBooksService
    {
        public IEnumerable<Book> GetBooks()
        {
            return new Book[]
            {
                new Book("C# in Depth", "Jon Skeet"),
                new Book("C# 9.0 in a Nutshell", "Joseph Albahari"),
                new Book("Learning Dapr", "Yaron Schnider")
            };
        }
    }
}
