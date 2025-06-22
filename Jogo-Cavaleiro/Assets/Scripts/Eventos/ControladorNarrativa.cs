using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

public class ControladorNarrativa : MonoBehaviour
{
    public enum FaseJogo
    {
        Etapa0,
        Etapa1,
        Etapa2,
        Etapa3,
        Etapa4,
        Etapa5_Final
    }

    public static ControladorNarrativa Instance;
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

    /*[Header("Fundo Cenário")]
    [SerializeField] private SpriteRenderer fundoRenderer;
    [SerializeField] private Sprite fundoDia;
    [SerializeField] private Sprite fundoNoite;
    [SerializeField] private float duracaoFade = 1f;
    */

    [SerializeField] private Image fadeImage;
    [SerializeField] private float duracaoFade = 1.5f;
    [SerializeField] private string CenaFim;

    private int kills = 0;
    private int etapa = 0;
    // Mude metas para não acumulativas
    private int[] metas = new int[] { 20, 20, 20, 20, 20 }; // total 130


    private CheckpointManager checkpointManager;

    private float intervaloSpawnGeral = 3f;
    private float proximoTempoSpawn = 0f;
    //private SpawnerBase[] spawnersAtivos; // interface base que todos os spawners implementam


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DesativarTodos(); // evita spawns antes da hora

        checkpointManager = Object.FindFirstObjectByType<CheckpointManager>();

        if (checkpointManager != null && PlayerPrefs.HasKey("FaseSalva"))
        {
            faseAtual = checkpointManager.CarregarCheckpoint();
            etapa = EtapaPorFase(faseAtual);
            kills = checkpointManager.CarregarKillsSalvas();
        }
        else
        {
            // Novo jogo, sem checkpoint
            faseAtual = FaseJogo.Etapa0;
            etapa = 0;
            kills = 0;
            StartCoroutine(Etapa0()); // só roda narrativa do início se não for um jogo salvo
            return; // IMPORTANTE: evita duplicar chamadas abaixo
        }

        // Executa narrativa da fase salva
        switch (etapa)
        {
            case 0: StartCoroutine(Etapa0()); break;
            case 1: StartCoroutine(Etapa1()); break;
            case 2: StartCoroutine(Etapa2()); break;
            case 3: StartCoroutine(Etapa3()); break;
            case 4: StartCoroutine(Etapa4()); break;
            case 5: StartCoroutine(Etapa5_Final()); break;
        }
    


