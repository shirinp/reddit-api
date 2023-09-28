namespace RedditSharp.API.Helper
{
    public class FileWriter : IFileWriter
    {
        private readonly IConfiguration _configuration;
        private readonly string _filePath;
        private readonly object _lock = new object();

        public FileWriter(IConfiguration configuration) 
        { 
            _configuration = configuration;
            _filePath = _configuration["FilePath"];
        }

        public async Task WriteLineAsync(string message)
        {
            using StreamWriter writer = new StreamWriter(_filePath, true);
            await writer.WriteLineAsync(message);
        }

        public void WriteLine(string message) 
        {
            lock(_lock)
            {
                using StreamWriter writer = new StreamWriter(_filePath, true);
                writer.WriteLine(message);
            }
        }
    }
}
