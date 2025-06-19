using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string CenaJogar;
    public string CenaCredito;
    public string CenaMenu;

    public GameObject painelMenu;
    public GameObject painelOpcoes;
    public GameObject painelConfirmacao;

    private System.Action acaoConfirmada;

    public void Jogar()
    {
        SceneManager.LoadScene(CenaJogar);
    }
    public void creditos()
    {
        SceneManager.LoadScene(CenaCredito);
    }

    public void AbrirOpcoes()
    {
        painelMenu.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenu.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void MostrarConfirmacao(System.Action acao)
    {
        painelMenu.SetActive(false);
        painelConfirmacao.SetActive(true);
        acaoConfirmada = acao;
    }

    public void voltarmenu()
    {
        SceneManager.LoadScene(CenaMenu);
    }
    public void sair()
    {
        MostrarConfirmacao(() =>
        {
            Application.Quit();
        });
    }

    public void BotaoConfirmarSim()
    {
        acaoConfirmada?.Invoke();
        acaoConfirmada = null;
    }

    public void BotaoConfirmarNao()
    {
        painelConfirmacao.SetActive(false);
        painelMenu.SetActive(true);
    }
}
