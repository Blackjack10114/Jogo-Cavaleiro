using UnityEngine;

public class Acompanha_player : MonoBehaviour
{
    GameObject Player;
    Vector3 posicaocoracao;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        posicaocoracao = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        this.transform.position = posicaocoracao + offset;
    }
}
