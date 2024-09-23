using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerPopUp : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public StepSlot stepSlot;

    // public PopUpData currentStep;

    public string currentStepText;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        stepSlot = GetComponentInChildren<StepSlot>();
    }

    public void SetData(string text)
    {
        stepSlot.InsertStep(text);
        currentStepText = text;

    }

    public void ClearData()
    {
        stepSlot.RemoveStep();
        currentStepText = "";
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
