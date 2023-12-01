using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ballPrefabs;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void SpawnBall()
    {
        if (!_gameManager.IsGameStarted() || _gameManager.IsGameOver()) return;

        var ball = ballPrefabs[Random.Range(0, ballPrefabs.Count)];
        var spawnX = Random.Range(-_gameManager.GetBoundaryLeftRight(), _gameManager.GetBoundaryLeftRight());
        var spawnY = _gameManager.GetBallSpawnHeight();
        Instantiate(ball, new Vector3(spawnX, spawnY, 0), ball.transform.rotation);
        Invoke(nameof(SpawnBall), Random.Range(_gameManager.GetSpawnMinRate(), _gameManager.GetSpawnMaxRate()));
    }
}