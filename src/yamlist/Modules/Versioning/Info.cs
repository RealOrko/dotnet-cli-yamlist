using System;
using System.Diagnostics;
using System.Reflection;

namespace yamlist.Modules.Versioning
{
    public class Info
    {
        public static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return String.Format($"{fileVersionInfo.ProductMajorPart}.{fileVersionInfo.ProductMinorPart}.{fileVersionInfo.ProductBuildPart}");
        }
    }
}