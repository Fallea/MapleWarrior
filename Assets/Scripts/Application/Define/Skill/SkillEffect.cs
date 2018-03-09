using System.Collections.Generic;

/// <summary>
/// 技能上的效果，一个技能可能有多个效果，比如伤害效果、闪避效果（添加BUFF的）
/// </summary>
public class SkillEffect
{
    public SkillEffectType type;
    public SkillTargetType targetType;
    public SkillTargetPosition targetPosition;
    public int targetNum = 0;
    public int targetNumMax = 0;
    public int probability = 0;
    public List<SkillEffectBuff> buffs = new List<SkillEffectBuff>();



    public SkillEffect()
    {

    }
}