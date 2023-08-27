namespace gql.test;

using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddType<DocumentInputType>()
            .AddType<DocumentType>()
            .RegisterService<IDocumentService>()
            .Services
            .AddScoped<IDocumentService, DocumentService>()
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<IQueryResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        await using var scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        await using var result = await Executor.ExecuteAsync(request, cancellationToken);

        IQueryResult res = result.ExpectQueryResult();

        return res;
    }
}