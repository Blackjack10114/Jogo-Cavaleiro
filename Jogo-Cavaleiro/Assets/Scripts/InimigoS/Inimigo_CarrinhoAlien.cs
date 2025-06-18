using UnityEngine;

public class CarrinhoAlien : MonoBehaviour
{
    public float velocidade = 15f;
    public float tempoAntesDeDisparar = 1.5f;
    public Vector2 direcao = Vector2.down;
    public int dano = 1;

    private bool podeMover = false;

    void Start()
    {
        StartCoroutine(AguardarAntesDeDisparar());
    }

    void Update()
    {
        if (podeMover)
        {
            transform.Translate(direcao * velocidade * Time.deltaTime);
        }
        else
        {
            // Tremor leve 
            float tremor = Mathf.Sin(Time.time * 50f) * 0.05f;
            transform.position += new Vector3(tremor, 0f, 0f) * Time.deltaTime;
        }
    }

    System.Collections.IEnumerator AguardarAntesDeDisparar()
    {
        yield return new WaitForSeconds(tempoAntesDeDisparar);
        podeMover = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vida vida = other.GetComponent<Vida>();
            if (vida != null)
            {
                vida.LevarDano(dano);
            }

            Destroy(gameObject);
        }
    }
}
