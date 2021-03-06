﻿using Meziantou.Analyzer.Rules;
using Xunit;
using TestHelper;

namespace Meziantou.Analyzer.Test.Rules
{
    public sealed class UseEventArgsEmptyAnalyzerTests
    {
        private static ProjectBuilder CreateProjectBuilder()
        {
            return new ProjectBuilder()
                .WithAnalyzer<UseEventArgsEmptyAnalyzer>();
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldReportAsync()
        {
            const string SourceCode = @"
class TypeName
{
    public void Test()
    {
        [||]new System.EventArgs();
    }
}";
            await CreateProjectBuilder()
                .WithSourceCode(SourceCode)
                .ValidateAsync();
        }
    }
}
