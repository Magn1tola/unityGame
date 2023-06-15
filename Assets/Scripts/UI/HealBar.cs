using System;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    private Slider _slider;
    private EntityPlayer _player;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _player = FindObjectOfType<EntityPlayer>();
    }

    private void Update()
    {
        _slider.value = _player._health / _player.MaxHealth;
    }
}
