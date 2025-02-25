using FPSGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCount: MonoBehaviour
{
    public static int killcount;
    public FPSGameManager manager;

    private void Start()
    {
        killcount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        killcount = manager.killcount;
    }
}
