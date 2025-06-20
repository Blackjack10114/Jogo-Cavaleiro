using UnityEngine;

public class PlayerMorteHandler : MonoBehaviour
{
    public GameObject painelFalha;
    public GameObject painelTexto;
    private Vida vida;

    void Start()
    {
        vida = GetComponent<Vida>();
        if (vida != null)
            vida.OnMorrer += MostrarPainel;
    }

    private void MostrarPainel()
    {
        Time.timeScale = 0f;
        painelTexto.SetActive(false);
        painelFalha.SetActive(true);
    }
}
