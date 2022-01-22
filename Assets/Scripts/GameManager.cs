using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score = 0;
    public GameObject scoreText;

    public float speed = 1f;
    public static GameManager instance = null;

    public GameObject gameOverScoreText;
    private Text actualText;

    private Text firstSceneActualText;

    private void Awake()
    {
        //instance = this;
        //Makes the GameManager a Singleton
        if (instance == null)
        instance = this;
        else if (instance != this)
        Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            score = 0;
            scoreText = GameObject.Find("FirstSceneScore");
            firstSceneActualText = scoreText.GetComponent<Text>();
            Debug.Log("It is Working!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("FirstSceneScore");
        firstSceneActualText = scoreText.GetComponent<Text>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            score += .10f;
            firstSceneActualText.text = "Score " + (int)score;
        }

        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            gameOverScoreText = GameObject.Find("Score Text");
            actualText = gameOverScoreText.GetComponent<Text>();
            actualText.text = "Your Final Score was " + (int)score;
        }
    }

    void setScoreText()
    {
        gameOverScoreText = GameObject.Find("Score Text");
    }
}
