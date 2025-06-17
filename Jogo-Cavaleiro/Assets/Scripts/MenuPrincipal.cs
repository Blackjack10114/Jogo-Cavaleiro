using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string CenaJogar;
    public string CenaCredito;
    public string CenaMenu;

    public void Jogar()
    {
        SceneManager.LoadScene(CenaJogar);
    }
    public void creditos()
    {
        SceneManager.LoadScene(CenaCredito);
    }
    public void voltarmenu()
    {
        SceneManager.LoadScene(CenaMenu);
    }
    public void sair()
    {
        Application.Quit();
    }
}
