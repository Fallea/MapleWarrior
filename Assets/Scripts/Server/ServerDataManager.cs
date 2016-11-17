using UnityEngine;
using System.IO;
using System.Text;
using JsonFx.Json;

/// <summary>
/// 服务器数据管理，相对于数据库方面的管理类
/// </summary>
public class ServerDataManager : TSingleton<ServerDataManager>
{
    private ServerDataManager() { }

    private SvDataPlayer player = null;

    private string localPlayerPath;

    /// <summary>
    /// 读取玩家数据，服务器启动时，加载到内存中
    /// </summary>
    public void LoadPlayerData()
    {
        localPlayerPath = Assets.RuntimeAssetsPath + "player.txt";
        if (File.Exists(localPlayerPath))
        {
            string temp = File.ReadAllText(localPlayerPath, Encoding.Default);
            player = JsonReader.Deserialize<SvDataPlayer>(temp);
        }
        if (player == null)
        {
            player = GetInitPlayer();
        }
        Debug.Log(">> Server > player name = " + player.name);
    }

    /// <summary>
    /// 存储玩家数据，服务器关闭时，存储所有玩家数据
    /// </summary>
    public void Save()
    {
        if (player != null)
        {
            File.WriteAllText(localPlayerPath, JsonWriter.Serialize(player), Encoding.Default);
            Debug.Log(">> Server > save player");
        }
    }

    /// <summary>
    /// 根据玩家ID获取玩家数据，省略账号验证这一部分，由于单机，故只用了一个全局对象来处理
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SvDataPlayer GetSvDataPlayer(int id)
    {
        if (player == null)
        {
            player = GetInitPlayer();
        }
        return player;
    }

    /// <summary>
    /// 存储指定玩家数据
    /// </summary>
    /// <param name="svDataPlayer"></param>
    public void Save(SvDataPlayer svDataPlayer)
    {
        if (svDataPlayer != null)
        {
            File.WriteAllText(localPlayerPath, JsonWriter.Serialize(svDataPlayer), Encoding.Default);
            Debug.Log(">> Server > save svPlayer");
        }
    }

    /// <summary>
    /// 获取初始玩家数据对象
    /// </summary>
    /// <returns></returns>
    private SvDataPlayer GetInitPlayer()
    {
        SvDataPlayer svDataPlayer = new SvDataPlayer();
        return svDataPlayer;
    }

}