using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject square;
    public GameObject endPanel;
    public Text timeTxt;
    public Text nowScore;
    public Text bestScore;

    public Animator anim;

    bool isPlay = true;

    float time = 0.0f;

    string key = "bestScore";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
        
    }

    void MakeSquare()
    {
        Instantiate(square);
    }

    public void GameOver()
    {
        isPlay = false  ;
        anim.SetBool("isDie", true);
        Invoke("TimeStop", 0.5f);
        nowScore.text = time.ToString("N2");

        // 최고점수가 있다면
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            // 최고점수 < 현재점수
            if (best < time)
            {
                //현재점수를 최고점수에 저장
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }
            else
            {
                bestScore.text = best.ToString("N2");
            }
        }
        // 최고점수가 없다면
        else
        {
            // 현재 점수를 저장
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");
        }


        endPanel.SetActive(true);
    }

    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}
