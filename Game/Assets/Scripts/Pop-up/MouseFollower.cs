// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MouseFollowerPopUp : MonoBehaviour
// {
//     [SerializeField]
//     private Canvas canvas;

//     public StepSlot stepSlot;

//     public Step currentStep;

//     private void Awake()
//     {
//         stepSlot = GetComponentInChildren<StepSlot>();
//     }

//     public void SetData(Step step)
//     {
//         StepSlot.InsertStep(step);
//         currentStep = equipmentData;
//     }

//     public void ClearData()
//     {
//         stepSlot.RemoveEquipment();
//         currentStep = "";
//     }

//     void Update()
//     {
//         Vector2 position;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             (RectTransform)canvas.transform,
//             Input.mousePosition,
//             canvas.worldCamera,
//             out position
//                 );
//         transform.position = canvas.transform.TransformPoint(position);
//     }

//     public void Toggle(bool val)
//     {
//         gameObject.SetActive(val);
//         if (!val)
//         {
//             ClearData();
//         }
//     }
// }
