using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRetryPanel : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.onGameOver += SetEnabled;
        SetDisabled();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onGameOver -= SetEnabled;
    }


    private void SetEnabled()
    {
        gameObject.SetActive(true);
    }

    private void SetDisabled()
    {
        gameObject.SetActive(false);
    }
}
