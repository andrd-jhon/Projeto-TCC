using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSystem : MonoBehaviour
{
    // [SerializeField] private GameObject dialogSystem;
    // [SerializeField] private GameObject equipmentSystem;
    // [SerializeField] private GameObject playerTopDown;

    [SerializeField] private List<GameObject> gameObjectsTopDown; 
    [SerializeField] private List<GameObject> gameObjectsPlat; 

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
        
        
        

        if(toToDown)
        {
            GameObject inicialPos = GameObject.Find(initialPositionName); // Encontre a posição inicial pelo nome

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
                playerController = FindObjectOfType<PlayerController>();
                playerController.transform.position = initialPositionPlace;
                inicialPos = null;
            }
        }
        else if(SceneManager.GetActiveScene().name != "TestMenu")
        {
            InstantiateObjectsPlat();
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

    private void InstantiateObjectsPlat()
    {
        foreach (GameObject obj in gameObjectsPlat)
        {
            Instantiate(obj);
        }
    }

    // private void CarrySelectedEquipments()
    // {
    //     GameObject playerPlat = GameObject.Find("Player");
    //     GameManager.GameData data = GameManager.instance.LoadGame();
    //     if(data.selectedEquipments != null)
    //     {
    //         foreach(string equipmentName in data.selectedEquipments)
    //         {
    //             EquipmentData currentEquipment = TakeEquipment(equipmentName);
    //             Instantiate(currentEquipment.equipmentGameObject, playerPlat.transform);
    //         }
    //     }
    // }

    // private EquipmentData TakeEquipment (string equipmentName)
    // {
    //     foreach (EquipmentData equipment in EquipmentManager.allEquipments)
    //     {
    //         if(equipment.equipmentName == equipmentName)
    //         {
    //             return equipment;
    //         }
    //     }
    //     return null;
    // }
}
