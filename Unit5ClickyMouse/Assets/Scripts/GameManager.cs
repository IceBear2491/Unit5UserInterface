using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    private bool paused;

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void StartGame(int difficulty)
    {
        spawnRate = difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLives(3);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
    }
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    { 
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
}
