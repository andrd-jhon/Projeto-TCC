using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEngine.UI;

public class TypeTextAnimation : MonoBehaviour
{
    public Action TypeFinished;

    public float typeTime;
    public TextMeshProUGUI textObject;

    public string speech;

    Coroutine coroutine;

    public void StartTyping() {
        coroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText(){
        textObject.text = speech;
        textObject.maxVisibleCharacters = 0;
        for (int i=0; i<=textObject.text.Length; i++){
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeTime);  
        }

        TypeFinished?.Invoke();
    }

    public void Skip()
    {
        StopCoroutine(coroutine);
        textObject.maxVisibleCharacters = textObject.text.Length;
    }

}
