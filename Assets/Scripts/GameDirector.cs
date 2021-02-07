using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    Text scoreText; 
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();　//ScoreのUIを代入する
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = "Score : " + score.ToString(); //得点を表示させる
    }

    public void ScoreDown()
    {
        score--;
        scoreText.text = "Score : " + score.ToString();
    }
}
