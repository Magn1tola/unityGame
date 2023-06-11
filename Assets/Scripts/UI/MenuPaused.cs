using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPaused : MonoBehaviour
{
    public GameObject menuPaused;
    [SerializeField] private KeyCode keyMenuPaused;
    bool isMenuPaused = false;

    private void Start()
    {
        menuPaused.SetActive(false);
    }

    void ActiveMenu()
    {
        if (Input.GetKey(keyMenuPaused))
        {
            isMenuPaused = !isMenuPaused;
        }

        if (isMenuPaused)
        {
            menuPaused.SetActive(true);
        }
        else
        {
            menuPaused.SetActive(false);
        }
    }
}
