using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickYesText : MonoBehaviour
{
    public void ClickText()
    {
        SceneManager.LoadScene("ResultScene");
        IngameManager.Instance._nowGameScene = DefinedEnum.GameType.Ending;
    }
}
