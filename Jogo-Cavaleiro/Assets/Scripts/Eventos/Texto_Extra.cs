using UnityEngine;

public class NarradorReativo : MonoBehaviour
{
    private PlayerAtaque player;
    private Vida vida;
    private float tempoSemAtacar;
    private bool falouDesvio, falouDano;

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerAtaque>();
        vida = player.GetComponent<Vida>();
    }

    void Update()
    {
        if (!player.EstaAtacando)
            tempoSemAtacar += Time.deltaTime;
        else
            tempoSemAtacar = 0f;

        if (tempoSemAtacar > 5f && !falouDesvio)
        {
            TextoNarrativa.Instance.MostrarTexto("Você vai ficar só desviando?");
            falouDesvio = true;
        }

        if (vida.VidaAtual() < vida.vidaMaxima - 2 && !falouDano)
        {
            TextoNarrativa.Instance.MostrarTexto("Talvez tentar bater ajudasse, hein?");
            falouDano = true;
        }
    }
}
