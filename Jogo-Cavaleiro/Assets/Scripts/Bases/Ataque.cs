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
            else if (direcao == Vector2.up)
                anim.SetTrigger("AtacarCima");
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
                Vida vida = inimigo.GetComponent<Vida>();
                if (vida != null)
                {
                    int vidaAntes = vida.VidaAtual();
                    bool morreuAntes = vida.Morreu;

                    // Verifica se é um piolho com laço
                    Inimigo_Piolho piolho = inimigo.GetComponent<Inimigo_Piolho>();
                    if (piolho != null && piolho.comLaco)
                    {
                        // Punição: jogador leva dano e o inimigo não morre
                        Vida vidaJogador = GetComponent<Vida>();
                        if (vidaJogador != null)
                        {
                            vidaJogador.LevarDano(1); // ou outro valor de punição
                            Debug.Log("Você atacou um piolho com laço! Tomou dano!");
                        }

                        return; // não continua, não mata o piolho
                    }

                    // Caso contrário, ataca normalmente
                    vida.LevarDano(dano);

                    if (vida.Morreu && !morreuAntes && vidaAntes > 0)
                    {
                        kills++;
                    }

                }
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
