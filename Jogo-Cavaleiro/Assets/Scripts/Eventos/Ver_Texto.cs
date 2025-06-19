using TMPro;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TextoCondicional
{
    [TextArea]
    public string frase;
    public int killsNecessarios;
    public bool jaMostrado = false;
}
public class Ver_Texto : MonoBehaviour
{
    private TextMeshProUGUI texto;
    private PlayerAtaque contador_kills;
    private GameObject Player;

    public float tempotexto = 3f;
    private float tempoAtual = 0f;
    private bool Trocoutexto = false;

    [TextArea]
    private string texto_vazio = "";
    public List<TextoCondicional> frasesCondicionais = new List<TextoCondicional>();

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        Player = GameObject.FindWithTag("Player");
        contador_kills = Player.GetComponent<PlayerAtaque>();
    }

    void Update()
    {
        VerificarFrases();

        if (Trocoutexto)
        {
            tempoAtual += Time.deltaTime;

            if (tempoAtual >= tempotexto)
            {
                texto.text = texto_vazio;
                Trocoutexto = false;
                tempoAtual = 0f;
                contador_kills.kills = 0;
            }
        }
    }

    void VerificarFrases()
    {
        if (Trocoutexto) return;

        foreach (var frase in frasesCondicionais)
        {
            if (!frase.jaMostrado && contador_kills.kills >= frase.killsNecessarios)
            {
                texto.text = frase.frase;
                frase.jaMostrado = true;
                Trocoutexto = true;
                break;
            }
        }
    }
}