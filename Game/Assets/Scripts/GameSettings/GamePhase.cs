using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhase : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject darkObj;

    private Image darkBG;

    private void Start()
    {
        darkBG = darkObj.GetComponent<Image>();
    }

    public void ShowDefeat()
    {
        StartCoroutine(DarkGB());
    }

    private IEnumerator DarkGB()
    {
        darkObj.SetActive(true);

        yield return new WaitForSeconds(2f);

        float timeElapsed = 0;

        while(timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime;
            darkBG.color = Color.Lerp(new Color(0,0,0,0), new Color(0,0,0,0.5f), timeElapsed / 1);
            yield return null;
        }

        defeatPanel.SetActive(true);
    }
}
