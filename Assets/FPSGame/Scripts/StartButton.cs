using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class StartButton : MonoBehaviour
    {
        public GameObject Button;       //시작버튼
        public GameObject SceneLoader;  //씬로더
        // Start is called before the first frame update
        public void ClickButton()
        {
            SceneLoader.SetActive(true);
        }

    }
}
