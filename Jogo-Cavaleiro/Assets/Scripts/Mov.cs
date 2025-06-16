using UnityEngine;
using UnityEngine.InputSystem; // <- necessário para usar o novo Input System

public class PlayerMov : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia;
    public bool NaEsquerda, NoCentro, NaDireita;

    private Vector2 movimento; // <- entrada vinda do Input System

    private void Start()
    {
        distancia = new Vector3(8f, 0f, 0f);
        NoCentro = true;
    }

    private void Update()
    {
        
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        
        if (movimento.x > 0.5f && (NoCentro || NaEsquerda))
        {
            transform.position += distancia;
            AtualizarPosicao(true); // direita
        }
        else if (movimento.x < -0.5f && (NoCentro || NaDireita))
        {
            transform.position -= distancia;
            AtualizarPosicao(false); // esquerda
        }

  
        movimento = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movimento = context.ReadValue<Vector2>();
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
