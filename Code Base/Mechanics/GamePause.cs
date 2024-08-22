using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : Singleton<GamePause>
{
    public bool IsPause;

    public void Pause()
    {
        IsPause = true;
    }

    public void Play()
    {
        IsPause = false;
    }
}
