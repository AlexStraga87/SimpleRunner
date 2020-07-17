using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<Transform> CoinDisable;

    public void OnDisable()
    {
        CoinDisable?.Invoke(transform);
    }
}
