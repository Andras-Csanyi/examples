namespace gql;

using HotChocolate.Resolvers;

public class DocumentResolver
{
    public async Task<List<Document>> GetDocuments()
    {
        return new List<Document>
        {
            new Document { Id = 10, Name = "name 10", Description = "10 desc" },
            new Document { Id = 20, Name = "name 20", Description = "20 desc" },
        };
    }

    public async Task<Document> AddAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        return new Document { Id = 23, Name = "recorded name", Description = " recorded desc" };
    }
}