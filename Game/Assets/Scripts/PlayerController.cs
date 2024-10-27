using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PLAYER { FREE, INTERACT }

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    DialogSystem dialogSystem;
    Dialog dialog;
    FadeComponent fadeComponent;
    public GameObject collidedOBJ;

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    public static bool playerIsClose;
    bool canMove;
    
    public static PLAYER state;

    [SerializeField] private AudioClip passos; 
    
    private void Awake() 
    {
        dialog = FindObjectOfType<Dialog>();
        dialogSystem = FindObjectOfType<DialogSystem>();
        fadeComponent = FindObjectOfType<FadeComponent>();
    }
    
    private void Start() 
    {
        // DontDestroyOnLoad(this.gameObject);
        // SceneManager.sceneLoaded += OnSceneLoaded;
        state = PLAYER.FREE;
    }

    void Update()
    {
        IsFree();

        switch (state)
        {
            case PLAYER.INTERACT:
                rb.velocity = new Vector2(0,0);
                animator.SetBool("isMoving", false);
                break;
            case PLAYER.FREE:
                Interaction();
                Move();
                break;
        }
    }

    private void Move(){
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

    if (movement.x != 0 || movement.y != 0){
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("isMoving", true);
        AudioSource.PlayClipAtPoint(passos, Camera.main.transform.position, 0.7f);
    }else{
        animator.SetBool("isMoving", false);
    }
    //transform.position = transform.position + movement * speed * Time.deltaTime;
    rb.velocity = new Vector2(movement.x, movement.y) * speed;
    }


    void OnTriggerEnter2D(Collider2D collided) {
        if(collided.CompareTag("Interactable")){
            collidedOBJ = collided.gameObject;
            playerIsClose = true;
            // Debug.Log("O player colidiu com o" + collidedOBJ.name);
        }
        
    }
    void OnTriggerExit2D(Collider2D collided) {
        if(collided.CompareTag("Interactable")){
            collidedOBJ = null;
            playerIsClose = false;
            // Debug.Log("O player saiu de colisao com o" + collided.gameObject.name);
        }   
    }

    
    void IsFree(){
        if(dialogSystem.state != STATE.DISABLED){
            state = PLAYER.INTERACT;
        }
    }

    void Interaction(){
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {            
            IInteractable obj = collidedOBJ.GetComponent<IInteractable>();

            if(obj == null) return;

            obj.Interact();
        }
    }

    // void OnSceneLoaded(Scene sceneLoaded, LoadSceneMode loadSceneMode)
    // {
    //     StartCoroutine(fadeComponent.FadeOut());
    // }

}

