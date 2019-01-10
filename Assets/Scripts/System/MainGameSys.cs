using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSys : MonoBehaviour {
    public static MainGameSys instance;

    public GameWin mGameWin;

    public void InitMainGameSys()
    {
        instance = this;
    }
}
