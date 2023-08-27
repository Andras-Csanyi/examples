using gql;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<IDocumentService, DocumentService>()
    .AddGraphQLServer()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddType<DocumentType>()
    .AddType<DocumentInputType>()
    .RegisterService<IDocumentService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();