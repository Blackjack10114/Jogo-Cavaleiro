using UnityEngine;

public class InimigoSeguidor : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia = new Vector3(8f, 0f, 0f); // mesma distância usada no player

    public enum Linha { Esquerda, Centro, Direita }
    public Linha linhaAtual = Linha.Centro;

    void Start()
    {
        // Posiciona o inimigo de acordo com a linha atual
        Vector3 pos = transform.position;

        if (linhaAtual == Linha.Esquerda)
            pos.x -= distancia.x;
        else if (linhaAtual == Linha.Direita)
            pos.x += distancia.x;

        transform.position = pos;
    }

    void Update()
    {
        // Sobe automaticamente
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }
}
