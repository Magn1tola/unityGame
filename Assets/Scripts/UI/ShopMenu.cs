using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private List<ItemData> items;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;

    private int _currentIndex;

    private void Update()
    {
        UpdateItem();

        if (MovementController.BlockInput)
        {
            if (Input.GetKeyDown(KeyCode.A)) _currentIndex--;
            else if (Input.GetKeyDown(KeyCode.D)) _currentIndex++;
            else if (Input.GetKeyDown(KeyCode.Space)) BuyItem();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        MovementController.BlockInput = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        MovementController.BlockInput = false;
    }

    private void BuyItem()
    {
        var item = items[_currentIndex];

        UnityEngine.Debug.Log(item);
    }

    private void UpdateItem()
    {
        if (_currentIndex >= items.Count) _currentIndex = 0;
        else if (_currentIndex < 0) _currentIndex = items.Count - 1;

        var item = items[_currentIndex];
        itemImage.sprite = item.sprite;
        price.text = item.price.ToString();
        description.text = item.description;
    }
}