
/// <summary>
/// 面板类型
/// </summary>
public enum UIViewPanelType
{

    None = 0,
    /// <summary>
    /// 主界面面板
    /// </summary>
    Main,
    /// <summary>
    /// 第一级面板，比如全屏面板
    /// </summary>
    Frist,
    /// <summary>
    /// 第二级面板，在第一级面板上弹出的面板
    /// </summary>
    Second,
    /// <summary>
    /// 第二级面板，在第二级面板上弹出的面板，Tips等
    /// </summary>
    Third,
    /// <summary>
    /// Tips面板，比如轮流的提示
    /// </summary>
    Tips,
    /// <summary>
    /// 通知公告面板
    /// </summary>
    Notice,
    /// <summary>
    /// 提示确认取消面板
    /// </summary>
    Prompt,
    /// <summary>
    /// 全局遮罩面板，比如loading
    /// </summary>
    Mask,
}
