using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerCollider : MonoBehaviour
{
    public UnityEvent enterCollider;
    private CircleCollider2D circleCollider;

    [SerializeField] private bool desactive;

    private void Start() {
        circleCollider = this.gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collided){
        if(collided.CompareTag("Player")){
            enterCollider.Invoke();
            if(desactive) circleCollider.enabled = false;
            // if(desactive) this.gameObject.SetActive(false);
            // if(destroy) Destroy(this.gameObject);
        }   
    }
}
