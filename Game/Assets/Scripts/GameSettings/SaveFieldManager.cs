using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveFieldManager : MonoBehaviour
{
    public GameObject gameObjectParent;
    private GameMenu gameMenu;

    private void Awake() 
    {
        gameMenu = GameObject.Find("MenuManager").GetComponent<GameMenu>();
    }

    public void OpenConfirmDelete()
    {
        gameMenu.OpenConfirmDelete(gameObjectParent);
    }
}
