using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        wallet.CoinTaked += OnCoinTaked;
    }

    private void OnDisable()
    {
        wallet.CoinTaked -= OnCoinTaked;
    }

    private void OnCoinTaked(string value)
    {
        text.text = value;
    }
}
