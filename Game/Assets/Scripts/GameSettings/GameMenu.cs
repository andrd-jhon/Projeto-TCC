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

    public void CreateNewGame()
    {
        string nameSave = inputNameGame.text;
        
        if(string.IsNullOrEmpty(nameSave)){
            Debug.Log("PREENCHA O CAMPO");
            return;
        }

        GameManager.instance.saveFileName = nameSave;

        GameManager.GameData newGameData = new GameManager.GameData();
        GameManager.instance.SaveGame(newGameData);
        changeToNewGame.Interact();
        Debug.Log("NOVO ARQUIVO CRIADO COM NOME: " + nameSave);
    }

    public void LoadGame()
    {

    }

    public void CreationMenuAppear()
    {
        newGameScreen.SetActive(true);
    }

    public void CreationMenuDisappear()
    {
        newGameScreen.SetActive(false);
        inputNameGame.text = "";
    }

    public void OpenSaveGamesMenu()
    {
        savesGameScreen.SetActive(true);
    }

    public void CloseSaveGamesMenu()
    {
        savesGameScreen.SetActive(false);
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
            saveGameText.text = file.Name;

            string pathCurrent = Application.persistentDataPath + "/" + file.Name;
            if(File.Exists(pathCurrent)){
                changePlace.currentSave = Path.GetFileNameWithoutExtension(file.Name);
            }
        }
    }
}
