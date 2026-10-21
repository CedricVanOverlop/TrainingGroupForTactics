using System.Reflection;
using NetArchTest.Rules;

namespace TGFT.ArchitectureTests;

/// <summary>
/// Clean Architecture rules: dependencies only point inwards
/// (Api -> Infrastructure -> Application -> Domain).
/// </summary>
public class LayerDependencyTests
{
    private static readonly Assembly DomainAssembly = typeof(Domain.AssemblyMarker).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(Application.AssemblyMarker).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyMarker).Assembly;

    private const string DomainNamespace = "TGFT.Domain";
    private const string ApplicationNamespace = "TGFT.Application";
    private const string InfrastructureNamespace = "TGFT.Infrastructure";
    private const string ApiNamespace = "TGFT.Api";

    private const string EfCoreNamespace = "Microsoft.EntityFrameworkCore";
    private const string AspNetCoreNamespace = "Microsoft.AspNetCore";

    [Fact]
    public void Domain_ShouldNotDependOnOtherLayers()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace, ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Domain_ShouldNotDependOnFrameworks()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(EfCoreNamespace, AspNetCoreNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Application_ShouldNotDependOnInfrastructureOrApi()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(InfrastructureNamespace, ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Application_ShouldNotDependOnEfCore()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(EfCoreNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOnApi()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    // Lists the offending types in the failure message instead of a bare "false".
    private static void AssertSuccessful(TestResult result)
    {
        var failingTypes = result.FailingTypeNames ?? [];
        Assert.True(result.IsSuccessful,
            $"Forbidden dependency found in: {string.Join(", ", failingTypes)}");
    }
}
