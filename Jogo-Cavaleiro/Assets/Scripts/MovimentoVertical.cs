using UnityEngine;

public class MovimentoVertical : MonoBehaviour
{
    public enum Direcao { Subindo, Descendo }
    public Direcao direcao = Direcao.Subindo;
    public float velocidade = 2f;

    void Update()
    {
        Vector3 dir = direcao == Direcao.Subindo ? Vector3.up : Vector3.down;
        transform.Translate(dir * velocidade * Time.deltaTime);
    }
}
