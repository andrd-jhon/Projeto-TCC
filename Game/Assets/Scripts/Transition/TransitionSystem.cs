using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSystem : MonoBehaviour
{
    // [SerializeField] private GameObject dialogSystem;
    // [SerializeField] private GameObject equipmentSystem;
    // [SerializeField] private GameObject playerTopDown;

    [SerializeField] private List<GameObject> gameObjectsTopDown; 

    private bool toToDown;

    FadeComponent fadeComponent;
    public GameObject runningAnimated;
    Image runAnim;
    PlayerController playerController;

    private string initialPositionName;
    private Vector2 initialPositionPlace; 
    
    private bool transitionByName;

    private void Awake() 
    {
        playerController = FindObjectOfType<PlayerController>();
        fadeComponent = FindObjectOfType<FadeComponent>();
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
        toToDown = true;
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(place);
    }

    public IEnumerator Transition(string place, Vector2 playerPosition)
    {
        initialPositionPlace = playerPosition;
        PlayerController.state = PLAYER.INTERACT;
        transitionByName = false;
        toToDown = true;
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(place);
    }

    public IEnumerator TransitionAsync(string place)
    {
        PlayerController.state = PLAYER.INTERACT;
        toToDown = false;
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
        GameManager.GameData data = GameManager.instance.LoadGame();
        

        GameObject inicialPos = GameObject.Find(initialPositionName); // Encontre a posição inicial pelo nome

        if(toToDown)
        {
            InstantiateObjectsTopDown();

            if (inicialPos != null && transitionByName)
            {
                Vector2 inicialPosPlayer = inicialPos.transform.position;
                playerController = FindObjectOfType<PlayerController>();
                playerController.transform.position = inicialPosPlayer;
                data.lastPlayerPosition = inicialPosPlayer;
                GameManager.instance.SaveGame(data);
                inicialPos = null;
            }
            else if(initialPositionPlace != null && !transitionByName)
            {
                Debug.Log("ESTA SENDO EXECUTADO");
                playerController = FindObjectOfType<PlayerController>();
                playerController.transform.position = initialPositionPlace;
                inicialPos = null;
            }
        }

        
        runningAnimated.SetActive(false);
        StartCoroutine(fadeComponent.FadeOut());
    }

    private void InstantiateObjectsTopDown()
    {
        foreach (GameObject obj in gameObjectsTopDown)
        {
            Instantiate(obj);
        }
    }
}
