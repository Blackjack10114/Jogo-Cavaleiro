using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia;
    public bool NaEsquerda, NoCentro, NaDireita;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distancia = new Vector3(8f, 0f, 0f);
        NoCentro = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.RightArrow) && (NoCentro || NaEsquerda))
        {
            this.transform.position = this.transform.position + distancia;
            verificarondeesta();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && (NoCentro || NaDireita))
        {
            this.transform.position = this.transform.position - distancia;
            verificarondeesta();
        }
    }
    private void verificarondeesta()
    {
        if (NoCentro && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NoCentro = false;
            NaDireita = false;
            NaEsquerda = true;
        }
        if (NoCentro && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NoCentro = false;
            NaDireita = true;
            NaEsquerda = false;
        }
        if (NaDireita && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NoCentro = true;
            NaDireita = false;
            NaEsquerda = false;
        }
        if (NaEsquerda && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NoCentro = true;
            NaDireita = false;
            NaEsquerda = false;
        }
    }
}
