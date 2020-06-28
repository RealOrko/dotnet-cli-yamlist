using System;
using System.IO;
using System.IO.Compression;
using SharpCompress.Common;
using SharpCompress.Readers;
using OperatingSystem = yamlist.Modules.Process.OperatingSystem;

namespace yamlist.Modules.IO.Compression
{
    public class Archive
    {
        public static void ExtractZip(string zipArchive, string destinationFolder)
        {
            ZipFile.ExtractToDirectory(zipArchive, destinationFolder, true);
        }

        public static void ExtractTar(string tarArchive, string destinationFolder)
        {
            using (Stream stream = File.OpenRead(tarArchive))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                    if (!reader.Entry.IsDirectory)
                    {
                        var opt = new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true,
                            WriteSymbolicLink =
                                (symbolicLink, actualPath) =>
                                {
                                    if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                                    {
                                        var symbolicLinkDirectory = Path.GetDirectoryName(symbolicLink);
                                        if (!Directory.Exists(symbolicLinkDirectory))
                                            Directory.CreateDirectory(symbolicLinkDirectory);

                                        var result = new Process.Process()
                                            .SetWorkingDirectory(symbolicLinkDirectory)
                                            .SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH"))
                                            .Execute("ln", "-s", actualPath, symbolicLink);

                                        if (result.HasErrorOutput()) System.Console.WriteLine(result.ErrorOutput);
                                    }
                                    else
                                    {
                                        System.Console.WriteLine(
                                            $"Could not write symlink {symbolicLink} -> {actualPath}, for more information please see https://github.com/dotnet/runtime/issues/24271");
                                    }
                                }
                        };

                        reader.WriteEntryToDirectory(destinationFolder, opt);
                    }
            }
        }
    }
}