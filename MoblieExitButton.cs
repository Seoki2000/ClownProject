using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoblieExitButton : MonoBehaviour
{
    public void ClickYesText()
    {
        Application.Quit();
    }
    public void ClickNoText()
    {
        Destroy(gameObject);
    }
}
