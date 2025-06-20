using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject painelPause;
    public GameObject painelConfirmacao;
    public GameObject painelTexto;

    private bool pausado = false;
    private System.Action acaoConfirmada;

    public static bool JogoPausado => Time.timeScale == 0f;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!pausado)
            AbrirPause();
        else
            FecharPause();
    }

    public void AbrirPause()
    {
        Time.timeScale = 0f;
        pausado = true;
        
        painelPause.SetActive(true);
        painelTexto.SetActive(false);
    }

    public void FecharPause()
    {
        Time.timeScale = 1f;
        pausado = false;
        painelPause.SetActive(false);
        painelTexto.SetActive(true);

    }

    public void BotaoContinuar() => FecharPause();

    public void BotaoMenuPrincipal()
    {
        MostrarConfirmacao(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MenuPrincipal");
        });
    }

    public void BotaoReiniciar()
    {
        MostrarConfirmacao(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    private void MostrarConfirmacao(System.Action acao)
    {
        painelPause.SetActive(false);
        painelConfirmacao.SetActive(true);
        acaoConfirmada = acao;
    }

    public void BotaoConfirmarSim()
    {
        acaoConfirmada?.Invoke();
        acaoConfirmada = null;
    }

    public void BotaoConfirmarNao()
    {
        painelConfirmacao.SetActive(false);
        painelPause.SetActive(true);
    }
}
