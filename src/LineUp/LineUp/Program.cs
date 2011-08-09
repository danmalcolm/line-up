using LineUp.Commands;
using LineUp.Configuration;

namespace LineUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var launcher = new CommandLauncher(GetConfiguration(), args);
            launcher.Execute();
            
        }

        private static Config GetConfiguration()
        {
            return DemoConfigBuilder.Build();
        }
    }
}
