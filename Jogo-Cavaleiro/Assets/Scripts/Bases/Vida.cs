using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Configuração vida")]
    public int vidaMaxima;
    private int vidaAtual;

    public bool Morreu;

    [Header("Drop de vida")]
    public GameObject prefabVidaDropavel;
    [Range(0f, 1f)] public float chanceDrop = 0.15f; // 15%

    public System.Action OnMorrer;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int dano)
    {
        if (Morreu) return;

        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} levou {dano} de dano. Vida restante: {vidaAtual}");

        // Reação especial de inimigos
        GetComponent<Inimigo_Ursinho>()?.LevarDanoRecuo();

        if (vidaAtual <= 0)
        {
            Morreu = true;
            Morrer();
        }
    }

    public int VidaAtual()
    {
        return vidaAtual;
    }

    protected void Morrer()
    {
        Debug.Log($"{gameObject.name} morreu!");

        OnMorrer?.Invoke(); // Notifica scripts conectados

        // Drop de cura (somente para inimigos)
        if (!CompareTag("Player") && prefabVidaDropavel != null && Random.value < chanceDrop)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                Vida vida = player.GetComponent<Vida>();
                if (vida != null && vida.VidaAtual() < vida.vidaMaxima)
                {
                    Instantiate(prefabVidaDropavel, transform.position, Quaternion.identity);
                }
            }
        }

        // Somente inimigos são destruídos automaticamente
        if (!CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void Curar(int valor)
    {
        if (Morreu) return;

        vidaAtual = Mathf.Clamp(vidaAtual + valor, 0, vidaMaxima);
        Debug.Log($"{gameObject.name} curou {valor} de vida. Agora: {vidaAtual}");
    }

    private void Update()
    {
        if (PauseController.JogoPausado) return;
    }
}
