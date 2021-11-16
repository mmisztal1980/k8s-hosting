using HotChocolate;
using HotChocolate.Types;
using K8SHosting.NET6.GraphQL.Books;

namespace K8SHosting.NET6.GraphQL.Graph
{    
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor
                .Field(f => f.Title)
                .Type<StringType>();

            descriptor
                .Field(f => f.Author)
                .Type<StringType>();
        }
    }
}
