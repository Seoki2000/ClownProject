using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChess : MonoBehaviour, IPointerClickHandler
{
    GameObject _chessObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_chessObj != null)
        {
            _chessObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/ChessBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _chessObj = GameObject.FindGameObjectWithTag("ChessObj");
        }
    }
}
