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
    private const int _widthGroundTile = 3;
    private const int _startXGround = -12;
    private const int _xOffsetCoin = 24;
    private const int _xOffsetObstacle = 21;

    private void Start()
    {
        foreach (var coinTransform in _coinsPool.GetElementList())
        {
            var coin = coinTransform.GetComponent<Coin>();
            _coins.Add(coin);
            coin.CoinTaked += OnCoinTaked;
        }

        var groundList = _groundsPool.GetElementList();
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].position = new Vector3(_startXGround + i * _widthGroundTile, 0, 0);
            groundList[i].gameObject.SetActive(true);
        }
        _firstGroundPiece = _groundsPool.GetElementByIndex(0);
        StartCoroutine(GenerateObstacle());
        StartCoroutine(GenerateCoinLine());
    }

    private void OnEnable()
    {
        foreach (var coin in _coins)
        {
            coin.CoinTaked += OnCoinTaked;
        }
    }

    private void OnDisable()
    {
        foreach (var coin in _coins)
        {
            coin.CoinTaked -= OnCoinTaked;
        }
    }

    private void OnCoinTaked(Transform coin)
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
            _firstGroundPiece.Translate(_widthGroundTile * _groundsPool.CountObject, 0, 0);
            var poolList = _groundsPool.GetElementList();
            poolList.Remove(_firstGroundPiece);
            poolList.Add(_firstGroundPiece);
            _groundsPool.SetElementList(poolList);
            _firstGroundPiece = _groundsPool.GetElementByIndex(0);            
        }

        if (_useds.Count > 0 && _player.position.x - _useds[0].position.x > _maxDistanceFromPlayer)
        {
            _useds[0].gameObject.SetActive(false);
            _useds.RemoveAt(0);            
        }
    }

    private IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1.3f));
            var obstacle = _obstaclePool.GetAvailableElement();
            if (obstacle != null)
            {
                Vector3 position = _player.position;
                position.x += _xOffsetObstacle;
                position.y = 0; 

                var collider2D = Physics2D.OverlapBoxAll(position, new Vector2(_widthGroundTile / 3f, _widthGroundTile / 3f), 0);
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

    private IEnumerator GenerateCoinLine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1));
            float yPosition = Random.Range(0, 2) * 2;
            var coinList = _coinsPool.GetAvailableElements(Random.Range(3, 6));

            for (int i = 0; i < coinList.Count; i++)
            {                
                GenerateCoin(coinList[i], i, yPosition);
            }
        }
    }

    private void GenerateCoin(Transform coin, int index, float yPosition)
    {
        Vector3 position = _player.position;
        position.x += _xOffsetCoin + index * 0.5f;
        position.y = yPosition;
        coin.gameObject.SetActive(true);
        coin.position = position;
        _useds.Add(coin);
    }
}
