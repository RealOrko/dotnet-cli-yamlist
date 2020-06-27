using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace yamlist.Modules.Process
{
    public class Process
    {
        private readonly Dictionary<string, string> _environmentVariables = new Dictionary<string, string>();
        private TimeSpan _waitForExit = TimeSpan.FromMinutes(5);
        private string _workingDirectory;

        public Process()
        {
            _environmentVariables["PATH"] = Environment.GetEnvironmentVariable("PATH");
        }

        public Process SetTimeout(TimeSpan waitForExit)
        {
            _waitForExit = waitForExit;
            return this;
        }

        public Process ClearEnvironmentVariable(string name)
        {
            _environmentVariables.Remove(name);
            return this;
        }

        public Process SetEnvironmentVariable(string name, string value)
        {
            _environmentVariables[name] = value;
            return this;
        }

        public Process SetWorkingDirectory(string workingDirectory)
        {
            _workingDirectory = workingDirectory;
            return this;
        }

        public virtual ProcessResult Execute(string command, params string[] args)
        {
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = string.Join(" ", args);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            if (!string.IsNullOrEmpty(_workingDirectory)) startInfo.WorkingDirectory = _workingDirectory;

            foreach (var environmentVariable in _environmentVariables)
                startInfo.EnvironmentVariables[environmentVariable.Key] = environmentVariable.Value;

            int exitCode;
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;

                try
                {
                    process.Start();

                    while (!process.StandardOutput.EndOfStream) stdout.AppendLine(process.StandardOutput.ReadLine());

                    while (!process.StandardError.EndOfStream) stderr.AppendLine(process.StandardError.ReadLine());
                }
                finally
                {
                    var wasError = false;
                    try
                    {
                        if (!process.WaitForExit((int) _waitForExit.TotalMilliseconds)) process.Kill();
                    }
                    catch (InvalidOperationException err)
                    {
                        wasError = true;
                    }

                    if (!wasError)
                        exitCode = process.ExitCode;
                    else
                        exitCode = -1;
                }
            }

            return new ProcessResult
            {
                ErrorOutput = stderr.ToString(),
                ExitCode = exitCode,
                StandardOutput = stdout.ToString()
            };
        }
    }
}