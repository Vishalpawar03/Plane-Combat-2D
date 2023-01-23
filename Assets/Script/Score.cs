using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = count.ToString();
        gameOverScoreText.text = "Score: "+count.ToString();
    }

    public void AddCount() {
        count++;
    }
}
