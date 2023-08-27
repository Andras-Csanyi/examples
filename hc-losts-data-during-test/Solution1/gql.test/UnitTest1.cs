namespace gql.test;

using System.Collections.ObjectModel;
using HotChocolate.Execution;
using Xunit.Abstractions;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        ArgumentNullException.ThrowIfNull(testOutputHelper);
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Query()
    {
        // Arrange
        string queryString = @"{ document { id name description } }";

        // Act
        IQueryResult result = await TestServices.ExecuteRequestAsync(q => q.SetQuery(queryString));
        _testOutputHelper.WriteLine(result.ToJson());
    }

    [Fact]
    public async Task Mutation()
    {
        // Arrange
        string mutationString = @"
             mutation asd($input: DocumentInput!) {
                addDocument(newDocument: $input) { id name description }
            }
        ";
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { "name", "name value" },
            { "description", "description value" },
        };

        // Act
        IQueryResult result = await TestServices.ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        });
        _testOutputHelper.WriteLine(result.ToJson());
    }
}