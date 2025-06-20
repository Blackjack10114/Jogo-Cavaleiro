using UnityEngine;

public class Unicornio : MonoBehaviour
{
    public float velocidadeVertical;
    public float velocidadeHorizontal = 5f;
    public float intervaloTrocaLinha;
    private float proximaTroca;

    public LinhasController.Linha linhaAtual;
    private float targetX;

    // coisas do laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;
    public float AutoDestruircomlaco;


    private int trocaDirecao = 1;

    private void Start()
    {
        targetX = LinhasController.Instance.PosicaoX(linhaAtual);
        transform.position = new Vector3(targetX, transform.position.y, 0f);
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");
    }

    void Update()
    {
        // instancia o laço
        if (comlaco && !lacoinsta)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            lacoinsta = true;
            laco.transform.parent = this.transform;
        }
        // Movimento vertical constante
        transform.Translate(Vector3.up * velocidadeVertical * Time.deltaTime);

        // Movimento lateral suave
        Vector3 posAtual = transform.position;
        Vector3 posDesejada = new Vector3(targetX, posAtual.y, posAtual.z);
        transform.position = Vector3.MoveTowards(posAtual, posDesejada, velocidadeHorizontal * Time.deltaTime);

        // Trocar linha periodicamente
        if (Time.time >= proximaTroca)
        {
            TrocarLinha();
            proximaTroca = Time.time + intervaloTrocaLinha;
        }
       

    }

    private void TrocarLinha()
    {
        if (linhaAtual == LinhasController.Linha.Centro)
        {
            linhaAtual = trocaDirecao > 0 ? LinhasController.Linha.Direita : LinhasController.Linha.Esquerda;
        }
        else
        {
            linhaAtual = LinhasController.Linha.Centro;
            trocaDirecao *= -1;
        }

        // Define o novo alvo X, sem alterar a posição imediatamente
        targetX = LinhasController.Instance.PosicaoX(linhaAtual);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !comlaco)
        {
            Vida vida = other.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(1);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Player") && comlaco) return;
    }
}
