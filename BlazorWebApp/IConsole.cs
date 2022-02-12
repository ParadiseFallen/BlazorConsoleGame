namespace BlazorWebApp
{
    public interface IConsole
    {
        public bool HasInput();
        public string ReadLine();
        public void Clear();
        public void WriteLine(string message);
    }
}
