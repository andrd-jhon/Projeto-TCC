using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : MonoBehaviour
{
    Image panel;
    public Image character1;
    public Image character2;
    TextMeshProUGUI name1;
    TextMeshProUGUI name2;
    TextMeshProUGUI talkText;

    DialogSystem dialogSystem;

    Color color;

    public float speed = 10f;
    bool transition = false;

    void Awake() 
    {
        dialogSystem = FindObjectOfType<DialogSystem>();

        panel       = transform.GetChild(0).GetComponent<Image>();
        character1  = transform.GetChild(1).GetComponent<Image>();
        character2  = transform.GetChild(2).GetComponent<Image>();
        name1       = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        name2       = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        talkText    = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    private void Update() 
    {
        if(transition){
            panel.fillAmount = Mathf.Lerp(panel.fillAmount, 1, speed * Time.deltaTime);
        }
        else
        {
            panel.fillAmount = Mathf.Lerp(panel.fillAmount, 0, speed * Time.deltaTime);
        }
    }

    private void Start() {

    }

    // public void SetNames(string nameText1, string nameText2){
    //     name1.text = nameText1;
    //     name2.text = nameText2;

    // }
    // public void SetCharacters(Sprite image1, Sprite image2){
    //     character1.sprite = image1;
    //     character2.sprite = image2;
    // }

    public void SetCharacter(string nameText1, Sprite image1, string nameText2, Sprite image2){
        name1.text = nameText1;
        character1.sprite = image1;
        name2.text = nameText2;
        character2.sprite = image2;
    }

    public void Enable()
    {
        panel.fillAmount = 0;
        character1.enabled = true;
        character2.enabled = true;
        transition = true;
    }

    public void Disable()
    {
        transition = false;
        name1.text = "";
        name2.text = "";
        character1.enabled = false;
        character2.enabled = false;
        talkText.text = "";
    }
}
    