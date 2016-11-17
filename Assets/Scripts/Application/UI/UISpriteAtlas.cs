using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISpriteAtlas : MonoBehaviour
{
    public List<string> names = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

    private Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    private void Start()
    {
        
    }

	public void Init()
	{
		Debug.Log(">> Atlas Init > " + this.gameObject.name);
		
		for (int i = 0; i < names.Count; i++)
		{
			spriteDic.Add(names[i], sprites[i]);
		}
	}

    public Sprite GetSprite(string name)
    {
        if (spriteDic.ContainsKey(name))
        {
            return spriteDic[name];
        }
        return null;
    }
}
