using UnityEngine;

public class InformationBox : MonoBehaviour
{
    GameObject informationsBox;

    private InformationsBoxEnd informationsBoxEnd;
    
    private TypeTextAnimation informationType;
    public string informationText;

    [SerializeField] private float timeToDissapear;

    private void Start() {
        informationsBox = Resources.Load<GameObject>("Prefabs/InformationsBox");
    }

    public void ShowInformationsBox(){
        GameObject instatiatedBox = Instantiate(informationsBox);
        informationType = instatiatedBox.GetComponentInChildren<TypeTextAnimation>();
        informationsBoxEnd = instatiatedBox.GetComponentInChildren<InformationsBoxEnd>();
        informationsBoxEnd.timeToDisappear = timeToDissapear;
        informationType.speech = informationText;
        informationType.StartTyping();
    }
}
