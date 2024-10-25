using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject newGameScreen;
    [SerializeField] private GameObject savesGameScreen;
    [SerializeField] private GameObject denialMessageScreen;

    [SerializeField] private Transform savesScreen;
    private GameObject saveGame;
    // [SerializeField] private GameObject NewGameScreen;
    [SerializeField] private TMP_InputField inputNameGame;

    [SerializeField] ChangePlace changeToNewGame;

    private string[] deleteButtons;

    private void Awake() {
        saveGame = Resources.Load<GameObject>("Prefabs/Save");
    }

    private void Start()
    {
        InstantationSaves();
    }

    #region Closes

    public void CloseCreationMenu()
    {
        newGameScreen.SetActive(false);
        inputNameGame.text = "";
    }

    public void CloseSaveGamesMenu()
    {
        savesGameScreen.SetActive(false);
    }

    public void CloseDenialMessage()
    {
        denialMessageScreen.SetActive(false);
    }

    #endregion

    #region Opens

    public void OpenCreationMenu()
    {
        newGameScreen.SetActive(true);
    }    

    public void OpenSaveGamesMenu()
    {
        savesGameScreen.SetActive(true);
    }

    public void OpenDenialMessage()
    {
        denialMessageScreen.SetActive(true);
    }

    public void OpenSaveGamesMenuFromDenial()
    {
        CloseDenialMessage();
        CloseCreationMenu();
        OpenSaveGamesMenu();
    }

    #endregion

    public void CreateNewGame()
    {
        string nameSave = inputNameGame.text;
        DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dirInfo.GetFiles();
        
        if(string.IsNullOrEmpty(nameSave))
        {
            Debug.Log("PREENCHA O CAMPO");
            return;
        }
        else if(files.Length >= 3)
        {
            Debug.Log("Quantidade máxima de salvamentos atingida. Exclua para poder criar mais");
            denialMessageScreen.SetActive(true);
            return;
        }
        else
        {
            GameManager.instance.saveFileName = nameSave;
            GameManager.GameData newGameData = new GameManager.GameData();
            GameManager.instance.SaveGame(newGameData);
            changeToNewGame.Interact();
            Debug.Log("NOVO ARQUIVO CRIADO COM NOME: " + nameSave);
        }
    }

    public void InstantationSaves()
    {
        string path = Application.persistentDataPath;
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] files = dirInfo.GetFiles();
        foreach(FileInfo file in files)
        {
            GameObject instantiadedSave = Instantiate(saveGame, savesScreen);
            instantiadedSave.name = Path.GetFileNameWithoutExtension(file.Name);
            GameObject saveGameButton = instantiadedSave.transform.Find("SaveGame").gameObject;
            TMP_Text saveGameText = saveGameButton.GetComponentInChildren<TMP_Text>();
            ChangePlace changePlace = instantiadedSave.GetComponent<ChangePlace>();
            saveGameText.text = Path.GetFileNameWithoutExtension(file.Name);

            string pathCurrent = Application.persistentDataPath + "/" + file.Name;
            if(File.Exists(pathCurrent)){
                changePlace.currentSave = Path.GetFileNameWithoutExtension(file.Name);
            }
        }
    }
}
