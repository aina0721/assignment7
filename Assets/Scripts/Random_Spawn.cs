using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Random_Spawn : MonoBehaviour
{
    public GameObject[] Mogura;

    public Text TimeText; // 残り時間のテキスト
    public GameObject resultPanal; // リザルト
    string playTime;
    float time = 30; // 残り時間

    public Text scoreText; // スコアのテキスト
    int score; // スコア
    public Text finalText; // 合計スコア

    public bool isClick;
    public bool isCat; // ネコかどうか
    public int number;

    private int Count;
    public float time_count;

    void Start()
    {
        scoreText.text = "得点：" + score;

        Count = 1;

        number = Random.Range(0, Mogura.Length);
        Mogura[number].SetActive(true);
    }

    void Update()
    {
        playTime = System.DateTime.Now.ToString();

        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 0;
        }
            
        TimeText.text = "残り時間：" + ((int)time).ToString();
        // 残り時間が0になったら
        if (time == 0)
        {
            // リザルトを表示する
            resultPanal.SetActive(true);
            finalText.text = scoreText.text;
            Debug.Log("時間になった");
        }

        Spawn();

        if (isClick)
        {
            Mogura[number].SetActive(false);
            time_count += Time.deltaTime;
            if (time_count >= 0.5f)
            {
                isClick = false;
                Count = 0;
                time_count = 0f;
            }
            Debug.Log("オブジェクトがタッチされました!!");
        }

        // ネコだったら
        if (isCat)
        {
            time_count += Time.deltaTime;
            if (time_count >= 1.0f)
            {
                isCat = false;
                time_count = 0f;
                isClick = true;   
            }
        }
    }

    void Spawn()
    {
        if (Count == 0)
        {
            number = Random.Range(0, Mogura.Length);
            Mogura[number].SetActive(true);
            if (number > 17)
            {
                isCat = true;
            }
            Count = 1;
        }
    }

    public void OnTouched()
    {
        isClick = true;

        // ネコをクリックしたら
        if (isCat)
        {
            // スコアを減らす
            score -= 10;
            isCat = false;
            time_count = 0f;
        }
        else
        {
            // スコアを増やす
            score += 10;
        }

        scoreText.text = "得点:" + score;
    }

    void InitScore()
    {
        //スコア初期化
        score = 0;
    }

    public void OnTitleButton()
    {
        // タイトルに戻る
        SceneManager.LoadScene("Title");
        InitScore();

    }

    public void OnRetryButton()
    {
        // もう一回遊ぶ
        SceneManager.LoadScene("Main");
        InitScore();

    }
}
