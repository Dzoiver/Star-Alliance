using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    private void Awake()
    {
        instance = this;
    }
    int score = 0;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
