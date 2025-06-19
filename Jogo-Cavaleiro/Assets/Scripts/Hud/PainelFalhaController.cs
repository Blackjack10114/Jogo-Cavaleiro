using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelFalhaController : MonoBehaviour
{
    public GameObject painelFalha;

    private bool jaMostrouFalha = false;

    void Update()
    {
        if (!jaMostrouFalha && JogadorMorreu())
        {
            MostrarFalha();
        }
    }

    private bool JogadorMorreu()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        Vida vida = player.GetComponent<Vida>();
        return vida != null && vida.Morreu;
    }

    private void MostrarFalha()
    {
        jaMostrouFalha = true;
        painelFalha.SetActive(true);
        Time.timeScale = 0f;
    }

}
