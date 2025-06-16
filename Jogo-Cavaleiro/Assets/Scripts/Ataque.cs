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

    private Vector2 direcaoMovimentoAtual = Vector2.right;
    private Vector2 ultimaDirecaoAtaque = Vector2.right;

    private float tempoGizmosAtivado = 0f;
    private float duracaoGizmos = 0.15f;

    // Atualiza a direção com base no movimento (suporte a teclado, setas, controle)
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != Vector2.zero)
            direcaoMovimentoAtual = input.normalized;
    }

    // Adicione um novo método para detectar um botão específico para ataque para cima
    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ultimaDirecaoAtaque = Vector2.up;
            tempoGizmosAtivado = Time.time + duracaoGizmos;
            Atacar(Vector2.up);
        }
    }

    // Modifique o método OnAttack para permitir ataque para cima mesmo sem input direcional
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 dir = direcaoMovimentoAtual;

            // Se não houver input direcional, ataque para cima por padrão
            if (dir == Vector2.zero)
                dir = Vector2.up;
            else if (dir.y > 0.3f)
                dir = Vector2.up;
            else if (dir.x > 0.5f)
                dir = Vector2.right;
            else if (dir.x < -0.5f)
                dir = Vector2.left;
            else
                dir = Vector2.up; // padrão: para cima

            ultimaDirecaoAtaque = dir;
            tempoGizmosAtivado = Time.time + duracaoGizmos;
            Atacar(dir);
        }
    }


    private void Atacar(Vector2 direcao)
    {
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
