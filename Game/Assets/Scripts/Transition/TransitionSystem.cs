using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSystem : MonoBehaviour
{
    FadeComponent fadeComponent;
    public GameObject runningAnimated;
    Image runAnim;
    PlayerController playerController;

    private string initialPositionName;
    private Vector2 initialPositionPlace; 

    private GameManager saveManager;
    
    private bool transitionByName;

    private void Awake() 
    {
        playerController = FindObjectOfType<PlayerController>();
        fadeComponent = FindObjectOfType<FadeComponent>();
        saveManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // runAnim = runningAnimated.GetComponent<Image>();
    }

    private void Start() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }

    public IEnumerator Transition(string place, string positionName)
    {
        initialPositionName = positionName;
        PlayerController.state = PLAYER.INTERACT;
        transitionByName = true;
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(place);
    }

    public IEnumerator Transition(string place, Vector2 playerPosition)
    {
        initialPositionPlace = playerPosition;
        PlayerController.state = PLAYER.INTERACT;
        transitionByName = false;
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(place);
    }

    public IEnumerator TransitionAsync(string place)
    {
        PlayerController.state = PLAYER.INTERACT;
        
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(place);
        while(!asyncOperation.isDone)
        {
            runningAnimated.SetActive(true);
            yield return null;
        }
    }

    void OnSceneLoaded(Scene sceneLoaded, LoadSceneMode loadSceneMode)
    {
        GameManager.GameData data = saveManager.LoadGame();
        

        GameObject inicialPos = GameObject.Find(initialPositionName); // Encontre a posição inicial pelo nome
        if (inicialPos != null && transitionByName)
        {
            Vector2 inicialPosPlayer = inicialPos.transform.position;
            playerController = FindObjectOfType<PlayerController>();
            playerController.transform.position = inicialPosPlayer;
            data.lastPlayerPosition = inicialPosPlayer;
            saveManager.SaveGame(data);
        }else{
            Debug.Log("Nao achou nenhum objeto com este nome");
        }

        if(initialPositionPlace != null && !transitionByName){
            playerController = FindObjectOfType<PlayerController>();
            playerController.transform.position = initialPositionPlace;
        }


        runningAnimated.SetActive(false);
        StartCoroutine(fadeComponent.FadeOut());
    }
}
