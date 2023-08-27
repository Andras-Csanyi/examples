namespace gql;

public class MutationType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Name(OperationTypeNames.Mutation)
            .Description("Mutation types to record data in the system");

        descriptor
            .Field("addDocument")
            .Type<NonNullType<DocumentType>>()
            .Description("Recording new documents in the system")
            .Argument("newDocument", arg => arg.Type<NonNullType<DocumentInputType>>())
            .ResolveWith<DocumentResolver>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}