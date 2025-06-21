using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public enum FaseJogo { Introducao, IntroducaoAvancada, Meio, MeioAvancado, Final } //Boss
    public FaseJogo faseAtual;
    public GameObject panelTexto;

    [Header("Spawners")]
    public SpawnerPiolho spawnerPiolho;
    public SpawnerChiclete spawnerChiclete;
    public Spawner_Cavaleiro spawnerCavaleiro;
    public SpawnerFantasma spawnerFantasma;
    public SpawnerMorcego spawnerMorcego1;
    public SpawnerMorcego spawnerMorcego2;
    public SpawnerUnicornio spawnerUnicornio;
    public SpawnerCarrinhoAlien spawnerCarrinho;
    public SpawnerUrsinho spawnerUrsinho;
    public Spawner_Miragem spawnerMiragem;

    private CheckpointManager checkpointManager;

    private void Start()
    {
        checkpointManager = Object.FindFirstObjectByType<CheckpointManager>();
        MudarParaFase(faseAtual);
    }

    public void MudarParaFase(FaseJogo novaFase)
    {
        faseAtual = novaFase;

        if (checkpointManager != null)
            

        DesativarTodos();

        switch (faseAtual)
        {
            case FaseJogo.Introducao: ConfigurarFase_Introducao(); break;
            case FaseJogo.IntroducaoAvancada: ConfigurarFase_IntroducaoAvancada(); break;
            case FaseJogo.Meio: ConfigurarFase_Meio(); break;
            case FaseJogo.MeioAvancado: ConfigurarFase_MeioAvancado(); break;
            case FaseJogo.Final: ConfigurarFase_Final(); break;
            //case FaseJogo.Boss: ConfigurarFase_Boss(); break;
        }
    }

    void ConfigurarFase_Introducao()
    {
        spawnerCarrinho.enabled = false;
        spawnerCavaleiro.enabled = false;
        spawnerFantasma.enabled = false;
        spawnerMiragem.enabled = false;
        spawnerMorcego1.enabled = false;
        spawnerMorcego2.enabled = false;
        spawnerUnicornio.enabled = false;
        spawnerUrsinho.enabled = false;
        
        spawnerPiolho.enabled = true;
        spawnerPiolho.chanceSpawn = 0.9f;
        spawnerPiolho.intervaloEntreSpawns = 2f;

        spawnerChiclete.enabled = true;
        spawnerChiclete.chanceSpawn = 0.5f;
    }

    void ConfigurarFase_IntroducaoAvancada()
    {
        spawnerPiolho.chanceSpawn = 0.9f;
        spawnerChiclete.chanceSpawn = 0.5f;
    }

    void ConfigurarFase_Meio()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerCavaleiro.intervalo = 5f;

        spawnerPiolho.chanceSpawn = 0.4f;
        spawnerChiclete.chanceSpawn = 0.3f;
    }

    void ConfigurarFase_MeioAvancado()
    {
        spawnerFantasma.enabled = true;
        spawnerFantasma.chanceSpawn = 0.5f;

        spawnerMorcego1.enabled = true;
        spawnerMorcego1.chanceSpawn = 0.5f;
        spawnerMorcego2.enabled = true;
        spawnerMorcego2.chanceSpawn = 0.5f;

        spawnerMiragem.enabled = true;
        spawnerMiragem.chanceSpawn = 0.4f;

        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerPiolho.chanceSpawn = 0.2f;
        spawnerChiclete.chanceSpawn = 0.2f;
    }

    void ConfigurarFase_Final()
    {
        spawnerPiolho.chanceSpawn = 0f;
        spawnerChiclete.enabled = false;

        spawnerCavaleiro.chanceSpawn = 0.2f;

        spawnerUnicornio.enabled = true;
        spawnerUnicornio.intervaloEntreSpawns = 4f;

        spawnerCarrinho.enabled = true;
        spawnerCarrinho.chanceSpawn = 0.3f;

        spawnerUrsinho.enabled = true;
        spawnerUrsinho.chanceSpawn = 0.4f;
    }

    /*void ConfigurarFase_Boss()
    {
        DesativarTodos();
        TextoNarrativa.Instance?.MostrarTexto("E então um Dragão...");
    }
    */
    public void DesativarTodos()
    {
        spawnerPiolho.enabled = false;
        spawnerChiclete.enabled = false;
        spawnerCavaleiro.enabled = false;
        spawnerFantasma.enabled = false;
        spawnerMorcego1.enabled = false;
        spawnerMorcego2.enabled = false;
        spawnerUnicornio.enabled = false;
        spawnerCarrinho.enabled = false;
        spawnerUrsinho.enabled = false;
        spawnerMiragem.enabled = false;
    }
}
