using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Ver_Texto : MonoBehaviour
{
    private TextMeshProUGUI texto;
    private PlayerAtaque contador_kills;
    private GameObject Player;
    public float tempotexto;
    [TextArea] public string primeira_frase, segunda_frase, terceira_frase;
    private string texto_vazio;
    private bool Trocoutexto, primeira, segunda, terceira;
    private float time;
    void Start()
    {
        texto = this.gameObject.GetComponent<TextMeshProUGUI>();
        Player = GameObject.FindWithTag("Player");
        contador_kills = Player.GetComponent<PlayerAtaque>();
        texto_vazio = "";
    }
    void Update()
    {
        chamartextos();
        if (Trocoutexto)
        {
            time += Time.deltaTime;
        }
        if (time >= tempotexto)
        {
            texto.text = texto_vazio;
            Trocoutexto = false;
            time = 0;
            contador_kills.kills = 0;
        }
    }
    private void chamartextos()
    {
        if (texto != null && !Trocoutexto && !primeira && contador_kills.kills >= 2)
        {
            texto.text = primeira_frase;
            Trocoutexto = true;
            primeira = true;
        }
        if (texto != null && !Trocoutexto && primeira && !segunda && contador_kills.kills >= 4)
        {
            texto.text = segunda_frase;
            Trocoutexto = true;
            segunda = true;
        }
        if (texto != null && !Trocoutexto && primeira && segunda && !terceira && contador_kills.kills >= 3)
        {
            texto.text = terceira_frase;
            Trocoutexto = true;
            terceira = true;
        }
    }
}
