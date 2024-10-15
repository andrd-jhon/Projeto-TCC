using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InformationsBoxEnd : MonoBehaviour
{
    private TypeTextAnimation informationType;
    private Image imageBox;

    public float timeToDisappear;

    private void Awake() {
        informationType = GetComponentInChildren<TypeTextAnimation>();
        imageBox = GetComponentInChildren<Image>();
        informationType.TypeFinished = DisappearBox;
    }

    private void DisappearBox(){
        StartCoroutine(OnTypeFinishe());
    }

    private IEnumerator OnTypeFinishe(){
        yield return new WaitForSeconds(timeToDisappear);
        
        float timeElapsed = 0f;

        while(timeElapsed < 1){
            timeElapsed += Time.deltaTime;
            imageBox.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(0, 0, 0, 0), timeElapsed / 1);
            informationType.textObject.color = Color.Lerp(new Color(63 / 255, 42 / 255 , 24 / 255, 1), new Color(0, 0, 0, 0), timeElapsed / 1);
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }
}
