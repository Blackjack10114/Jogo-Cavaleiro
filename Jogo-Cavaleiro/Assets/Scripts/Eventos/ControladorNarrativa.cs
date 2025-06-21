using UnityEngine;
using System.Collections;

public class ControladorNarrativa : MonoBehaviour
{
    public enum FaseJogo
    {
        Introducao,
        IntroducaoAvancada,
        Meio,
        MeioAvancado,
        ComecoFinal,
        Final
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

    [Header("Fundo Cenário")]
    [SerializeField] private SpriteRenderer fundoRenderer;
    [SerializeField] private Sprite fundoDia;
    [SerializeField] private Sprite fundoNoite;
    [SerializeField] private float duracaoFade = 1f;

    private int kills = 0;
    private int etapa = 0;
    private int[] metas = new int[] { 5, 20, 30, 50, 60 };

    private CheckpointManager checkpointManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        checkpointManager = Object.FindFirstObjectByType<CheckpointManager>();
        if (checkpointManager != null)
            faseAtual = checkpointManager.CarregarCheckpoint();

        kills = checkpointManager != null ? checkpointManager.CarregarKillsSalvas() : 0;

        MudarParaFase(faseAtual);

        switch (faseAtual)
        {
            case FaseJogo.Introducao:
                etapa = 0;
                kills = 0; // começa zerado
                StartCoroutine(IntroducaoNarrativa());
                break;

            case FaseJogo.IntroducaoAvancada:
                etapa = 1;
                kills = 5; 
                StartCoroutine(Etapa0());
                break;

            case FaseJogo.Meio:
                etapa = 2;
                kills = 20;
                StartCoroutine(Etapa1());
                break;

            case FaseJogo.MeioAvancado:
                etapa = 3;
                kills = 30;
                StartCoroutine(Etapa2());
                break;

            case FaseJogo.ComecoFinal:
                etapa = 4;
                kills = 50;
                StartCoroutine(Etapa3());
                break;

            case FaseJogo.Final:
                etapa = 5;
                kills = 60;
                StartCoroutine(Etapa4Final());
                break;
        }

    }


    private IEnumerator IntroducaoNarrativa()
    {
        TextoNarrativa.Instance.Narrador("Mas o mago não encolhe o cavaleiro. Você não pode mudar a história assim!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Por que não? Já ouvi essa história diversas vezes! E assim fica mais divertido!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("*Argh* Enfim! O cavaleiro depois de ser encolhido pelo mago, iniciou sua escalada que agora será ainda mais longa entre os cabelos da princesa para resgatá-lá.");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Mas o cabelo da princesa estava cheio de... de ... PIOLHOS E CHICLETE! Que vão para cima do cavaleiro!");
        Fase_Introducao();
    }

    public void RegistrarKill()
    {
        kills++;
        Debug.Log($"Kills: {kills}");

        if (etapa == 0 && kills >= 5)
        {
            etapa++;
            StartCoroutine(Etapa0());
        }
        else if (etapa == 1 && kills >= 20)
        {
            etapa++;
            StartCoroutine(Etapa1());
        }
        else if (etapa == 2 && kills >= 30)
        {
            etapa++;
            StartCoroutine(Etapa2());
        }
        else if (etapa == 3 && kills >= 50)
        {
            etapa++;
            StartCoroutine(Etapa3());
        }
        else if (etapa == 4 && kills >= 60)
        {
            etapa++;
            StartCoroutine(Etapa4Final());
        }
        else
        {
            if (etapa < metas.Length)
            {
                int faltam = metas[etapa] - kills;
                Debug.Log($"Faltam {faltam} kills para a próxima etapa.");
            }
        }
    }

    private IEnumerator Etapa0()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("É o cabelo de uma princesa! Por que teria chiclete e piolho no cabelo dela?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Porque eu gosto de chiclete e acho piolhos legais, mas pensando bem, acho que nem todos seriam ruins, acho que deve ter alguns que são amigos. Né?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.IntroducaoAvancada);
    }

    private IEnumerator Etapa1()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("Não sei onde chicletes e piolhos são legais ou que sejam amigos, mas enfim, depois de uma longa... e bastante problemática escalada, nosso cavaleiro se encontra no topo do cas-");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Mas o cavaleiro não imaginaria que teria sido uma jornada de DIAS! Ele levaria muito mais tempo para chegar no topo do castelo, anoiteceu e ele mal percebeu!");
        AplicarFundoPorFase();
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Quando ele menos percebe começa a surgir outros cavaleiros! De outros reinos! QUERENDO MATAR A PRINCESA!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Estou começando a sentir pena dessa princesa...");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("*Risadinha*");
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.Meio);
    }

    private IEnumerator Etapa2()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("*Respira Fundo* Conforme escalava, a torre ficava cada vez mais sombria, inimigos se espreitavam entre as mechas de cabelo e cercavam o cavaleiro, morcegos, fantasmas e clones espelhados do cavaleiro que surgem por comando do mago, para botar um fim em sua bravura.");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("Nenhuma interrupção?");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("Eu gosto desses monstros. *risadinha*");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("*Alívio* Finalmente...");
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.MeioAvancado);
    }

    private IEnumerator Etapa3()
    {
        DesativarTodos();
        TextoNarrativa.Instance.Narrador("A FESTA DO CASTELO COMEÇOU!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("TODOS ESTAVAM ESPERANDO O CAVALEIRO PARA DAR UMA GRANDE FESTA DE COMEMORAÇÃO!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Narrador("QUÊ?!!");
        yield return new WaitUntil(() => !TextoNarrativa.Instance.EstaMostrandoTexto());
        TextoNarrativa.Instance.Crianca("YEAAAAAAA");
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.ComecoFinal);
    }

    private IEnumerator Etapa4Final()
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
        TextoNarrativa.Instance.Mae("*risadinha* desliga as luzes e vá dormir você também, e então amanhã... conte uma nova história para ele.");
        yield return new WaitForSeconds(2f);
        MudarParaFase(FaseJogo.Final);
    }

    public void MudarParaFase(FaseJogo novaFase)
    {
        faseAtual = novaFase;
        if (checkpointManager != null)
            checkpointManager.SalvarCheckpoint(faseAtual);

        DesativarTodos();

        switch (faseAtual)
        {
            case FaseJogo.Introducao: Fase_Introducao(); break;
            case FaseJogo.IntroducaoAvancada: Fase_IntroducaoAvancada(); break;
            case FaseJogo.Meio: Fase_Meio(); break;
            case FaseJogo.MeioAvancado: Fase_MeioAvancado(); break;
            case FaseJogo.ComecoFinal: Fase_Final(); break;
            case FaseJogo.Final: DesativarTodos(); break;
        }
    }

