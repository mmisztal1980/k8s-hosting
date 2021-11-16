using GraphQLTest.Books;

namespace GraphQLTest.Graph
{
    [GraphQLDescription("Books from the HotChocolate documentation: https://chillicream.com/docs/hotchocolate/defining-a-schema/queries")]
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
