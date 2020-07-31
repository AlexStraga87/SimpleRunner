using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<Transform> CoinTaked;

    public void OnCoinTaked()
    {
        CoinTaked?.Invoke(transform);
    }
}
