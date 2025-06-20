using UnityEngine;

public class Eventos_Texto : MonoBehaviour
{
    private PlayerAtaque Contador;
    private GameObject Player, Texto, Canvas;
    public Vector3 offset;
    private bool TextoInsta;
    public int Kills_Necessarias;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Canvas = GameObject.FindWithTag("Canvas");
        Contador = Player.GetComponent<PlayerAtaque>();
        Texto = Resources.Load<GameObject>("Texto_Eventos");
    }
    void Update()
    {
        if (Contador.kills == Kills_Necessarias && !TextoInsta)
        {
            Texto = Instantiate(Texto, Player.transform.position + offset, Quaternion.identity);
            Texto.transform.parent = Canvas.transform;
            Texto.transform.localScale = new Vector3(1, 1, 1);
            TextoInsta = true;
        }
    }
}
