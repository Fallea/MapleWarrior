using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigManager : TSingleton<ConfigManager>
{

    ConfigManager() { }

    private bool mLoaded = false;
    private System.Action loadCompleted = null;
    private List<string> configs = new List<string>();
    private Dictionary<string, System.Action<string>> callbacks = new Dictionary<string, System.Action<string>>();
    private int loadCount = 0;

    /// <summary>
    /// 加载配置表
    /// </summary>
    /// <param name="loadCompleted"></param>
    public void Load(System.Action loadCompleted)
    {
        this.loadCompleted = loadCompleted;
        configs.Clear();
        callbacks.Clear();

        //================================

		AddLoad("Adventure", ParseAdventure);
		AddLoad("Area", ParseArea);
		AddLoad("Box", ParseBox);
		AddLoad("Card", ParseCard);
		AddLoad("CardLevelExp", ParseCardLevelExp);
		AddLoad("Error", ParseError);
		AddLoad("Fight", ParseFight);
		AddLoad("LevelExp", ParseLevelExp);
		AddLoad("Map", ParseMap);
		AddLoad("Monster", ParseMonster);
		AddLoad("Skill", ParseSkill);

        //================================

        loadCount = 0;
        Load(configs[loadCount]);
    }

    private void AddLoad(string name, System.Action<string> callback)
    {
        configs.Add(name);
        callbacks.Add(name, callback);
    }

    private void Load(string name)
    {
        ResourceLoadManager.Instance.LoadAssetAsync<TextAsset>(string.Format("Config/{0}", name), ConfigLoadCompleted);
    }

    private void ConfigLoadCompleted(TextAsset textAsset)
    {
        Parse(configs[loadCount], textAsset.text);

        loadCount++;
        if (loadCount >= configs.Count)
        {
            if (this.loadCompleted != null)
            {
                this.loadCompleted.Invoke();
                this.loadCompleted = null;
                this.mLoaded = true;
                configs.Clear();
                callbacks.Clear();
            }
        }
        else
        {
            Load(configs[loadCount]);
        }
    }

    /// <summary>
    /// 配置表是否加载完成
    /// </summary>
    public bool loaded
    {
        get { return this.mLoaded; }
    }

    private void Parse(string name, string text)
    {
        if (callbacks.ContainsKey(name))
        {
            callbacks[name].Invoke(text);
        }
    }

    private string[] GetConfigParseList(string text)
    {
        string[] list = text.Split('\n');
        return list;
    }

	//================================================================

	private List<AdventureConfigData> adventureConfigList = new List<AdventureConfigData>();
	private Dictionary<int, AdventureConfigData> adventureConfigDict = new Dictionary<int, AdventureConfigData>();
	
	private void ParseAdventure(string text)
	{
		adventureConfigList.Clear();
		adventureConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			AdventureConfigData configData = new AdventureConfigData(list[i]);
			if (adventureConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseAdventure > " + configData.id);
			}
			else
			{
				adventureConfigDict.Add(configData.id, configData);
				adventureConfigList.Add(configData);
			}
		}
	}
	
	public AdventureConfigData GetAdventureConfigData(int key)
	{
		if (adventureConfigDict.ContainsKey(key))
		{
			return adventureConfigDict[key];
		}
		return null;
	}
	
	public List<AdventureConfigData> GetAdventureConfigList
	{
		get { return adventureConfigList; }
	}
		//================================================================

	private List<AreaConfigData> areaConfigList = new List<AreaConfigData>();
	private Dictionary<int, AreaConfigData> areaConfigDict = new Dictionary<int, AreaConfigData>();
	
	private void ParseArea(string text)
	{
		areaConfigList.Clear();
		areaConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			AreaConfigData configData = new AreaConfigData(list[i]);
			if (areaConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseArea > " + configData.id);
			}
			else
			{
				areaConfigDict.Add(configData.id, configData);
				areaConfigList.Add(configData);
			}
		}
	}
	
	public AreaConfigData GetAreaConfigData(int key)
	{
		if (areaConfigDict.ContainsKey(key))
		{
			return areaConfigDict[key];
		}
		return null;
	}
	
	public List<AreaConfigData> GetAreaConfigList
	{
		get { return areaConfigList; }
	}
		//================================================================

	private List<BoxConfigData> boxConfigList = new List<BoxConfigData>();
	private Dictionary<int, BoxConfigData> boxConfigDict = new Dictionary<int, BoxConfigData>();
	
	private void ParseBox(string text)
	{
		boxConfigList.Clear();
		boxConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			BoxConfigData configData = new BoxConfigData(list[i]);
			if (boxConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseBox > " + configData.id);
			}
			else
			{
				boxConfigDict.Add(configData.id, configData);
				boxConfigList.Add(configData);
			}
		}
	}
	
	public BoxConfigData GetBoxConfigData(int key)
	{
		if (boxConfigDict.ContainsKey(key))
		{
			return boxConfigDict[key];
		}
		return null;
	}
	
	public List<BoxConfigData> GetBoxConfigList
	{
		get { return boxConfigList; }
	}
		//================================================================

	private List<CardConfigData> cardConfigList = new List<CardConfigData>();
	private Dictionary<int, CardConfigData> cardConfigDict = new Dictionary<int, CardConfigData>();
	
	private void ParseCard(string text)
	{
		cardConfigList.Clear();
		cardConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			CardConfigData configData = new CardConfigData(list[i]);
			if (cardConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseCard > " + configData.id);
			}
			else
			{
				cardConfigDict.Add(configData.id, configData);
				cardConfigList.Add(configData);
			}
		}
	}
	
	public CardConfigData GetCardConfigData(int key)
	{
		if (cardConfigDict.ContainsKey(key))
		{
			return cardConfigDict[key];
		}
		return null;
	}
	
	public List<CardConfigData> GetCardConfigList
	{
		get { return cardConfigList; }
	}
		//================================================================

	private List<CardLevelExpConfigData> cardLevelExpConfigList = new List<CardLevelExpConfigData>();
	private Dictionary<int, CardLevelExpConfigData> cardLevelExpConfigDict = new Dictionary<int, CardLevelExpConfigData>();
	
	private void ParseCardLevelExp(string text)
	{
		cardLevelExpConfigList.Clear();
		cardLevelExpConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			CardLevelExpConfigData configData = new CardLevelExpConfigData(list[i]);
			if (cardLevelExpConfigDict.ContainsKey(configData.level))
			{
				Debug.LogWarning(">> ParseCardLevelExp > " + configData.level);
			}
			else
			{
				cardLevelExpConfigDict.Add(configData.level, configData);
				cardLevelExpConfigList.Add(configData);
			}
		}
	}
	
	public CardLevelExpConfigData GetCardLevelExpConfigData(int key)
	{
		if (cardLevelExpConfigDict.ContainsKey(key))
		{
			return cardLevelExpConfigDict[key];
		}
		return null;
	}
	
	public List<CardLevelExpConfigData> GetCardLevelExpConfigList
	{
		get { return cardLevelExpConfigList; }
	}
		//================================================================

	private List<ErrorConfigData> errorConfigList = new List<ErrorConfigData>();
	private Dictionary<int, ErrorConfigData> errorConfigDict = new Dictionary<int, ErrorConfigData>();
	
	private void ParseError(string text)
	{
		errorConfigList.Clear();
		errorConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			ErrorConfigData configData = new ErrorConfigData(list[i]);
			if (errorConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseError > " + configData.id);
			}
			else
			{
				errorConfigDict.Add(configData.id, configData);
				errorConfigList.Add(configData);
			}
		}
	}
	
	public ErrorConfigData GetErrorConfigData(int key)
	{
		if (errorConfigDict.ContainsKey(key))
		{
			return errorConfigDict[key];
		}
		return null;
	}
	
	public List<ErrorConfigData> GetErrorConfigList
	{
		get { return errorConfigList; }
	}
		//================================================================

	private List<FightConfigData> fightConfigList = new List<FightConfigData>();
	private Dictionary<int, FightConfigData> fightConfigDict = new Dictionary<int, FightConfigData>();
	
	private void ParseFight(string text)
	{
		fightConfigList.Clear();
		fightConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			FightConfigData configData = new FightConfigData(list[i]);
			if (fightConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseFight > " + configData.id);
			}
			else
			{
				fightConfigDict.Add(configData.id, configData);
				fightConfigList.Add(configData);
			}
		}
	}
	
	public FightConfigData GetFightConfigData(int key)
	{
		if (fightConfigDict.ContainsKey(key))
		{
			return fightConfigDict[key];
		}
		return null;
	}
	
	public List<FightConfigData> GetFightConfigList
	{
		get { return fightConfigList; }
	}
		//================================================================

	private List<LevelExpConfigData> levelExpConfigList = new List<LevelExpConfigData>();
	private Dictionary<int, LevelExpConfigData> levelExpConfigDict = new Dictionary<int, LevelExpConfigData>();
	
	private void ParseLevelExp(string text)
	{
		levelExpConfigList.Clear();
		levelExpConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			LevelExpConfigData configData = new LevelExpConfigData(list[i]);
			if (levelExpConfigDict.ContainsKey(configData.level))
			{
				Debug.LogWarning(">> ParseLevelExp > " + configData.level);
			}
			else
			{
				levelExpConfigDict.Add(configData.level, configData);
				levelExpConfigList.Add(configData);
			}
		}
	}
	
	public LevelExpConfigData GetLevelExpConfigData(int key)
	{
		if (levelExpConfigDict.ContainsKey(key))
		{
			return levelExpConfigDict[key];
		}
		return null;
	}
	
	public List<LevelExpConfigData> GetLevelExpConfigList
	{
		get { return levelExpConfigList; }
	}
		//================================================================

	private List<MapConfigData> mapConfigList = new List<MapConfigData>();
	private Dictionary<int, MapConfigData> mapConfigDict = new Dictionary<int, MapConfigData>();
	
	private void ParseMap(string text)
	{
		mapConfigList.Clear();
		mapConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			MapConfigData configData = new MapConfigData(list[i]);
			if (mapConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseMap > " + configData.id);
			}
			else
			{
				mapConfigDict.Add(configData.id, configData);
				mapConfigList.Add(configData);
			}
		}
	}
	
	public MapConfigData GetMapConfigData(int key)
	{
		if (mapConfigDict.ContainsKey(key))
		{
			return mapConfigDict[key];
		}
		return null;
	}
	
	public List<MapConfigData> GetMapConfigList
	{
		get { return mapConfigList; }
	}
		//================================================================

	private List<MonsterConfigData> monsterConfigList = new List<MonsterConfigData>();
	private Dictionary<int, MonsterConfigData> monsterConfigDict = new Dictionary<int, MonsterConfigData>();
	
	private void ParseMonster(string text)
	{
		monsterConfigList.Clear();
		monsterConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			MonsterConfigData configData = new MonsterConfigData(list[i]);
			if (monsterConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseMonster > " + configData.id);
			}
			else
			{
				monsterConfigDict.Add(configData.id, configData);
				monsterConfigList.Add(configData);
			}
		}
	}
	
	public MonsterConfigData GetMonsterConfigData(int key)
	{
		if (monsterConfigDict.ContainsKey(key))
		{
			return monsterConfigDict[key];
		}
		return null;
	}
	
	public List<MonsterConfigData> GetMonsterConfigList
	{
		get { return monsterConfigList; }
	}
		//================================================================

	private List<SkillConfigData> skillConfigList = new List<SkillConfigData>();
	private Dictionary<int, SkillConfigData> skillConfigDict = new Dictionary<int, SkillConfigData>();
	
	private void ParseSkill(string text)
	{
		skillConfigList.Clear();
		skillConfigDict.Clear();
		string[] list = GetConfigParseList(text);
		for (int i = 3; i < list.Length; i++)
		{
			if (string.IsNullOrEmpty(list[i]))
			{
				continue;
			}
			SkillConfigData configData = new SkillConfigData(list[i]);
			if (skillConfigDict.ContainsKey(configData.id))
			{
				Debug.LogWarning(">> ParseSkill > " + configData.id);
			}
			else
			{
				skillConfigDict.Add(configData.id, configData);
				skillConfigList.Add(configData);
			}
		}
	}
	
	public SkillConfigData GetSkillConfigData(int key)
	{
		if (skillConfigDict.ContainsKey(key))
		{
			return skillConfigDict[key];
		}
		return null;
	}
	
	public List<SkillConfigData> GetSkillConfigList
	{
		get { return skillConfigList; }
	}
	
}