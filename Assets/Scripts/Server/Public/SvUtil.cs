
public class SvUtil
{
    static System.Random random = new System.Random();

    /// <summary>
    /// ��ȡ�������������Сֵ�����������ֵ�����ֵ���Ժ���Сֵһ��
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
