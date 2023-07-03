using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
     [SerializeField] private GameObject menu;
     
     private EntityPlayer _player;
     private void Awake()
     {
          _player = FindObjectOfType<EntityPlayer>();
          _player.playerDead.AddListener(ActivateMenu);
     }

     private void Start() => menu.SetActive(false);

     private void ActivateMenu() => menu.SetActive(true);

     public void Exit() => Application.Quit();

     public void LoadHub() => SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
}
