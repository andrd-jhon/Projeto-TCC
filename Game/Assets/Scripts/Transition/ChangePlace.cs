using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlace : MonoBehaviour, IInteractable
{
    TransitionSystem transitionSystem;

    [SerializeField] bool isAsync;

    [SerializeField] string namePlace;

    private void Awake()
    {
        transitionSystem = FindObjectOfType<TransitionSystem>();
    }

    public void Interact(){
        ChangeScene();
    }

    void ChangeScene()
    {
         PlayerController.state = PLAYER.INTERACT;

        if(!isAsync)
        {
            StartCoroutine(transitionSystem.Transition(namePlace));
        }
        else{
            StartCoroutine(transitionSystem.TransitionAsync(namePlace));
        }
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