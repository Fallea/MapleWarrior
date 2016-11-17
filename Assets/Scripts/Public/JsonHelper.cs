using UnityEngine;
using System.Collections;
using JsonFx.Json;

public class JsonHelper
{
    /// <summary>
    /// 将对象序列化为JSON格式
    /// </summary>
    public static string Serialize(object obj)
    {
        string json = JsonWriter.Serialize(obj);
        return json;
    }

    /// <summary>
    /// 解析JSON字符串生成对象实体
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">json字符串(eg.{"ID":"1","Name":"名称"})</param>
    /// <returns>对象实体</returns>
    public static T Deserialize<T>(string json) where T : class
    {
        T result = JsonReader.Deserialize<T>(json);
        return result;
    }
}
