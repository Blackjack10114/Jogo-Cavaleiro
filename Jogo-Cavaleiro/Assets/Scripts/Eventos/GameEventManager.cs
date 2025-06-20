using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public enum FaseJogo { Introducao, Meio, Sombria, Final, Boss }
    public FaseJogo faseAtual;

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
    }
    public void MudarParaFase(FaseJogo novaFase)
    {
        faseAtual = novaFase;

        // Atualiza checkpoint ao entrar na nova fase
        if (checkpointManager != null)
            checkpointManager.SalvarCheckpoint(faseAtual);

        switch (faseAtual)
        {
            case FaseJogo.Introducao:
                ConfigurarFase_Introducao();
                break;
            case FaseJogo.Meio:
                ConfigurarFase_Meio();
                break;
            case FaseJogo.Sombria:
                ConfigurarFase_Sombria();
                break;
            case FaseJogo.Final:
                ConfigurarFase_Final();
                break;
        }
    }


    void ConfigurarFase_Introducao()
    {
        if (TextoNarrativa.Instance != null)
            TextoNarrativa.Instance.MostrarTexto("O cavaleiro começou sua escalada...");

        spawnerPiolho.enabled = true;
        spawnerPiolho.chanceSpawn = 0.9f;
        spawnerPiolho.intervaloEntreSpawns = 2f;

        spawnerChiclete.enabled = true;
        spawnerChiclete.chanceSpawn = 0.5f;

        spawnerCavaleiro.enabled = false;
        spawnerFantasma.enabled = false;
        spawnerMorcego1.enabled = false;
        spawnerMorcego2.enabled = false;
        spawnerUnicornio.enabled = false;
        spawnerCarrinho.enabled = false;
        spawnerUrsinho.enabled = false;
        spawnerMiragem.enabled = false;
    }

    void ConfigurarFase_Meio()
    {
        TextoNarrativa.Instance?.MostrarTexto("Outros cavaleiros surgem!");

        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerCavaleiro.intervalo = 5f;

        spawnerPiolho.chanceSpawn = 0.4f;
        spawnerChiclete.chanceSpawn = 0.3f;
    }

    void ConfigurarFase_Sombria()
    {
        TextoNarrativa.Instance?.MostrarTexto("A torre ficou sombria...");

        spawnerFantasma.enabled = true;
        spawnerFantasma.chanceSpawn = 0.5f;

        spawnerMorcego1.enabled = true;
        spawnerMorcego1.chanceSpawn = 0.5f;
        spawnerMorcego2.enabled = true;
        spawnerMorcego2.chanceSpawn = 0.5f;

        spawnerMiragem.enabled = true;
        spawnerMiragem.chanceSpawn = 0.4f;

        spawnerPiolho.chanceSpawn = 0.2f;
        spawnerChiclete.chanceSpawn = 0.2f;
    }

    void ConfigurarFase_Final()
    {
        TextoNarrativa.Instance?.MostrarTexto("O caos começa!");

        spawnerPiolho.chanceSpawn = 0f;
        spawnerChiclete.enabled = false;

        spawnerCavaleiro.chanceSpawn = 0.3f;

        spawnerUnicornio.enabled = true;
        spawnerUnicornio.intervaloEntreSpawns = 4f;

        spawnerCarrinho.enabled = true;
        spawnerCarrinho.chanceSpawn = 0.3f;

        spawnerUrsinho.enabled = true;
        spawnerUrsinho.chanceSpawn = 0.4f;
    }

    void ConfigurarFase_Boss()
    {
        DesativarTodos();
        TextoNarrativa.Instance?.MostrarTexto("E então um Dragão...");
        
    }

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
