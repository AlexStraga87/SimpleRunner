using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Pool _groundsPool;
    [SerializeField] private Pool _coinsPool;
    [SerializeField] private Pool _obstaclePool;
    [SerializeField] private Transform _player;

    private List<Transform> _useds = new List<Transform>();
    private List<Coin> _coins = new List<Coin>();
    private Transform _firstGroundPiece;
    private const int _maxDistanceFromPlayer = 8;

    private void Start()
    {
        foreach (var coinTransform in _coinsPool.GetElementList())
        {
            var coin = coinTransform.GetComponent<Coin>();
            _coins.Add(coin);
            coin.CoinDisable += OnCoinDisable;
        }

        var groundList = _groundsPool.GetElementList();
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].position = new Vector3(-12 + i * 3, 0, 0);
            groundList[i].gameObject.SetActive(true);

        }
        _firstGroundPiece = _groundsPool.GetElementByIndex(0);
        StartCoroutine(PutObstacleOnZone());
        StartCoroutine(CreateCoinLine());
    }

    private void OnEnable()
    {
        foreach (var coin in _coins)
        {
            coin.CoinDisable += OnCoinDisable;
        }
    }

    private void OnDisable()
    {
        foreach (var coin in _coins)
        {
            coin.CoinDisable -= OnCoinDisable;
        }
    }

    private void OnCoinDisable(Transform coin)
    {
        if (_useds.Contains(coin))
        {
            _useds.Remove(coin);
        }
    }

    private void Update()
    {
        if (_player.position.x - _groundsPool.GetElementByIndex(0).position.x > _maxDistanceFromPlayer)
        {
            _firstGroundPiece.Translate(30, 0, 0);
            _groundsPool.MoveFirstElementToEndList();
            _firstGroundPiece = _groundsPool.GetElementByIndex(0);            
        }

        if (_useds.Count > 0 && _player.position.x - _useds[0].position.x > _maxDistanceFromPlayer)
        {
            _useds[0].gameObject.SetActive(false);
            _useds.RemoveAt(0);            
        }
    }

    private IEnumerator PutObstacleOnZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1.3f));
            var obstacle = _obstaclePool.GetAvailableElement();
            if (obstacle != null)
            {
                Vector3 position = _player.position;
                position.x += 21;
                position.y = 0; 

                var collider2D = Physics2D.OverlapBoxAll(position, new Vector2(1.2f, 0.8f), 0);
                foreach (var collider in collider2D)
                {
                    if (collider.GetComponent<Coin>())
                    {
                        position.y = 2;
                        break;
                    }
                }

                obstacle.gameObject.SetActive(true);
                obstacle.position = position;
                _useds.Add(obstacle);                
            }
        }
    }

    private IEnumerator CreateCoinLine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1));
            float yPosition = Random.Range(0, 2) * 2;
            var coinList = _coinsPool.GetAvailableElements(Random.Range(3, 6));

            for (int i = 0; i < coinList.Count; i++)
            {                
                PutCoinOnZone(coinList[i], i, yPosition);
            }
        }
    }

    private void PutCoinOnZone(Transform coin, int index, float yPosition)
    {
        Vector3 position = _player.position;
        position.x += 24 + index * 0.5f;
        position.y = yPosition;
        coin.gameObject.SetActive(true);
        coin.position = position;
        _useds.Add(coin);
    }
}
