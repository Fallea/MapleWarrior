using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;

public class ConfigTableTool : MonoBehaviour
{

    private static string managerAddConfigText = "\t\tAddLoad(\"{0}\", Parse{1});";


    [MenuItem("Tools/ConfigTable/Generate ConfigData Code")]
    private static void GenerateConfigDataCode()
    {

        string configFileDirPath = Application.dataPath + "/Resources/Config/";
        string configDataFileDirPath = Application.dataPath + "/Scripts/Application/Config/";
        string managerFilePath = Application.dataPath + "/Scripts/AppManager/ConfigManager.cs";

        string configDataTemplatePath = Application.dataPath + "/Editor/ConfigTable/ConfigDataTemplate.txt";
        string managerClassTemplatePath = Application.dataPath + "/Editor/ConfigTable/ManagerClassTemplate.txt";
        string managerParseConfigTemplatePath = Application.dataPath + "/Editor/ConfigTable/ManagerParseConfigTemplate.txt";


        if (!Directory.Exists(configFileDirPath))
        {
            Debug.LogError("configFileDirPath not exist");
            return;
        }

        if (!Directory.Exists(configDataFileDirPath))
        {
            Directory.CreateDirectory(configDataFileDirPath);
        }

        string configDataTemplateText = File.ReadAllText(configDataTemplatePath, Encoding.UTF8);
        string managerClassTemplateText = File.ReadAllText(managerClassTemplatePath, Encoding.UTF8);
        string managerParseConfigTemplateText = File.ReadAllText(managerParseConfigTemplatePath, Encoding.UTF8);

        List<string> fileNameList = new List<string>();
        List<string> keyIdList = new List<string>();

        DirectoryInfo configFileDirInfo = new DirectoryInfo(configFileDirPath);
        FileInfo[] configFiles = configFileDirInfo.GetFiles();
        for (int i = 0; i < configFiles.Length; i++)
        {
            FileInfo fileInfo = configFiles[i];
            if (fileInfo.Name.EndsWith(".txt"))
            {
                Debug.Log(fileInfo.Name);
                string fileName = fileInfo.Name.Replace(".txt", "").Trim();
                fileNameList.Add(fileName);
                string configDataFilePath = configDataFileDirPath + fileName + "ConfigData.cs";

                string classTxt = configDataTemplateText;
                classTxt = classTxt.Replace("{0}", fileName);

                string configDataFileTxt = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);

                string[] configDataFileTxtArr = configDataFileTxt.Split('\n');

                string[] commentArr = configDataFileTxtArr[0].Split('\t');
                string[] attributeArr = configDataFileTxtArr[1].Split('\t');
                string[] typeArr = configDataFileTxtArr[2].Split('\t');

                keyIdList.Add(attributeArr[0].Trim());

                StringBuilder attributeBuilder = new StringBuilder("");
                StringBuilder parseBuilder = new StringBuilder("");

                for (int j = 0; j < attributeArr.Length; j++)
                {
                    string commentStr = commentArr[j].Trim();
                    string attributeStr = attributeArr[j].Trim();
                    string typeStr = typeArr[j].Trim();

                    HandleArrtibute(attributeBuilder, commentStr, typeStr, attributeStr);
                    parseBuilder.Append(GetParse(typeStr, attributeStr));
                    parseBuilder.Append(System.Environment.NewLine);
                }

                classTxt = classTxt.Replace("{1}", attributeBuilder.ToString());
                classTxt = classTxt.Replace("{2}", parseBuilder.ToString());

                File.WriteAllText(configDataFilePath, classTxt, Encoding.UTF8);
            }
        }

        StringBuilder addConfigBuilder = new StringBuilder("");
        StringBuilder parseConfigBuilder = new StringBuilder("");

        for (int i = 0; i < fileNameList.Count; i++)
        {
            addConfigBuilder.Append(string.Format(managerAddConfigText, fileNameList[i], fileNameList[i]));
            addConfigBuilder.Append(System.Environment.NewLine);

            string temp = managerParseConfigTemplateText.Replace("{0}", fileNameList[i]);
            temp = temp.Replace("{1}", FristToLower(fileNameList[i]));
            temp = temp.Replace("{2}", keyIdList[i]);

            parseConfigBuilder.Append(temp);
        }

        string managerClassTxt = managerClassTemplateText.Replace("{1}", addConfigBuilder.ToString());
        managerClassTxt = managerClassTxt.Replace("{2}", parseConfigBuilder.ToString());

        File.WriteAllText(managerFilePath, managerClassTxt, Encoding.UTF8);

        Debug.Log("Generate ConfigData Code Finsh.");
        AssetDatabase.Refresh();
    }

    private static void HandleArrtibute(StringBuilder builder, string commentStr, string typeStr, string attributeStr)
    {
        builder.Append("	/// <summary>");
        builder.Append(System.Environment.NewLine);
        builder.Append("	/// ");
        builder.Append(commentStr);
        builder.Append(System.Environment.NewLine);
        builder.Append("	/// <summary>");
        builder.Append(System.Environment.NewLine);

        builder.Append("	public " + typeStr + " " + attributeStr + " { get; private set; }");
        builder.Append(System.Environment.NewLine);
    }

    private static string GetParse(string typeStr, string attributeStr)
    {
        if (typeStr.Equals("int"))
        {
            return string.Format("		this.{0} = int.Parse(arr[index++]);", attributeStr);
        }
        else if (typeStr.Equals("string"))
        {
            return string.Format("		this.{0} = arr[index++];", attributeStr);
        }
        else if (typeStr.Equals("float"))
        {
            return string.Format("		this.{0} = float.Parse(arr[index++]);", attributeStr);
        }
        else
        {
            return string.Format("		this.{0} = ({1})int.Parse(arr[index++]);", attributeStr, typeStr);
        }
    }

    private static string FristToLower(string str)
    {
        return str.Substring(0, 1).ToLower() + str.Substring(1);

    }


}
