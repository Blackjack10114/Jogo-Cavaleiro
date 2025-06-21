using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    public float velocidade = 2f;
    private Vector3 distancia;
    public bool NaEsquerda, NoCentro, NaDireita;
    public Vector2 direcaoInput = Vector2.right;

    private Vector2 movimento;

    private bool inputReset = true;


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
        if (PauseController.JogoPausado) return;


        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        if (playerAtaque != null && playerAtaque.EstaAtacando)
        {
            return; 
        }

       
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



        movimento = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            Vector2 input = context.ReadValue<Vector2>();

            // Se estiver no centro, libera trocar de lado
            if (Mathf.Abs(input.x) < 0.3f)
            {
                inputReset = true;
            }

            // Só permite mudar de direção se soltou antes (inputReset = true)
            if (inputReset)
            {
                if (input.x > 0.5f)
                {
                    direcaoInput = Vector2.right;
                    inputReset = false;
                }
                else if (input.x < -0.5f)
                {
                    direcaoInput = Vector2.left;
                    inputReset = false;
                }
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
        GetComponent<SomPlayer>()?.Tocar(GetComponent<SomPlayer>().somTrocarLinha);

    }
}
