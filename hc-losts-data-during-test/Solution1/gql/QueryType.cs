namespace gql;

public class QueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Name(OperationTypeNames.Query)
            .Description("Queries to get data from the system.");

        descriptor
            .Field("document")
            .Description("Document entity")
            .Type<ListType<DocumentType>>()
            .ResolveWith<DocumentResolver>(r => r.GetDocuments());
    }
}