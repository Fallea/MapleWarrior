using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class TextureTool : MonoBehaviour
{

    [MenuItem("Tools/Texture/Handle Sprite(By Folder Name)")]
    private static void HandleSpriteByFolderName()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (!System.IO.Directory.Exists(path))
        {
            Debug.LogError("请选择需要处理的文件夹！");
            return;
        }
        if (path.EndsWith("/"))
        {
            path = path.TrimEnd('/');
        }

        DirectoryInfo dir = new DirectoryInfo(path);

        ModifyTexture(TextureImporterFormat.RGBA32, dir.Name);
    }

    [MenuItem("Tools/Texture/Handle Sprite(Single)")]
    private static void HandleSpriteSingle()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (System.IO.Directory.Exists(path))
        {
            Debug.LogError("请选择需要处理的文件！");
            return;
        }

        FileInfo info = new FileInfo(path);

        DirectoryInfo dir = new DirectoryInfo(info.DirectoryName);

        ModifyTexture(TextureImporterFormat.RGBA32, dir.Name);
    }


    //[MenuItem("Tools/Texture/Handle Sprite(Public)")]
    private static void HandleSpritePublic()
    {
        ModifyTexture(TextureImporterFormat.RGBA32, "Public");
    }

    //[MenuItem("Tools/Texture/Handle Sprite(Card)")]
    private static void HandleSpriteCard()
    {
        ModifyTexture(TextureImporterFormat.RGBA32, "Card");
    }

    static void ModifyTexture(TextureImporterFormat importerFormat, string tag)
    {
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (selection.Length > 0)
        {
            foreach (Object obj in selection)
            {
                string texturePath = AssetDatabase.GetAssetPath(obj);
                if (texturePath.EndsWith(".png"))
                {
                    TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;
                    if (textureImporter != null)
                    {
                        textureImporter.textureFormat = importerFormat;
                        textureImporter.spritePackingTag = tag;
                        textureImporter.mipmapEnabled = false;
                        textureImporter.textureType = TextureImporterType.Sprite;
                        AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
                    }
                    else
                    {
                        Debug.Log(texturePath);
                    }
                }
            }
            AssetDatabase.SaveAssets();
        }
    }

    //================================================================

    [MenuItem("Tools/Texture/Careate Sprite Atlas(By Folder)")]
    static private void MakeAtlas()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (!System.IO.Directory.Exists(path))
        {
            Debug.LogError("请选择需要处理的文件夹！");
            return;
        }
        if (path.EndsWith("/"))
        {
            path = path.TrimEnd('/');
        }

        DirectoryInfo dir = new DirectoryInfo(path);

        string uiPath = Application.dataPath + "/Resources/UI/";
        if (!Directory.Exists(uiPath))
        {
            Directory.CreateDirectory(uiPath);
        }

        string spriteAtlasPath = uiPath + dir.Name + "Atlas.prefab";
        spriteAtlasPath = spriteAtlasPath.Substring(spriteAtlasPath.IndexOf("Assets"));


        GameObject go = new GameObject(dir.Name);
        UISpriteAtlas atlas = go.AddComponent<UISpriteAtlas>();

        FileInfo[] dirInfos = dir.GetFiles("*.png", SearchOption.AllDirectories);

        foreach (FileInfo pngFile in dirInfos)
        {
            string allPath = pngFile.FullName;
            string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            atlas.names.Add(sprite.name);
            atlas.sprites.Add(sprite);
        }
        PrefabUtility.CreatePrefab(spriteAtlasPath, go);
        GameObject.DestroyImmediate(go);
    }
}
