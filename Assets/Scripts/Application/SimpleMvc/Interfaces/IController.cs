using System;

namespace SimpleMvc
{
    public interface IController
    {
        void Register(IView view, string[] commandNames);

        void Execute(IMessage message);

        void Remove(IView view, string[] commandNames);

        bool Has(string messageName);
    }
}