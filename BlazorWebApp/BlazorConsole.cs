using System.Text;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;

namespace BlazorWebApp
{
    public class BlazorConsole : IConsole
    {
        public event Action StateChanged;
        public MarkupString Text => (MarkupString)Builder.ToString();   
        private StringBuilder Builder { get; } = new();
        public Queue<string> Inputs { get; } = new();
        public void WriteLine(string message)
        {
            Builder.AppendLine(message);
            Builder.AppendLine("<br/>");
            StateChanged?.Invoke();
        }

        public string ReadLine()
        {
            return Inputs.Dequeue();
        }

        public bool HasInput() => Inputs.Any();

        public void Clear()
        {
            Builder.Clear();
            StateChanged?.Invoke();
        }
    }
}
