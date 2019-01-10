using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : UIWinBase {
    protected override void InitWin()
    {
        AudioSvc.instance.PlayBgClip(GameConstant.BgMainCity, true);
    }
    
}
