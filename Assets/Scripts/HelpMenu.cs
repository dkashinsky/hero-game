using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScenesManager.Instance.OpenMainMenu();
        }
    }
}
