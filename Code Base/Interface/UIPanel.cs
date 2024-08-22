using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public void Open()
    {
        if (!GamePause.Instance.IsPause)
        {
            GamePause.Instance.Pause();
            gameObject.SetActive(true);
        }
    }
    
    public void Close()
    {
        if (GamePause.Instance.IsPause)
        {
            GamePause.Instance.Play();
            gameObject.SetActive(false);
        }
    }
}
