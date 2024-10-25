using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsTD : MonoBehaviour
{
    [SerializeField] private GameObject settings;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(settings.activeInHierarchy) settings.SetActive(false);
            else settings.SetActive(true);
        }
    }
}
