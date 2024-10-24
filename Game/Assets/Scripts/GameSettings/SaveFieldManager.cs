using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveFieldManager : MonoBehaviour
{
    public GameObject gameObjectParent;

    public void DeleteSave()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dirInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if(Path.GetFileNameWithoutExtension(file.Name) == gameObjectParent.name)
            {
                file.Delete();
                Destroy(gameObjectParent);
                return;
            }
        }
    }
}
