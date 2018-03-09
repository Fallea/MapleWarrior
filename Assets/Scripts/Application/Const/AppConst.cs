using UnityEngine;
using System.Collections;

public class AppConst {


    /// <summary>
    /// 调试模式，用于内部测试
    /// </summary>
    public const bool DebugMode = false;

    /// <summary>
    /// 帧频
    /// </summary>
    public const int FrameRate = 30;

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public const string AppName = "App";

    /// <summary>
    /// App版本
    /// </summary>
    public System.Version Version = new System.Version(1, 0, 0);


    public const string WebUrl = "http://localhost:6688/";      //测试更新地址

    public static int SocketPort = 0;                           //Socket服务器端口
    public static string SocketAddress = string.Empty;          //Socket服务器地址

}
