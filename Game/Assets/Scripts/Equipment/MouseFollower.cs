using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    public EquipmentField equipmentField;

    public EquipmentData currentEquipment { get; private set; }

    private void Awake() 
    {
        canvas = transform.root.GetComponent<Canvas>();
        equipmentField = GetComponentInChildren<EquipmentField>();    
    }

    public void SetData(EquipmentData equipmentData)
    {
        equipmentField.InsertEquipment(equipmentData);
        currentEquipment = equipmentData;
    }

    public void ClearData()
    {
        equipmentField.RemoveEquipment();
        currentEquipment = null;
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
                );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
        if (!val)
        {
            ClearData();
        }
    }
}
