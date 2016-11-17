
public class SvUtil
{
    static System.Random random = new System.Random();

    /// <summary>
    /// 获取随机数，包含最小值，不包含最大值，最大值可以和最小值一样
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetRandom(int min, int max)
    {
        if (max < min) { return min; }
        int index = random.Next(min, max);
        return index;
    }
}
