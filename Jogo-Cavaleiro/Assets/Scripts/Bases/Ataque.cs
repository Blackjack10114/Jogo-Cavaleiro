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

    private Vector2 ultimaDirecaoAtaque = Vector2.right;
    private float tempoGizmosAtivado = 0f;
    private float duracaoGizmos = 0.15f;

    private PlayerMov playerMov;

    public bool EstaAtacando { get; private set; } = false;
    void Start()
    {
        playerMov = GetComponent<PlayerMov>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EstaAtacando = true; // <- Ativando o ataque

            Vector2 dir = Vector2.right;

            if (playerMov.mirandoParaCima)
                dir = Vector2.up;
            else if (playerMov.direcaoInput.x > 0.5f)
                dir = Vector2.right;
            else if (playerMov.direcaoInput.x < -0.5f)
                dir = Vector2.left;

            ultimaDirecaoAtaque = dir;
            tempoGizmosAtivado = Time.time + duracaoGizmos;
            Atacar(dir);
            Debug.Log($"Atacou na direção: {dir}, mirandoParaCima: {playerMov.mirandoParaCima}, direcaoInput: {playerMov.direcaoInput}");
            // Desativa o ataque após 0.2s
            Invoke(nameof(FinalizarAtaque), 0.2f);
        }
    }

    private void FinalizarAtaque()
    {
        EstaAtacando = false;
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
