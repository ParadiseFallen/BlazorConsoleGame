using System.Diagnostics;

namespace BlazorWebApp
{
    public class Game
    {
        public IConsole Console { get; }
        public Game( IConsole console)
        {
            Console = console;
        }


        public async Task Run()
        {
			Random random = new();

			const string menu = @"
  Quick Draw
  Face your opponent and wait for the signal. Once the
  signal is given, shoot your opponent by pressing [space]
  before they shoot you. It's all about your reaction time.
  Choose Your Opponent:
  [1] Easy....1000 milliseconds
  [2] Medium...500 milliseconds
  [3] Hard.....250 milliseconds
  [4] Harder...125 milliseconds
  [escape] give up";

			const string wait = @"
  Quick Draw
              _O                          O_            
             |/|_          wait          _|\|           
             /\                            /\           
            /  |                          |  \          
  ------------------------------------------------------";

			const string fire = @"
  Quick Draw
                         ********                       
                         * FIRE *                       
              _O         ********         O_            
             |/|_                        _|\|           
             /\          spacebar          /\           
            /  |                          |  \          
  ------------------------------------------------------";

			const string loseTooSlow = @"
  Quick Draw
                                        > ╗__O          
           //            Too Slow           / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

			const string loseTooFast = @"
  Quick Draw
                         Too Fast       > ╗__O          
           //           You Missed          / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

			const string win = @"
  Quick Draw
            O__╔ <                                      
           / \                               \\         
             /\          You Win          /\__\O        
            /  |                          /             
  ------------------------------------------------------";


			try
			{
				while (true)
				{
					Console.Clear();
					Console.WriteLine(menu);
					TimeSpan? requiredReactionTime = TimeSpan.FromMilliseconds(1000);
					Console.Clear();
					TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 35000));
					Console.WriteLine(wait);
					Stopwatch stopwatch = new();
					stopwatch.Restart();
					bool tooFast = false;
					while (stopwatch.Elapsed < signal && !tooFast)
                    {
                        if (Console.HasInput() && Console.ReadLine() == "space")
                        {
                            tooFast = true;
                        }
						await Task.Yield();
					}
					Console.Clear();
					Console.WriteLine(fire);
					stopwatch.Restart();
					bool tooSlow = true;
					TimeSpan reactionTime = default;
					while (!tooFast && stopwatch.Elapsed < requiredReactionTime && tooSlow)
					{
						if (Console.HasInput() && Console.ReadLine() == "space")
						{
							tooSlow = false;
							reactionTime = stopwatch.Elapsed;
						}
						await Task.Yield();
					}
					Console.Clear();
					Console.WriteLine(
						tooFast ? loseTooFast :
						tooSlow ? loseTooSlow :
						$"{win}{Environment.NewLine}  Reaction Time: {reactionTime.TotalMilliseconds} milliseconds");
					Console.WriteLine("  Play Again [enter] or quit [escape]?");
                    while (!Console.HasInput())
                    {
						await Task.Yield();
					}
					switch (Console.ReadLine())
					{
						case "enter": break;
						case "escape": return;
						default: break;
					}
				}
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.ToString());
            }
			finally
			{
				Console.Clear();
				Console.WriteLine("Quick Draw was closed.");
			}
		}
    }
}
