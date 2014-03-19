using System;
using System.IO;
using System.Windows.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VSSDK.Tools.VsIdeTesting;
using WebEditor = Microsoft.Web.Editor.WebEditor;

namespace CssSorter.Test.Shared
{
    public static class VSHost
    {
        static readonly string BaseDirectory = Path.GetDirectoryName(typeof(VSHost).Assembly.Location);
        static readonly string FixtureDirectory = Path.Combine(BaseDirectory, "fixtures", "Visual Studio");

        static string TestCaseDirectory { get { return Path.Combine(Path.GetTempPath(), "Css Sorter Test Files", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")); } }
        static DTE DTE { get { return VsIdeTestHostContext.Dte; } }
        static System.IServiceProvider ServiceProvider { get { return VsIdeTestHostContext.ServiceProvider; } }

        static T GetService<T>(Type idType) { return (T)ServiceProvider.GetService(idType); }

        static Dispatcher Dispatcher
        {
            get { return Dispatcher.FromThread(WebEditor.UIThread); }
        }

        ///<summary>Gets the currently active project (as reported by the Solution Explorer), if any.</summary>
        static Project GetActiveProject()
        {
            try
            {
                Array activeSolutionProjects = VSHost.DTE.ActiveSolutionProjects as Array;

                if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
                    return activeSolutionProjects.GetValue(0) as Project;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error getting the active project" + ex);
            }

            return null;
        }

        public static void ReadyingSolution()
        {
            // Waste of electrons
            var directory = TestCaseDirectory;
            var solution = (Solution2)DTE.Solution;
            solution.Create(directory, "DependencyCreationTests");
            var template = solution.GetProjectTemplate("EmptyWebApplicationProject40.zip", "CSharp");
            solution.AddFromTemplate(template, Path.Combine(directory, "WebAppProject"), "WebAppProject.csproj");
        }
    }
}