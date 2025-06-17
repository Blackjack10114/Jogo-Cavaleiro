using UnityEngine;

public class SpawnerDeInimigos : MonoBehaviour
{
    public GameObject prefabInimigo;
    public float intervaloEntreSpawns = 2f;
    public float alturaSpawn = 20f;

    public float distanciaEntreLinhas = 8f;
    public float xCentro = 0f;

    private float tempoProximoSpawn = 0f;
    public enum DirecaoSpawn { Cima, Baixo }
    public DirecaoSpawn direcaoSpawn = DirecaoSpawn.Cima;

    void Update()
    {
        if (Time.time >= tempoProximoSpawn)
        {
            SpawnInimigoAleatorio();
            tempoProximoSpawn = Time.time + intervaloEntreSpawns;
        }
    }

    void SpawnInimigoAleatorio()
    {
        LinhasController.Linha linhaAleatoria = (LinhasController.Linha)Random.Range(0, 3);
        float x = LinhasController.Instance.PosicaoX(linhaAleatoria);

        float y = (direcaoSpawn == DirecaoSpawn.Cima) ? alturaSpawn : -alturaSpawn;
        Vector3 posicao = new Vector3(x, y, 0f);

        GameObject inimigo = Instantiate(prefabInimigo, posicao, Quaternion.identity);
        inimigo.name = "Inimigo_" + linhaAleatoria;

        // Define a linha e direção no script do inimigo
        InimigoLinha scriptLinha = inimigo.GetComponent<InimigoLinha>();
        if (scriptLinha != null)
        {
            scriptLinha.linhaAtual = linhaAleatoria;
            scriptLinha.direcao = (direcaoSpawn == DirecaoSpawn.Cima) ?
                                  InimigoLinha.DirecaoMovimento.Descendo :
                                  InimigoLinha.DirecaoMovimento.Subindo;
        }
    }


}
