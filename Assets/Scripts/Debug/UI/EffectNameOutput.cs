using TMPro;
using UnityEngine;

public class EffectNameOutput : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private EntityPlayer _player;
    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
        _player = FindObjectOfType<EntityPlayer>();
    }

    private void Update() => _textMeshPro.text = GenerateString();

    private string GenerateString()
    {
        var list = _player.effectsController.Effects;
        string str = "EffectList(Debug):\n";
        foreach (var item in list)
        {
            str += item.ToString();
            str += '\n';
        }

        return str;
    }
}
