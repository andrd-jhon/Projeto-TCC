using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER { FREE, INTERACT }

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    DialogSystem dialogSystem;
    Dialog dialog;
    public GameObject collidedOBJ;

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    public bool playerIsClose;
    bool canMove;
    
    public PLAYER state;
    
    private void Awake() 
    {
        dialog = FindObjectOfType<Dialog>();
        dialogSystem = FindObjectOfType<DialogSystem>();
    }
    
    private void Start() 
    {
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
                // dialog.Interact();
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
    }else{
        animator.SetBool("isMoving", false);
    }
    //transform.position = transform.position + movement * speed * Time.deltaTime;
    rb.velocity = new Vector2(movement.x, movement.y) * speed;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Interactable")){
            collidedOBJ = other.gameObject;
            playerIsClose = true;
            Debug.Log("O player colidiu com o" + collidedOBJ.name);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Interactable")){
            collidedOBJ = null;
            playerIsClose = false;
            Debug.Log("O player saiu de colisao com o" + other.gameObject.name);
        }
        
    }

    
    void IsFree(){
        if(dialogSystem.state == STATE.DISABLED){
            state = PLAYER.FREE;
        }else{
            state = PLAYER.INTERACT;
        }
    }

    void Interaction(){
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            dialog.Conversation();
            state = PLAYER.INTERACT;
        }
    }
}