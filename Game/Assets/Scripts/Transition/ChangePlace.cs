using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ChangePlace : MonoBehaviour, IInteractable
{
    TransitionSystem transitionSystem;

    [SerializeField] bool notToTopDown;

    [SerializeField] public string nameScene;

    [SerializeField] public string positionName;

    public string currentSave;

    // private GameManager saveManagers;

    PlayerController playerController;

    private void Start()
    {
        transitionSystem = FindObjectOfType<TransitionSystem>();
        if(FindFirstObjectByType<PlayerController>())
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }
    }

    public void Interact(){
        ChangeScene();
    }

    private void ChangeScene()
    {
        PlayerController.state = PLAYER.INTERACT;

        if(!notToTopDown)
        {
            StartCoroutine(transitionSystem.Transition(nameScene, positionName));
            MarkLastScene();
        }
        else
        {
            StartCoroutine(transitionSystem.TransitionAsync(nameScene));
            MarkLastSceneThis();
        }
    }

    public void ToMenu()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        StartCoroutine(transitionSystem.TransitionAsync("TestMenu"));
        if(playerController != null) data.lastPlayerPosition = playerController.transform.position;
        GameManager.instance.SaveGame(data);
    }

    public void ToLastScene()
    {
        GameManager.instance.saveFileName = currentSave;
        GameManager.GameData data = GameManager.instance.LoadGame();
        StartCoroutine(transitionSystem.Transition(data.lastScene, data.lastPlayerPosition));
    }

    public void PhaseToLastScene()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        StartCoroutine(transitionSystem.Transition(data.lastScene, data.lastPlayerPosition));
    }

    public void ToSameScene()
    {
        StartCoroutine(transitionSystem.TransitionAsync(SceneManager.GetActiveScene().name));
    }

    private void MarkLastScene()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        data.lastScene = nameScene;
        if(playerController != null) data.lastPlayerPosition = playerController.transform.position;
        GameManager.instance.SaveGame(data);
    }

    private void MarkLastSceneThis()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        data.lastScene = SceneManager.GetActiveScene().name;
        if(playerController != null) data.lastPlayerPosition = playerController.transform.position;
        GameManager.instance.SaveGame(data);
    }

    // IEnumerator Transition()
    // {
    //     yield return StartCoroutine(fadeComponent.FadeIn());
    //     yield return new WaitForSeconds(1.5f);
    //     SceneManager.LoadScene(namePlace);
    // }

    // IEnumerator TransitionAsync()
    // {
    //     // yield return StartCoroutine(fadeComponent)
    // }
        
}