void Fase_Introducao()
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
        spawnerPiolho.intervaloEntreSpawns = 2f;

        spawnerChiclete.enabled = true;
        spawnerChiclete.chanceSpawn = 0.45f;
    }

    void Fase_IntroducaoAvancada()
    {
        spawnerPiolho.enabled = true;
        spawnerChiclete.enabled = true;
        spawnerPiolho.chanceSpawn = 0.9f;
        spawnerPiolho.chanceDeLaco = 0.3f;
        spawnerChiclete.chanceSpawn = 0.55f;
    }

    void Fase_Meio()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerCavaleiro.chanceDeLaco = 0.1f;
        spawnerCavaleiro.intervalo = 5f;

        spawnerPiolho.enabled = true;
        spawnerChiclete.enabled = true;
        spawnerPiolho.chanceSpawn = 0.4f;
        spawnerPiolho.chanceDeLaco = 0.09f;
        spawnerChiclete.chanceSpawn = 0.3f;
    }

    void Fase_MeioAvancado()
    {
        spawnerFantasma.enabled = true;
        spawnerFantasma.chanceSpawn = 0.5f;
        spawnerFantasma.chanceDeLaco = 0.1f;

        spawnerMorcego1.enabled = true;
        spawnerMorcego1.chanceSpawn = 0.5f;
        spawnerMorcego1.chanceDeLaco = 0.1f;
        spawnerMorcego2.enabled = true;
        spawnerMorcego2.chanceSpawn = 0.5f;
        spawnerMorcego2.chanceDeLaco = 0.1f;

        spawnerMiragem.enabled = true;
        spawnerMiragem.chanceSpawn = 0.4f;
        spawnerMiragem.chanceDeLaco = 0.1f;

        spawnerCavaleiro.enabled = true;
        spawnerPiolho.enabled = true;
        spawnerChiclete.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.6f;
        spawnerPiolho.chanceSpawn = 0.2f;
        spawnerChiclete.chanceSpawn = 0.2f;
    }

    void Fase_Final()
    {
        spawnerCavaleiro.enabled = true;
        spawnerCavaleiro.chanceSpawn = 0.2f;

        spawnerUnicornio.enabled = true;
        spawnerUnicornio.chanceDeLaco = 0.1f;
        spawnerUnicornio.intervaloEntreSpawns = 4f;

        spawnerCarrinho.enabled = true;
        spawnerCarrinho.chanceSpawn = 0.3f;

        spawnerUrsinho.enabled = true;
        spawnerUrsinho.chanceSpawn = 0.4f;
        spawnerUrsinho.chanceDeLaco = 0.1f;
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
        faseAtual = novaFase;
        etapa = EtapaPorFase(novaFase);
        kills = metas[Mathf.Clamp(etapa - 1, 0, metas.Length - 1)];

        MudarParaFase(novaFase);

        // Executa a narrativa correspondente
        switch (etapa)
        {
            case 0: StartCoroutine(IntroducaoNarrativa()); break;
            case 1: StartCoroutine(Etapa0()); break;
            case 2: StartCoroutine(Etapa1()); break;
            case 3: StartCoroutine(Etapa2()); break;
            case 4: StartCoroutine(Etapa3()); break;
            case 5: StartCoroutine(Etapa4Final()); break;
        }
    }

    private int EtapaPorFase(FaseJogo fase)
    {
        return fase switch
        {
            FaseJogo.Introducao => 0,
            FaseJogo.IntroducaoAvancada => 1,
            FaseJogo.Meio => 2,
            FaseJogo.MeioAvancado => 3,
            FaseJogo.ComecoFinal => 4,
            FaseJogo.Final => 5,
            _ => 0
        };
    }

    private void AplicarFundoPorFase()
    {
        Sprite novoFundo = (faseAtual == FaseJogo.Introducao || faseAtual == FaseJogo.IntroducaoAvancada) ? fundoDia : fundoNoite;
        StartCoroutine(FadeTrocaFundo(novoFundo));
    }

    private IEnumerator FadeTrocaFundo(Sprite novoSprite)
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

}
