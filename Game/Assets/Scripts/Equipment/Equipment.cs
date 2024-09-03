using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private string EquipmentName;

    [SerializeField]
    private Sprite image;

    [TextArea]
    [SerializeField]
    private string description;

    private EquipmentManager equipmentManager;
    
    void Start()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            equipmentManager.AddEquipment(EquipmentName, image, description);
            Destroy(gameObject);
        }
    }
}
