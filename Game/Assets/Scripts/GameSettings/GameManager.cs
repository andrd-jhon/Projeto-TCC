using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public class GameData
    {
        public List<string> destroyedObjects = new List<string>();
        public Vector2 lastPlayerPosition;
        public string lastScene;
    }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //AQUI QUE CRIA O ARQUIVO
        Debug.Log("JOGO SALVO");
    }

    public GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.Log("Nenhum arquivo encontrado");
            return null;
            // return new GameData();
        }
    }

    public void Test(){
        GameManager.GameData data = LoadGame();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        SaveGame(data);
    }
}
