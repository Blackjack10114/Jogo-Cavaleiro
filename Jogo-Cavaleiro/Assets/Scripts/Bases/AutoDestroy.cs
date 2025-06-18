using UnityEngine;

public class AutoDestroyPorDistancia : MonoBehaviour
{
    public float distanciaMaxima = 100f;
    private Transform alvo;

    void Start()
    {
        alvo = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (alvo == null) return;

        float distancia = Vector2.Distance(transform.position, alvo.position);
        if (distancia > distanciaMaxima)
        {
            Destroy(gameObject);
        }
    }
}