        // Agora, só executa a narrativa — o Fase_XXX será chamado no fim dela
        switch (etapa)
        {
            case 0: StartCoroutine(Etapa0()); break;
            case 1: StartCoroutine(Etapa1()); break;
            case 2: StartCoroutine(Etapa2()); break;
            case 3: StartCoroutine(Etapa3()); break;
            case 4: StartCoroutine(Etapa4()); break;
            case 5: StartCoroutine(Etapa5_Final()); break;
        }
    }


    /* void Update()
     {
         if (Time.time >= proximoTempoSpawn)
         {
             TentarSpawnarInimigo();
             proximoTempoSpawn = Time.time + intervaloSpawnGeral;
         }
     }
    */


  

    public void RegistrarKill()
    {
        kills++;
        Debug.Log($"[Narrativa] Etapa atual: {etapa}, Kills: {kills}");

        if (etapa == 0 && kills >= 20)
        {
            etapa++;
            StartCoroutine(Etapa1());
            return;
        }
        else if (etapa == 1 && kills >= 40)
        {
            etapa++;
            StartCoroutine(Etapa2());
            return;
        }
        else if (etapa == 2 && kills >= 80)
        {
            etapa++;
            StartCoroutine(Etapa3());
            return;
        }
        else if (etapa == 3 && kills >= 100)
        {
            etapa++;
            StartCoroutine(Etapa4());
            return;
        }
        else if (etapa == 4 && kills >= 130)
        {
            etapa++;
            StartCoroutine(Etapa5_Final());
            return;
        }
        else
        {
            Debug.Log($"[Narrativa] Etapa atual: {etapa}, Kills: {kills}");

            if (etapa < metas.Length)
            {
                int faltam = metas[etapa] - kills;
                Debug.Log($"[Narrativa] Faltam {faltam} kills para atingir a meta da Etapa {etapa} (Meta acumulada: {metas[etapa]})");
            }
        }
    }

    private IEnumerator Etapa0()
    {
        TextoNarrativa.Instance.Narrador("Mas o mago não encolhe o cavaleiro. Você não pode mudar a história assim!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Por que não? Já ouvi essa história diversas vezes! E assim fica mais divertido!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("*Argh* Enfim! O cavaleiro depois de ser encolhido pelo mago, iniciou sua escalada que agora será ainda mais longa entre os cabelos da princesa para resgatá-lá.");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Mas o cabelo da princesa estava cheio de... de ... PIOLHOS E CHICLETE! Que vão para cima do cavaleiro!");
        Fase_Etapa0();
    }

    private IEnumerator Etapa1()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("É o cabelo de uma princesa! Por que teria chiclete e piolho no cabelo dela?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Porque eu gosto de chiclete e acho piolhos legais, mas pensando bem, acho que nem todos seriam ruins, acho que deve ter alguns que são amigos. Né?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Então não pode atacar piolhos com LAÇO");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.Etapa1);
    }

    private IEnumerator Etapa2()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("Não sei onde chicletes e piolhos são legais ou que sejam amigos, mas enfim, depois de uma longa... e bastante problemática escalada, nosso cavaleiro se encontra no topo do cas-");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        //TextoNarrativa.Instance.Crianca("Mas o cavaleiro não imaginaria que teria sido uma jornada de DIAS! Ele levaria muito mais tempo para chegar no topo do castelo, anoiteceu e ele mal percebeu!");
        //AplicarFundoPorFase();
        //yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Quando ele menos percebe começa a surgir outros cavaleiros! De outros reinos! QUERENDO MATAR A PRINCESA!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Estou começando a sentir pena dessa princesa...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("*Risadinha*");
        yield return new WaitForSeconds(2f); 
        MudarParaFase(FaseJogo.Etapa2);
    }

    private IEnumerator Etapa3()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("*Respira Fundo*");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Conforme escalava, a torre ficava cada vez mais sombria, inimigos se espreitavam entre as mechas de cabelo e cercavam o cavaleiro..");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Morcegos, fantasmas e clones espelhados do cavaleiro que surgem por comando do mago, para botar um fim em sua bravura.");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Nenhuma interrupção?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Eu gosto desses monstros. *risadinha*");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("*Alívio* Finalmente...");
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.Etapa3);
    }

    private IEnumerator Etapa4()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("Eu já nem me lembro mais como era a história original... Essa história não está nem ao menos seguindo um rumo...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Como não? Estamos chegando na melhor parte!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Jura? E qual seria?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("A FESTA DO CASTELO! ");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("QUÊ?!!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("TODOS ESTAVAM ESPERANDO O CAVALEIRO PARA DAR UMA GRANDE FESTA DE COMEMORAÇÃO!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("AAAHHH!! Isso não é possível!!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("YEAAAAAAAAAAAAAAAAAA!!!!!!!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        yield return new WaitForSeconds(4f);
        MudarParaFase(FaseJogo.Etapa4);
    }
   
    private IEnumerator Etapa5_Final()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("Ok, ok, essa história já foi longe demais! Hora de ir dormir!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Ah, mas paaai. A gente estava quase na parte do dra-");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Mae("Gente que barulheira é essa? Querido, eu não te pedi para colocar nosso filho para dormir?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.NarradorPai("S-Sim, mas é que ele ficou atrapalhando a história...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("* Criança fingindo que está dormindo* ZZZZZZzzzZZ");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.NarradorPai("...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Mae("*risadinha* desliga as luzes e vá dormir você também, e então amanhã... conte uma nova história para ele.");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        etapa = 5;
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOutETrocarCena());
    }

    public void MudarParaFase(FaseJogo novaFase)
    {
        faseAtual = novaFase;
        if (checkpointManager != null)
            checkpointManager.SalvarCheckpoint(faseAtual);

        DesativarTodos();

        switch (faseAtual)
        {
            case FaseJogo.Etapa0: Fase_Etapa0(); break;
            case FaseJogo.Etapa1: Fase_Etapa1(); break;
            case FaseJogo.Etapa2: Fase_Etapa2(); break;
            case FaseJogo.Etapa3: Fase_Etapa3(); break;
            case FaseJogo.Etapa4: Fase_Etapa4(); break; 
        }
    }

    void Fase_Etapa0()
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
        spawnerPiolho.chanceDeLaco = 0f;
        spawnerPiolho.chanceSpawn = 0.75f;
        spawnerPiolho.intervaloEntreSpawns = 2.5f;

        spawnerChiclete.enabled = true;
        spawnerChiclete.chanceSpawn = 0.45f;
    }

    void Fase_Etapa1()
    {
        spawnerCavaleiro.enabled = false;
        spawnerPiolho.enabled = true;
        spawnerChiclete.enabled = true;
        spawnerPiolho.chanceSpawn = 0.9f;
        spawnerPiolho.chanceDeLaco = 0.35f;
        spawnerChiclete.chanceSpawn = 0.55f;
    }

    void Fase_Etapa2()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.8f;
        spawnerCavaleiro.chanceDeLaco = 0.1f;
        spawnerCavaleiro.intervalo = 5f;
        spawnerPiolho.enabled = true;
        spawnerChiclete.enabled = true;
        spawnerPiolho.chanceSpawn = 1f;
        spawnerPiolho.chanceDeLaco = 0.1f;
        spawnerChiclete.chanceSpawn = 0.3f;
    }
    void Fase_Etapa3()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerCavaleiro.chanceDeLaco = 0f;
        spawnerCavaleiro.intervalo = 5f;

        spawnerFantasma.enabled = true;
        spawnerFantasma.chanceSpawn = 0.5f;
        spawnerFantasma.chanceDeLaco = 0f;

        spawnerMorcego1.enabled = true;
        spawnerMorcego1.chanceSpawn = 0.5f;
        spawnerMorcego1.chanceDeLaco = 0f;
        spawnerMorcego2.enabled = true;
        spawnerMorcego2.chanceSpawn = 0.5f;
        spawnerMorcego2.chanceDeLaco = 0f;

        spawnerMiragem.enabled = true;
        spawnerMiragem.chanceSpawn = 0.4f;
        spawnerMiragem.chanceDeLaco = 0f;

        spawnerPiolho.enabled = true;
        //spawnerChiclete.enabled = true;
        spawnerPiolho.chanceSpawn = 0.2f;
        spawnerPiolho.chanceDeLaco = 0.05f;
        //spawnerChiclete.chanceSpawn = 0.3f;
    }

    void Fase_Etapa4()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.2f;

        spawnerUnicornio.enabled = true;
        spawnerUnicornio.chanceDeLaco = 0f;
        spawnerUnicornio.intervaloEntreSpawns = 4f;

        spawnerCarrinho.enabled = true;
        spawnerCarrinho.chanceSpawn = 0.6f;

        spawnerUrsinho.enabled = true;
        spawnerUrsinho.chanceSpawn = 0.6f;
        spawnerUrsinho.chanceDeLaco = 0f;
    }



    /*void Fase_Boss()
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

    public void ForcarFase(FaseJogo novaFase)
    {
        DesativarTodos();

        faseAtual = novaFase;
        etapa = EtapaPorFase(novaFase);
        kills = checkpointManager != null ? checkpointManager.CarregarKillsSalvas() : 0;

        MudarParaFase(novaFase);

        // Executa a narrativa correspondente
        switch (etapa)
        {
            case 0: StartCoroutine(Etapa0()); break;
            case 1: StartCoroutine(Etapa1()); break;
            case 2: StartCoroutine(Etapa2()); break;
            case 3: StartCoroutine(Etapa3()); break;
            case 4: StartCoroutine(Etapa4()); break;           
            case 5: StartCoroutine(Etapa5_Final()); break;  
        }

    }

    private int EtapaPorFase(FaseJogo fase)
    {
        return fase switch
        {
            FaseJogo.Etapa0 => 0,
            FaseJogo.Etapa1 => 1,
            FaseJogo.Etapa2 => 2,
            FaseJogo.Etapa3 => 3,
            FaseJogo.Etapa4 => 4,
            FaseJogo.Etapa5_Final => 5, 
            _ => 0
        };
    }



    /* void TentarSpawnarInimigo()
     {
         if (spawnersAtivos == null || spawnersAtivos.Length == 0) return;

         // Embaralha a ordem dos spawners
         var lista = new List<SpawnerBase>(spawnersAtivos);
         lista.Shuffle(); // função de extensão que você pode criar

         foreach (var spawner in lista)
         {
             if (spawner.PodeSpawnarAgora())
             {
                 spawner.Spawnar();
                 break; // só 1 por ciclo
             }
         }
     }
    */

    /* private void AplicarFundoPorFase()
     {
         Sprite novoFundo = (faseAtual == FaseJogo.Introducao || faseAtual == FaseJogo.IntroducaoAvancada) ? fundoDia : fundoNoite;
         StartCoroutine(FadeTrocaFundo(novoFundo));
     }
    */

    /* private IEnumerator FadeTrocaFundo(Sprite novoSprite)
     {
         Color corAtual = fundoRenderer.color;

         // Fade out
         for (float t = 0; t < duracaoFade; t += Time.deltaTime)
         {
             float alpha = Mathf.Lerp(1f, 0f, t / duracaoFade);
             fundoRenderer.color = new Color(corAtual.r, corAtual.g, corAtual.b, alpha);
             yield return null;
         }

         fundoRenderer.sprite = novoSprite;

         // Fade in
         for (float t = 0; t < duracaoFade; t += Time.deltaTime)
         {
             float alpha = Mathf.Lerp(0f, 1f, t / duracaoFade);
             fundoRenderer.color = new Color(corAtual.r, corAtual.g, corAtual.b, alpha);
             yield return null;
         }
     }
     */
    private IEnumerator FadeOutETrocarCena()
    {
        float duration = 8f;
        float t = 0f;

        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(CenaFim);
    }

    public int[] Metas()
    {
        return metas;
    }

    public int EtapaAtual()
    {
        return etapa;
    }

    public int Kills()
    {
        return kills;
    }

    public int FaltamParaProximaMeta()
    {
        if (etapa >= metas.Length) return 0;

        return Mathf.Max(0, metas[etapa] - KillsEtapaAtual());
    }

    private int KillsEtapaAtual()
    {
        int somaMetasAnteriores = 0;
        for (int i = 0; i < etapa; i++)
            somaMetasAnteriores += metas[i];

        return kills - somaMetasAnteriores;
    }



}

