using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima;
    private int vidaAtual;

    public bool Morreu;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} levou {dano} de dano. Vida restante: {vidaAtual}");

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

        //

        if (Morreu)
        {
            Destroy(gameObject);
        }
    }
}
