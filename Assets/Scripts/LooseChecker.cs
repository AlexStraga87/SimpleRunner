using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseChecker : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private GameObject _gameOverText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Block>())
        {
            _mover.enabled = false;
            _gameOverText.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
