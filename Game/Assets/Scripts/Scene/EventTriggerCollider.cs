using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerCollider : MonoBehaviour
{
    public UnityEvent enterCollider;

    private void OnTriggerEnter2D(Collider2D collided){
        if(collided.CompareTag("Player")){
            enterCollider.Invoke();
        }   
    }
}
