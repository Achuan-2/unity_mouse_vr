using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileWrite : MonoBehaviour
{
    public void WriteToText(string fileName, string header,string content)
    {
        //Path of  the file
        #if UNITY_EDITOR //如果是在编辑器环境下
               string path = Application.dataPath + "/Log/"+ fileName;
        #else//在打包出来的环境下
               string path = System.IO.Directory.GetCurrentDirectory() + "/Log/"+ fileName;
        #endif
        
        // Create File if it doesn't exist
        if (!File.Exists(path)) {
            File.WriteAllText(path, header);
        }
        // if file exist, write content to it
        File.AppendAllText(path,content);
    }
}