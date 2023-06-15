using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private Text _text;
    private PlayerData _data;

    private void Start()
    {
        _text = GetComponent<Text>();
        _data = FindObjectOfType<EntityPlayer>().Data;
    }

    private void Update()
    {
        _text.text = _data.Money.ToString();
    }
}
