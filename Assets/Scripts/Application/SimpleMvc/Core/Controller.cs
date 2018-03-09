using System;
using System.Collections.Generic;

namespace SimpleMvc
{
    public class CmdView
    {
        private string mCommand;
        private List<IView> views = new List<IView>();

        public CmdView(string command)
        {
            this.mCommand = command;
        }

        public string Command
        {
            get { return this.mCommand; }
        }

        public void Add(IView view)
        {
            if (!views.Contains(view))
            {
                views.Add(view);
            }
        }

        public void Remove(IView view)
        {
            int index = views.IndexOf(view);
            if (index > -1)
            {
                views.RemoveAt(index);
            }
        }

        public bool IsEmpty
        {
            get { return views.Count < 1; }
        }

        public void Execute(IMessage note)
        {
            for (int i = 0; i < views.Count; i++)
            {
                views[i].OnMessage(note);
            }
        }
    }

    public class Controller : IController
    {
        protected IDictionary<string, CmdView> mCmdViewMap;

        protected static volatile IController m_instance;
        protected readonly object m_syncRoot = new object();
        protected static readonly object m_staticSyncRoot = new object();

        protected Controller()
        {
            InitializeController();
        }

        static Controller()
        {
        }

        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new Controller();
                    }
                }
                return m_instance;
            }
        }

        protected virtual void InitializeController()
        {
            mCmdViewMap = new Dictionary<string, CmdView>();
        }

        public virtual void Execute(IMessage note)
        {
            lock (m_syncRoot)
            {
                if (mCmdViewMap.ContainsKey(note.Name))
                {
                    mCmdViewMap[note.Name].Execute(note);
                }
            }
        }

        public virtual void Register(IView view, string[] commandNames)
        {
            lock (m_syncRoot)
            {
                string command;
                for (int i = 0; i < commandNames.Length; i++)
                {
                    command = commandNames[i];
                    if (mCmdViewMap.ContainsKey(command))
                    {
                        mCmdViewMap[command].Add(view);
                    }
                    else
                    {
                        CmdView cv = new CmdView(command);
                        mCmdViewMap.Add(command, cv);
                        cv.Add(view);
                    }
                }
            }
        }

        public virtual bool Has(string commandName)
        {
            lock (m_syncRoot)
            {
                return mCmdViewMap.ContainsKey(commandName);
            }
        }

        public virtual void Remove(IView view, string[] commandNames)
        {
            lock (m_syncRoot)
            {
                string command;
                for (int i = 0; i < commandNames.Length; i++)
                {
                    command = commandNames[i];
                    if (mCmdViewMap.ContainsKey(command))
                    {
                        mCmdViewMap[command].Remove(view);
                        if (mCmdViewMap[command].IsEmpty)
                        {
                            mCmdViewMap.Remove(command);
                        }
                    }
                }
            }
        }

    }

}


