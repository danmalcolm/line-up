namespace LineUp.Commands
{
    public interface ICommand
    {
        void Execute(CommandContext context);
    }
}