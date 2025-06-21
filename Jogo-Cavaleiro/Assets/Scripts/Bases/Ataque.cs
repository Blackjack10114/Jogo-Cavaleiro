using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtaque : MonoBehaviour
{
    public float alcanceAtaque = 1f;
    public int dano = 1;
    public LayerMask inimigoLayer;

    public Transform pontoAtaqueCima;
    public Transform pontoAtaqueDireita;
    public Transform pontoAtaqueEsquerda;

    public float tempoEntreAtaques = 0.4f;
    private float proximoAtaquePermitido = 0f;


    private Vector2 ultimaDirecaoAtaque = Vector2.right;
    private float tempoGizmosAtivado = 0f;
    private float duracaoGizmos = 0.15f;
    public int kills;

    private Animator anim;

    public bool EstaAtacando { get; private set; } = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FinalizarAtaque()
    {
        EstaAtacando = false;
    }

    // Chamada via InputAction: AttackUp
    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (PauseController.JogoPausado) return;

        if (context.performed)
            AtacarComDirecao(Vector2.up);
    }

    // Chamada via InputAction: AttackRight
    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (PauseController.JogoPausado) return;

        if (context.performed)
            AtacarComDirecao(Vector2.right);
    }

    // Chamada via InputAction: AttackLeft
    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (PauseController.JogoPausado) return;


        if (context.performed)
            AtacarComDirecao(Vector2.left);
    }

    private void AtacarComDirecao(Vector2 direcao)
    {
        if (PauseController.JogoPausado) return;

        if (Time.time < proximoAtaquePermitido)
            return; // está em cooldown

        proximoAtaquePermitido = Time.time + tempoEntreAtaques;

        EstaAtacando = true;
        ultimaDirecaoAtaque = direcao;
        tempoGizmosAtivado = Time.time + duracaoGizmos;

        Atacar(direcao);

        // Dispara a animação certa conforme direção
        if (anim != null)
        {
            if (direcao == Vector2.right)
                anim.SetTrigger("AtacarDireita");
            else if (direcao == Vector2.left)
                anim.SetTrigger("AtacarEsquerda");
        }


        Invoke(nameof(FinalizarAtaque), 0.2f);
        GetComponent<SomPlayer>()?.TocarAtaque();

    }


    private void Atacar(Vector2 direcao)
    {
        if (PauseController.JogoPausado) return;


        Transform pontoDeAtaque = null;

        if (direcao == Vector2.up)
            pontoDeAtaque = pontoAtaqueCima;
        else if (direcao == Vector2.right)
            pontoDeAtaque = pontoAtaqueDireita;
        else if (direcao == Vector2.left)
            pontoDeAtaque = pontoAtaqueEsquerda;

        if (pontoDeAtaque != null)
        {
            Collider2D[] inimigos = Physics2D.OverlapCircleAll(pontoDeAtaque.position, alcanceAtaque, inimigoLayer);
            foreach (Collider2D inimigo in inimigos)
            {
                inimigo.GetComponent<Vida>()?.LevarDano(dano);
                kills++;
            }

            Debug.Log("Atacou em direção: " + direcao);
        }
    }

    private void OnDrawGizmos()
    {
        if (Time.time > tempoGizmosAtivado) return;

        Gizmos.color = Color.red;

        if (ultimaDirecaoAtaque == Vector2.up && pontoAtaqueCima != null)
            Gizmos.DrawWireSphere(pontoAtaqueCima.position, alcanceAtaque);
        else if (ultimaDirecaoAtaque == Vector2.right && pontoAtaqueDireita != null)
            Gizmos.DrawWireSphere(pontoAtaqueDireita.position, alcanceAtaque);
        else if (ultimaDirecaoAtaque == Vector2.left && pontoAtaqueEsquerda != null)
            Gizmos.DrawWireSphere(pontoAtaqueEsquerda.position, alcanceAtaque);
    }
}
