using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ParseUtil
{

    private delegate T ParseFromString<T>(string strToParse);
    private static readonly char[] configSpliter = new char[] { ',' };

    private static T[] ParseStringToArray<T>(string str, bool dereplicate, ParseFromString<T> parseDel)
    {
        if (string.IsNullOrEmpty(str))
            return null;

        string[] items = str.Split(configSpliter, StringSplitOptions.RemoveEmptyEntries);
        if ((null == items) || (0 >= items.Length))
            return null;

        List<T> result = new List<T>();
        for (int index = 0; index < items.Length; index++)
        {
            if (items[index].Equals("NULL"))
                continue;

            T value = parseDel(items[index]);

            if (null == value)
                continue;

            if (dereplicate && result.Contains(value))
                continue;

            result.Add(value);
        }

        return result.ToArray();
    }

    public static T[] ParseStringToEnumArray<T>(string str, bool dereplicate) where T : struct
    {
        return ParseStringToArray(str, dereplicate, (strToParse) =>
        {
            return (T)Enum.Parse(typeof(T), strToParse);
        });
    }

    public static Type[] ParseStringToTypeArray(string str, bool dereplicate)
    {
        return ParseStringToArray(str, dereplicate, (strToParse) =>
        {
            return Type.GetType(strToParse);
        });
    }

    public static Vector3 ParseStringToVector3(string str)
    {
        float[] values = ParseStringToArray(str, false, (strToParse) =>
        {
            return float.Parse(strToParse);
        });

        Vector3 result = Vector3.zero;
        if (values.Length > 0) result.x = values[0];
        if (values.Length > 1) result.y = values[1];
        if (values.Length > 2) result.z = values[2];

        return result;
    }
}
