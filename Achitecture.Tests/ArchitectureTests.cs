namespace Achitecture.Tests
{
    using NetArchTest.Rules;

    public class ArchitectureTests
    {
        private const string DomainAssembly = "Domain";
        private const string ApplicationAssembly = "Application";
        private const string InfrastructureAssembly = "Infrastructure";

        [Test]
        public void Domain_ShouldReferenceOtherProjects_ReturnsTrue()
        {
            var assembly = typeof(Kanban.Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationAssembly,
                InfrastructureAssembly,
            };

            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }
    }
}