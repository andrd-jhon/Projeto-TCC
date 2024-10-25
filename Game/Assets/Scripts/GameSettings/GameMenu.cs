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
    [SerializeField] public GameObject confirmDeleteScreen;

    [SerializeField] private Transform savesScreen;
    private GameObject saveGame;
    // [SerializeField] private GameObject NewGameScreen;
    [SerializeField] private TMP_InputField inputNameGame;
    [SerializeField] private TMP_Text alertNameGame;

    [SerializeField] ChangePlace changeToNewGame;

    private GameObject saveToDestroy;
    

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
        alertNameGame.text = "";
    }

    public void CloseSaveGamesMenu()
    {
        savesGameScreen.SetActive(false);
    }

    public void CloseDenialMessage()
    {
        denialMessageScreen.SetActive(false);
    }

    public void CloseConfirmDelete()
    {
        confirmDeleteScreen.SetActive(false);
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

    public void OpenConfirmDelete(GameObject save)
    {
        confirmDeleteScreen.SetActive(true);
        saveToDestroy = save;
        TMP_Text textConfirm = confirmDeleteScreen.GetComponentInChildren<TMP_Text>();
        textConfirm.text = "Tem certeza que deseja excluir o salvamento " + save.name + "?";
    }

    public void OpenSaveGamesMenuFromDenial()
    {
        CloseDenialMessage();
        CloseCreationMenu();
        OpenSaveGamesMenu();
    }

    #endregion

    public void DeleteSave()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dirInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if(Path.GetFileNameWithoutExtension(file.Name) == saveToDestroy.name)
            {
                file.Delete();
                Destroy(saveToDestroy);
                saveToDestroy = null;
                CloseConfirmDelete();
                Debug.Log("Arquivo deletado");
                return;
            }
        }
    }

    public void CreateNewGame()
    {
        string nameSave = inputNameGame.text;
        DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dirInfo.GetFiles();
        
        if(string.IsNullOrEmpty(nameSave))
        {
            alertNameGame.text = "Insira um nome válido";
            return;
        }

        foreach (FileInfo file in files)
        {
            if(Path.GetFileNameWithoutExtension(file.Name) == nameSave)
            {
                alertNameGame.text = "Já existe um salvamento com este nome";
                return;
            }
        }

        if(files.Length >= 3)
        {
            denialMessageScreen.SetActive(true);
            alertNameGame.text = "";
            return;
        }
        else
        {
            
            alertNameGame.text = "";
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
