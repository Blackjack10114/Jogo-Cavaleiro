using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia;
    public bool NaEsquerda, NoCentro, NaDireita;
    public Vector2 direcaoInput = Vector2.right;

    private Vector2 movimento;
    public bool mirandoParaCima = false;

    private float tempoUltimoToqueDireita = -2f;
    private float tempoUltimoToqueEsquerda = -2f;
    public float intervaloDuploToque = 2f;

    private PlayerAtaque playerAtaque;



    private void Start()
    {
        playerAtaque = GetComponent<PlayerAtaque>();
        distancia = new Vector3(8f, 0f, 0f);
        NoCentro = true;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        if (playerAtaque != null && playerAtaque.EstaAtacando)
        {
            return; // está atacando, então não movimenta para os lados
        }

        // Movimento lateral permitido mesmo mirando para cima
        float agora = Time.time;

        

        if (movimento.x > 0.5f)
        {
            if (agora - tempoUltimoToqueDireita <= intervaloDuploToque && (NoCentro || NaEsquerda))
            {
                transform.position += distancia;
                AtualizarPosicao(true);
                tempoUltimoToqueDireita = -1f; // reseta
            }
            else
            {
                tempoUltimoToqueDireita = agora;
            }
        }
        else if (movimento.x < -0.5f)
        {
            if (agora - tempoUltimoToqueEsquerda <= intervaloDuploToque && (NoCentro || NaDireita))
            {
                transform.position -= distancia;
                AtualizarPosicao(false);
                tempoUltimoToqueEsquerda = -1f; // reseta
            }
            else
            {
                tempoUltimoToqueEsquerda = agora;
            }
        }


        movimento = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            Vector2 input = context.ReadValue<Vector2>();

            mirandoParaCima = input.y > 0.3f;

            if (input.x > 0.5f)
            {
                direcaoInput = Vector2.right;
            }
            else if (input.x < -0.5f)
            {
                direcaoInput = Vector2.left;
            }
            else if (mirandoParaCima)
            {
                direcaoInput = Vector2.up;
            }

            movimento = input;
        }
    }


    private void AtualizarPosicao(bool direita)
    {
        if (NoCentro)
        {
            NoCentro = false;
            NaDireita = direita;
            NaEsquerda = !direita;
        }
        else
        {
            NoCentro = true;
            NaDireita = false;
            NaEsquerda = false;
        }
    }
}
