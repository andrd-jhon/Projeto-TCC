using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ChangePlace : MonoBehaviour, IInteractable
{
    TransitionSystem transitionSystem;

    [SerializeField] bool notToTopDown;

    [SerializeField] string nameScene;

    [SerializeField] string positionName;

    private GameManager saveManager;

    PlayerController playerController;

    private void Awake()
    {
        transitionSystem = FindObjectOfType<TransitionSystem>();
        saveManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Interact(){
        ChangeScene();
    }

    void ChangeScene()
    {
         PlayerController.state = PLAYER.INTERACT;
        if(!notToTopDown)
        {
            StartCoroutine(transitionSystem.Transition(nameScene, positionName));
            MarkLastScene();
        }
        else{
            StartCoroutine(transitionSystem.TransitionAsync(nameScene));
        }
    }

    public void ToMenu()
    {
        GameManager.GameData data = saveManager.LoadGame();
        StartCoroutine(transitionSystem.TransitionAsync("TestMenu"));
        data.lastPlayerPosition = playerController.transform.position;
        saveManager.SaveGame(data);
    }

    public void ToLastScene()
    {
        GameManager.GameData data = saveManager.LoadGame();
        StartCoroutine(transitionSystem.Transition(data.lastScene, data.lastPlayerPosition));
    }

    private void MarkLastScene()
    {
        GameManager.GameData data = saveManager.LoadGame();
        data.lastScene = nameScene;
        saveManager.SaveGame(data);
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