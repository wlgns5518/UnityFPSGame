using FPSGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingKillCount : MonoBehaviour
{
    // 킬 수를 UI로 보여줄 때 사용하는 Text 컴포넌트.
    public TMPro.TextMeshProUGUI killcountText;

    // 플레이어가 획득한 킬(Kill) 수.
    public int endingkillcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        endingkillcount = KillCount.killcount;
        if (killcountText != null)
        {
            killcountText.text = "KILL " + endingkillcount.ToString("0000");
        }
    }

}
