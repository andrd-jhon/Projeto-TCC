using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerCollider : MonoBehaviour
{
    public UnityEvent enterCollider;
    private CircleCollider2D circleCollider;

    [SerializeField] private bool desactive;

    // private GameManager saveManager;

    // private void Awake() {
    //     saveManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    // }

    private void Start() {
        circleCollider = this.gameObject.GetComponent<CircleCollider2D>();
        GameManager.GameData data = GameManager.instance.LoadGame();
        if(data.destroyedObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collided){
        if(collided.CompareTag("Player")){
            enterCollider.Invoke();
            if(desactive){
                circleCollider.enabled = false;
                DestroyPermanently();
            } 
            // if(desactive) this.gameObject.SetActive(false);
            // if(destroy) Destroy(this.gameObject);
        }   
    }

    public void DestroyPermanently()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        data.destroyedObjects.Add(gameObject.name);
        GameManager.instance.SaveGame(data);
    }
}
