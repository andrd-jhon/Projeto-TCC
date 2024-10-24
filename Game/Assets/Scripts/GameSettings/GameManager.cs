using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string saveFileName;

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
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName + ".json", json); //AQUI QUE CRIA O ARQUIVO
        Debug.Log("JOGO SALVO");
    }

    public GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/" + saveFileName + ".json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.Log("Nenhum arquivo encontrado com nome :" + saveFileName);
            return null;
            // return new GameData();
        }
    }

    public void Test(){
        GameManager.GameData data = LoadGame();
        if(data != null){
            SaveGame(data);
        }
        else{
            Debug.Log("Falha no carregamento para o arquivo: " + saveFileName);
        }
    }
}
