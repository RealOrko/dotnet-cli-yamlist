using System.Diagnostics;
using System.Reflection;

namespace yamlist.Modules.Versioning
{
    public class Info
    {
        public static string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return string.Format(
                $"{fileVersionInfo.ProductMajorPart}.{fileVersionInfo.ProductMinorPart}.{fileVersionInfo.ProductBuildPart}");
        }
    }
}