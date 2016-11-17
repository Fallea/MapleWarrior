using System;

public class SvCardIDGenerator : TSingleton<SvCardIDGenerator>
{
    SvCardIDGenerator() { }

    private long lastTime;
    private long nowTime;
    private long count;

    // 毫秒数 + 12位

    public long NewID()
    {
        nowTime = DateTime.UtcNow.Ticks;

        if (nowTime > lastTime)
        {
            lastTime = nowTime;
            count = 0;
        }

        count++;

        if (count > 4000)
        {
            lastTime += 1;
            count = 0;
        }

        return (lastTime << 12) | count;
    }

}