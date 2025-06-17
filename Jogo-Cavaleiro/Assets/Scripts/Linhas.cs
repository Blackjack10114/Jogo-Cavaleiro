using UnityEngine;

public class LinhasController : MonoBehaviour
{
    public enum Linha
    {
        Esquerda,
        Centro,
        Direita
    }

    public Transform linhaEsquerda;
    public Transform linhaCentro;
    public Transform linhaDireita;

    public static LinhasController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public float PosicaoX(Linha linha)
    {
        switch (linha)
        {
            case Linha.Esquerda: return linhaEsquerda.position.x;
            case Linha.Direita: return linhaDireita.position.x;
            case Linha.Centro:
            default: return linhaCentro.position.x;
        }
    }
}
