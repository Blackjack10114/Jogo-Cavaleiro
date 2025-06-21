using UnityEngine;

public class MovimentoVertical : MonoBehaviour
{
    public enum Direcao { Subindo, Descendo }
    public Direcao direcao = Direcao.Subindo;
    public float velocidade = 2f;

    private Inimigo_Piolho piolho;

    void Start()
    {
        // Busca a referência do script de origem 
        piolho = GetComponent<Inimigo_Piolho>();
    }

    void Update()
    {
        if (PauseController.JogoPausado) return;

        Vector3 dir = direcao == Direcao.Subindo ? Vector3.up : Vector3.down;
        transform.Translate(dir * velocidade * Time.deltaTime);

        // MANTÉM o piolho fixo na linha
        if (piolho != null && LinhasController.Instance != null)
        {
            float xLinha = LinhasController.Instance.PosicaoX(piolho.linhaAtual);
            transform.position = new Vector3(xLinha, transform.position.y, transform.position.z);
        }
    }
}
