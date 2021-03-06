﻿using Meziantou.Analyzer.Rules;
using Xunit;
using TestHelper;

namespace Meziantou.Analyzer.Test.Rules
{
    public sealed class DontUseDangerousThreadingMethodsAnalyzerTests
    {
        private static ProjectBuilder CreateProjectBuilder()
        {
            return new ProjectBuilder()
                .WithAnalyzer<DontUseDangerousThreadingMethodsAnalyzer>();
        }

        [Theory]
        [InlineData("Thread.CurrentThread.Abort()")]
        [InlineData("Thread.CurrentThread.Suspend()")]
        [InlineData("Thread.CurrentThread.Resume()")]
        public async System.Threading.Tasks.Task ReportDiagnosticAsync(string text)
        {
            await CreateProjectBuilder()

                  .WithSourceCode(@"using System.Threading;
public class Test
{
    public void A()
    {
        [||]" + text + @";
    }
}")
                  .ValidateAsync();
        }
    }
}
