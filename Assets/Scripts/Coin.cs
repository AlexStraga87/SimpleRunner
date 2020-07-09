using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<GameObject> CoinTaked;

    private void OnDisable()
    {
        CoinTaked?.Invoke(gameObject);
    }
}
