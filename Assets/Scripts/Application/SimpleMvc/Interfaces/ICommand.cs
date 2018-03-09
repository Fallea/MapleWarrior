
namespace SimpleMvc
{
    public interface ICommand
    {
        void Execute(IMessage message);
    }

}