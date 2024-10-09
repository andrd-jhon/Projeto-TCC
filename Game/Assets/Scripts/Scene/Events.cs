using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public UnityEvent teste;
    private void OnTriggerEnter2D(Collider2D collided) {
        if(collided.CompareTag("Player")){
            Debug.Log("pao");
            teste.Invoke();
        }
    }
}
