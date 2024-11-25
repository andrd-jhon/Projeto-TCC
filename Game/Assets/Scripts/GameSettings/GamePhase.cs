using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhase : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject darkDefeatObj;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject darkPauseObj;

    private bool pauseIsOpen;
    private bool defeatIsOpen;

    private Image darkDefeatBG;

    private void Start()
    {
        darkDefeatBG = darkDefeatObj.GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseIsOpen && !defeatIsOpen)
        {
            OpenPausePanel();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseIsOpen && !defeatIsOpen)
        {
            ClosePausePanel();
        }

        if(defeatIsOpen && pauseIsOpen)
        {
            ClosePausePanel();
        }
    }

    public void ShowDefeat()
    {
        defeatIsOpen = true;
        StartCoroutine(DarkGB());
    }

    private IEnumerator DarkGB()
    {
        darkDefeatObj.SetActive(true);

        yield return new WaitForSeconds(2f);

        float timeElapsed = 0;

        while(timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime;
            darkDefeatBG.color = Color.Lerp(new Color(0,0,0,0), new Color(0,0,0,0.5f), timeElapsed / 1);
            yield return null;
        }

        defeatPanel.SetActive(true);
    }

    private void OpenPausePanel()
    {
        pauseIsOpen = true;
        pausePanel.SetActive(true);
        darkPauseObj.SetActive(true);
    }

    public void ClosePausePanel()
    {
        pauseIsOpen = false;
        pausePanel.SetActive(false);
        darkPauseObj.SetActive(false);
    }
}
