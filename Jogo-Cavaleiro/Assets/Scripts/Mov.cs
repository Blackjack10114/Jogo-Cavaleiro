using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia;
    public bool NaEsquerda, NoCentro, NaDireita;
    public Vector2 direcaoInput = Vector2.right; // Torne público para Ataque acessar
    private bool mirandoParaCima = false;

    private Vector2 movimento;

    private void Start()
    {
        distancia = new Vector3(8f, 0f, 0f);
        NoCentro = true;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        if (!mirandoParaCima) // Só permite mover se não estiver mirando para cima
        {
            if (movimento.x > 0.5f && (NoCentro || NaEsquerda))
            {
                transform.position += distancia;
                AtualizarPosicao(true);
            }
            else if (movimento.x < -0.5f && (NoCentro || NaDireita))
            {
                transform.position -= distancia;
                AtualizarPosicao(false);
            }
        }

        movimento = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.y > 0.3f)
            {
                mirandoParaCima = true;
                direcaoInput = Vector2.up;
            }
            else if (input.x > 0.5f)
            {
                mirandoParaCima = false;
                direcaoInput = Vector2.right;
            }
            else if (input.x < -0.5f)
            {
                mirandoParaCima = false;
                direcaoInput = Vector2.left;
            }
            else
            {
                mirandoParaCima = false;
            }
            movimento = input;
        }
        else if (context.canceled)
        {
            mirandoParaCima = false;
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
