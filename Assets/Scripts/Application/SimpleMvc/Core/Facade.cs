
namespace SimpleMvc
{
    public class Facade
    {
        protected IController mController;

        protected Facade()
        {
            InitFramework();
        }

        protected virtual void InitFramework()
        {
            if (mController != null) return;
            mController = Controller.Instance;
        }

        public virtual bool Has(string commandName)
        {
            return mController.Has(commandName);
        }


        public void SendMessage(string message, object body = null)
        {
            mController.Execute(new Message(message, body));
        }


    }
}

