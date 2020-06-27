namespace yamlist.Modules.Process
{
    public class ProcessResult
    {
        public static readonly ProcessResult OK = new ProcessResult(){ ExitCode = 0, ErrorOutput = string.Empty, StandardOutput = string.Empty };

        public int ExitCode { get; set; }
        public string ErrorOutput { get; set; }
        public string StandardOutput { get; set; }

        public bool HasErrorOutput()
        {
            return ExitCode != 0 && !string.IsNullOrEmpty(ErrorOutput);
        }

        public bool HasStandardOutput()
        {
            return ExitCode == 0 && !string.IsNullOrEmpty(StandardOutput);
        }

        public override string ToString()
        {
            return $"{nameof(ExitCode)}: {ExitCode}, {nameof(ErrorOutput)}: {ErrorOutput}, {nameof(StandardOutput)}: {StandardOutput}";
        }

        public static ProcessResult Error(string message)
        {
            return new ProcessResult()
            {
                ExitCode = 1,
                ErrorOutput = message
            };
        }
    }
}