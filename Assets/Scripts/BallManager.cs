using UnityEngine;

public class BallManager : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (transform.position.y < -2) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_gameManager.IsGameStarted() || _gameManager.IsGameOver()) return;
        
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("player") && gameObject.CompareTag("ball_blue"))
        {
            _gameManager.OnCatchBlueBall();
            Destroy(gameObject);
        }
        else if (otherGameObject.CompareTag("player") && gameObject.CompareTag("ball_red"))
        {
            _gameManager.OnCatchRedBall();
            Destroy(gameObject);
        }
    }
}