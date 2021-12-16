using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uigame : MonoBehaviour
{
    public Text timer;


    private void Update()
    {
        timer.text = LevelManager.timeLeft.ToString();
    }
        
}
