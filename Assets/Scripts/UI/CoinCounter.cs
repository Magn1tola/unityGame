using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private PlayerData _data;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        _data = FindObjectOfType<EntityPlayer>().Data;
    }

    private void Update() => _text.text = _data.Coins.ToString();
}