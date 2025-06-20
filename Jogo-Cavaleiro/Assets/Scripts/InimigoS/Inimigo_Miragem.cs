using UnityEngine;

public class Inimigo_Miragem : MonoBehaviour
{
    private GameObject Player;
    private Vector3 posicaomiragem;
    public int dano = 1;
    // coisas do laço
    public float chancelaco = 0.3f;
    public bool comlaco;
    private bool lacoinsta;
    private GameObject prefabLaco;
    public float AutoDestruircomlaco;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        // verificar se tem laço
        if (Random.value < chancelaco)
        {
            comlaco = true;
        }
        prefabLaco = Resources.Load<GameObject>("Laco_Rosa");
    }
    private void Update()
    {
        // instancia o laço
        if (comlaco && !lacoinsta)
        {
            GameObject laco = Instantiate(prefabLaco, transform.position, Quaternion.identity);
            lacoinsta = true;
            laco.transform.parent = this.transform;
        }
        posicaomiragem = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        this.transform.position = posicaomiragem;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !comlaco)
        {
            Vida vidaJogador = collision.GetComponent<Vida>();
            if (vidaJogador != null)
            {
                vidaJogador.LevarDano(dano);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Player") && comlaco)
        {
                Destroy(gameObject);
        }
    }
}
