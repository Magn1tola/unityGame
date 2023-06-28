using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private EntityPlayer _player;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _player = FindObjectOfType<EntityPlayer>();
    }

    private void Update() => _slider.value = _player.Stamina / _player.MaxStamina;
}