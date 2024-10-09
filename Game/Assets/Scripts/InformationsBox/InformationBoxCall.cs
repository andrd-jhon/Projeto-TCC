using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InformationBoxCall : MonoBehaviour
{
    public GameObject informationBox;
    private TypeTextAnimation informationType;
    public string informationText;
    
    public float timeAppeared;

    private bool isCalled;

    private void Start()
    {
        informationType = informationBox.GetComponent<TypeTextAnimation>();
    }

    public void ApenasDeTeste(){
        StartCoroutine(ActiveBox());
    }

    public IEnumerator ActiveBox(){
        if(!isCalled){
            isCalled = true;
            informationBox.SetActive(true);
            informationType.speech = informationText;
            informationType.StartTyping();
            yield return new WaitForSeconds(timeAppeared);
            informationBox.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
