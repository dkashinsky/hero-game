using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public GameObject windowsHelp;
    public GameObject androidHelp;

    void Awake()
    {
        windowsHelp.SetActive(Application.platform != RuntimePlatform.Android);
        androidHelp.SetActive(Application.platform == RuntimePlatform.Android);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScenesManager.Instance.OpenMainMenu();
        }
    }
}
