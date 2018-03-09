
namespace SimpleMvc
{
    public class AppFacade : Facade
    {
        private static AppFacade mInstance;

        public AppFacade() : base()
        {
        }

        public static AppFacade Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new AppFacade();
                }
                return mInstance;
            }
        }

        override protected void InitFramework()
        { 
            base.InitFramework();
            //RegisterCommand(NotiConst.START_UP, typeof(StartUpCommand));
        }

        /// <summary>
        /// 启动框架
        /// </summary>
        public void StartUp()
        {
            //SendMessageCommand(NotiConst.START_UP);
           // RemoveMultiCommand(NotiConst.START_UP);
        }
    }
}