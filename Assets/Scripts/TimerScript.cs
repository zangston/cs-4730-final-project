using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeLeft;
    public bool timerOn = false;

    public TMP_Text timerText;

    void Start()
    {
        timerOn = true;
        timeLeft = 50;
        timerText = GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0 && timerOn)
        {
            timeLeft -= Time.deltaTime;
            int tmp = (int)timeLeft;
            timerText.SetText(tmp.ToString());
        }
        else
        {
            timeLeft = 0;
            timerOn = false;
            Debug.Log("Should load scene " + Time.time);
            SceneManager.LoadScene("WinningScreen");
        }
    }
}
