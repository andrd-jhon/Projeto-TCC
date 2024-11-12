using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathPF;
    private float currentHealth;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;    
    }

    public void DecreaseHealth(float mount)
    {
        currentHealth -= mount;
        
        if(currentHealth <= 0.0f)
        {
            Die();
        }
        else
        {

        }
    }

    private void Die()
    {
        GameObject deathGO = Instantiate(deathPF, transform.position, transform.rotation);
        CinemachineVirtualCamera CM = FindFirstObjectByType<CinemachineVirtualCamera>();
        CM.Follow = deathGO.transform;

        GamePhase gamePhase = FindFirstObjectByType<GamePhase>();
        gamePhase.ShowDefeat();

        Destroy(gameObject);
    }


}
