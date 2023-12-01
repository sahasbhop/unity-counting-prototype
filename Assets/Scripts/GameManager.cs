using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed = 15;
    [SerializeField] private float boundaryLeftRight = 4;
    [SerializeField] private float ballSpawnHeight = 25;
    [SerializeField] private float spawnMinRate = 0.1f;
    [SerializeField] private float spawnMaxRate = 0.5f;
    [SerializeField] private int timeLimitSeconds = 20;
    [SerializeField] private int blueBallScore = 10;
    [SerializeField] private int redBallScore = 25;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI blueBallText;
    public TextMeshProUGUI redBallText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI gameOverText;
    public Button playButton;
    public Button replayButton;
    private SpawnManager _spawnManager;
    private bool _isGameStarted;
    private bool _isGameOver;
    private int _seconds;
    private int _blueBallCount;
    private int _redBallCount;
    private int _score;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playButton.onClick.AddListener(OnGameStart);
        replayButton.onClick.AddListener(OnReplay);
    }

    private void OnGameStart()
    {
        titleText.gameObject.SetActive(false);
        hintText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        if (_isGameStarted) return;
        
        _isGameStarted = true;
        _score = 0;
        _seconds = timeLimitSeconds;
        StartCoroutine(CountdownRoutine());
        _spawnManager.SpawnBall();
    }

    private void OnGameOver()
    {
        _isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
    }

    private IEnumerator CountdownRoutine()
    {
        while (_seconds > 0)
        {
            yield return new WaitForSeconds(1);
            _seconds--;
            timeText.SetText($"Time: {_seconds}");            
        }
        OnGameOver();
    }

    private static void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IncreaseScore(int addingScore)
    {
        _score += addingScore;
        scoreText.SetText($"Score: {_score}");
    }

    public void OnCatchBlueBall()
    {
        _blueBallCount++;
        blueBallText.SetText($"Blue: {_blueBallCount}");
        IncreaseScore(blueBallScore);
    }

    public void OnCatchRedBall()
    {
        _redBallCount++;
        redBallText.SetText($"Red: {_redBallCount}");
        IncreaseScore(redBallScore);
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public float GetBoundaryLeftRight()
    {
        return boundaryLeftRight;
    }

    public float GetBallSpawnHeight()
    {
        return ballSpawnHeight;
    }

    public float GetSpawnMinRate()
    {
        return spawnMinRate;
    }
    
    public float GetSpawnMaxRate()
    {
        return spawnMaxRate;
    }

    public bool IsGameStarted()
    {
        return _isGameStarted;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
}