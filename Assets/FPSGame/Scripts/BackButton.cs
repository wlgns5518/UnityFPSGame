using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPSGame
{
    public class BackButton : MonoBehaviour
    {
        public GameObject Button;       //엔딩 버튼
        public void ClickButton()
        {
            SceneManager.LoadScene("Startup");   //처음화면으로 이동
        }

    }
}
