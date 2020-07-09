using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public event UnityAction<string> CoinTaked;
    private int coinCont = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);
            coinCont++;
            CoinTaked?.Invoke(coinCont.ToString());
        }
    }
}
