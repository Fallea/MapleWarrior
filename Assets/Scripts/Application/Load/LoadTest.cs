using UnityEngine;
using System;
using System.Collections.Generic;


public class LoadTest : MonoBehaviour
{
    void Start()
    {
        ResourceLoadManager.Instance.LoadAssetAsync<GameObject>("Resources/Prefabs/Cube.assetbundle", "Cube", (obj)=> 
        {
            //GameObject go = GameObject.Instantiate(obj);
        });

        ResourceLoadManager.Instance.LoadAssetAsync<GameObject>("Resources/Prefabs/Cube.assetbundle", "Cube", (obj) =>
        {
            //GameObject go = GameObject.Instantiate(obj);
        });

        ResourceLoadManager.Instance.LoadAssetAsync<GameObject>("Resources/Effect/Effect.assetbundle", "Effect", (obj) =>
        {
            //GameObject go = GameObject.Instantiate(obj);
        });
    }
}
