using UnityEngine;

public class Vida_HUD : MonoBehaviour
{
    public GameObject coracao1, coracao2, coracao3;
    private GameObject Player;
    private Vida vidaatual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        vidaatual = Player.GetComponent<Vida>();
    }
    void Update()
    {
        if (vidaatual.vidaAtual >= 3)
        {
            coracao1.GetComponent<SpriteRenderer>().enabled = true;
            coracao2.GetComponent<SpriteRenderer>().enabled = true;
            coracao3.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (vidaatual.vidaAtual == 2)
        {
            coracao3.GetComponent<SpriteRenderer>().enabled = false;
            coracao1.GetComponent<SpriteRenderer>().enabled = true;
            coracao2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (vidaatual.vidaAtual == 1)
        {
            coracao1.GetComponent<SpriteRenderer>().enabled = true;
            coracao2.GetComponent<SpriteRenderer>().enabled = false;
            coracao3.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (vidaatual.vidaAtual == 0)
        {
            coracao1.GetComponent<SpriteRenderer>().enabled = false;
            coracao2.GetComponent<SpriteRenderer>().enabled = false;
            coracao3.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
