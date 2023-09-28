namespace RedditSharp.API.Helper
{
    public interface IFileWriter
    {
        Task WriteLineAsync(string message);
        void WriteLine(string message);
    }
}
