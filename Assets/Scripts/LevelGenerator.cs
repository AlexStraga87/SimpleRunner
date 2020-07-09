using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _groundTemplate;
    [SerializeField] private GameObject _coinTemplate;
    [SerializeField] private GameObject _blockTemplate;    
    [SerializeField] private Transform _player;
    private List<Transform> _grounds = new List<Transform>();
    private List<GameObject> _coins = new List<GameObject>();
    private List<GameObject> _blocks = new List<GameObject>();
    private List<GameObject> _useds = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject ground = Instantiate(_groundTemplate, new Vector3(-12 + i * 3, 0, 0), Quaternion.identity);
            _grounds.Add(ground.transform);
            ground.transform.SetParent(transform);
            _blocks.Add(Instantiate(_blockTemplate, new Vector3(0, 0, 0), Quaternion.identity));
            _blocks[i].transform.SetParent(transform);
            _blocks[i].SetActive(false);
        }

        for (int i = 0; i < 20; i++)
        {
            _coins.Add(Instantiate(_coinTemplate, new Vector3(0, 0, 0), Quaternion.identity));
            _coins[i].transform.SetParent(transform);
            _coins[i].SetActive(false);            
        }

        StartCoroutine(PutBlockCoroutine());
        StartCoroutine(PutCoinCoroutine());
    }

    private void OnEnable()
    {
        for (int i = 0; i < 20; i++)
        {
            _coins[i].GetComponent<Coin>().CoinTaked += OnCoinTaked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < 20; i++)
        {
            _coins[i].GetComponent<Coin>().CoinTaked -= OnCoinTaked;
        }
    }

    private void OnCoinTaked(GameObject coin)
    {
        if (_useds.Contains(coin))
        {
            _useds.Remove(coin);
        }
    }

    private void Update()
    {
        if (_player.position.x - _grounds[0].position.x > 8)
        {
            SendGroundForward();
        }
        if (_useds.Count > 0 && _player.position.x - _useds[0].transform.position.x > 8)
        {
            _useds[0].SetActive(false);
            _useds.RemoveAt(0);            
        }
    }

    private void SendGroundForward()
    {
        var ground = _grounds[0];
        ground.Translate(30, 0, 0);
        _grounds.RemoveAt(0);
        _grounds.Add(ground);
    }

    private IEnumerator PutBlockCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1.3f));
            for (int i = 0; i < 10; i++)
            {
                if (_blocks[i].activeSelf == false)
                {
                    GameObject block = _blocks[i];
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
                    
                    block.SetActive(true);
                    block.transform.position = position;
                    _useds.Add(block);
                    break;
                }
            }
        }
    }

    private IEnumerator PutCoinCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0, 1));
            int coinCount = Random.Range(3, 6);
            int index = 0;
            float yPosition = Random.Range(0, 2) * 2;
            for (int i = 0; i < 20; i++)
            {                
                if (_coins[i].activeSelf == false)
                {
                    PutCoin(_coins[i], index, yPosition);
                    coinCount--;
                    index++;
                    if (coinCount == 0) break;
                }
            }
        }
    }

    private void PutCoin(GameObject coin, int index, float yPosition)
    {
        Vector3 position = _player.position;
        position.x += 21 + index * 0.5f;
        position.y = yPosition;
        coin.SetActive(true);
        coin.transform.position = position;
        _useds.Add(coin);
    }
}
