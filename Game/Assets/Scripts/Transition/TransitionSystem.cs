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

    private void Awake() 
    {
        playerController = FindObjectOfType<PlayerController>();
        fadeComponent = FindObjectOfType<FadeComponent>();
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

        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(place);
    }

    public IEnumerator TransitionAsync(string place, string positionName)
    {
        initialPositionName = positionName;
        PlayerController.state = PLAYER.INTERACT;
        
        yield return StartCoroutine(fadeComponent.FadeIn());
        yield return new WaitForSeconds(1.5f);
        Debug.Log(PlayerController.state);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(place);
        while(!asyncOperation.isDone)
        {
            runningAnimated.SetActive(true);
            yield return null;
        }
    }

    void OnSceneLoaded(Scene sceneLoaded, LoadSceneMode loadSceneMode)
    {
        GameObject inicialPos = GameObject.Find(initialPositionName); // Encontre a posição inicial pelo nome
        if (inicialPos != null)
        {
            Transform inicialPosTransform = inicialPos.transform;
            Vector3 inicialPosPlayer = inicialPosTransform.position;
            playerController.transform.position = inicialPosPlayer;
        }else{
            Debug.Log("Nao achou nenhum objeto com este nome");
        }

        runningAnimated.SetActive(false);
        StartCoroutine(fadeComponent.FadeOut());
    }
}
