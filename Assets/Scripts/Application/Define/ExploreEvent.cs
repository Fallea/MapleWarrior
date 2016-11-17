

public enum ExploreEventType
{
    None = 0,
    /// <summary>
    /// 怪物
    /// </summary>
    Monster,
    /// <summary>
    /// 宝箱
    /// </summary>
    Box,
    /// <summary>
    /// 奇遇
    /// </summary>
    Adventure
}

/// <summary>
/// 探索事件基类
/// </summary>
public class ExploreEvent
{
    private ExploreEventType mType = ExploreEventType.None;
    /// <summary>
    /// 出发时间
    /// </summary>
    public float triggerTime = 0;

    protected ExploreEvent()
    {
    }

    public ExploreEvent(ExploreEventType type)
    {
        this.mType = type;
    }

    /// <summary>
    /// 探索事件类型
    /// </summary>
    public ExploreEventType type
    {
        get { return mType; }
    }
}
