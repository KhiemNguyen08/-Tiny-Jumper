using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
   public static int bestScore
    {
        set
        {
            if(PlayerPrefs.GetInt(Prefconst.BEST_SCORE,0)< value)
            {
                PlayerPrefs.SetInt(Prefconst.BEST_SCORE, value);
            }
        }
        get => PlayerPrefs.GetInt(Prefconst.BEST_SCORE, 0);
    }
}
