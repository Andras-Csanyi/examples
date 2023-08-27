namespace gql;

public class DocumentInputType : InputObjectType<Document>
{
    protected override void Configure(IInputObjectTypeDescriptor<Document> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<IdType>();

        descriptor
            .Field(f => f.Name)
            .Description("Name of the entity")
            .Type<StringType>();

        descriptor
            .Field(f => f.Description)
            .Description("Description of the entity")
            .Type<StringType>();

    }
}