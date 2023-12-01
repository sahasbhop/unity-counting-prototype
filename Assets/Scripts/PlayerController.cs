using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!_gameManager.IsGameStarted() || _gameManager.IsGameOver()) return;
        
        var tf = transform;
        var horizontalInput = Input.GetAxis("Horizontal");
        tf.Translate(Vector3.right * (Time.deltaTime * horizontalInput * _gameManager.GetPlayerMoveSpeed()));

        var position = tf.position;
        var boundaryLeftRight = _gameManager.GetBoundaryLeftRight();
        if (position.x < -boundaryLeftRight) tf.position = new Vector3(-boundaryLeftRight, position.y, position.z);
        else if (position.x > boundaryLeftRight) tf.position = new Vector3(boundaryLeftRight, position.y, position.z);
    }
}