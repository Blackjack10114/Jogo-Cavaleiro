using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Configura��o vida")]
    public int vidaMaxima;
    [HideInInspector] public int vidaAtual;

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

        // Rea��o especial de inimigos
        GetComponent<Inimigo_Ursinho>()?.LevarDanoRecuo();

        if (vidaAtual <= 0)
        {
            Morreu = true;
            Morrer();
        }

        if (CompareTag("Player"))
        {
            SomPlayer somPlayer = GetComponent<SomPlayer>();
            if (somPlayer != null)
            {
                somPlayer.TocarDano();
            }
        }


    }

    public int VidaAtual()
    {
        return vidaAtual;
    }

    protected void Morrer()
    {
        Debug.Log($"{gameObject.name} morreu!");

        ControladorNarrativa.Instance?.RegistrarKill();//Conta Kill
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

        // Somente inimigos s�o destru�dos automaticamente
        if (!CompareTag("Player"))
        {
            GetComponent<SomPlayer>()?.TocarSom(GetComponent<SomPlayer>().somMorte);
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
