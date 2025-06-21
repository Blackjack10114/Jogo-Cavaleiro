using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform jogador;
    public ControladorNarrativa controladorNarrativa;

    private const string ChaveFase = "FaseSalva";
    private const string ChaveY = "PosY";
    private const string ChaveKills = "KillsSalvas"; // constante nome da chave

    void Start()
    {
        if (PlayerPrefs.HasKey(ChaveFase))
        {
            var faseSalva = (ControladorNarrativa.FaseJogo)PlayerPrefs.GetInt(ChaveFase);

            // Aplica a fase no ControladorNarrativa
            controladorNarrativa.MudarParaFase(faseSalva);

            // Carrega Y salvo e define X como Centro
            float posY = PlayerPrefs.GetFloat(ChaveY, jogador.position.y);
            Vector3 pos = jogador.position;
            jogador.position = new Vector3(LinhasController.Instance.PosicaoX(LinhasController.Linha.Centro), posY, pos.z);

            Debug.Log("Checkpoint carregado: Fase " + faseSalva + ", Y: " + posY);
        }
    }

    public void SalvarCheckpoint(ControladorNarrativa.FaseJogo fase)
    {
        PlayerPrefs.SetInt(ChaveFase, (int)fase);

        // Salva kills equivalentes à meta da fase
        int killsSalvas = fase switch
        {
            ControladorNarrativa.FaseJogo.Introducao => 0,
            ControladorNarrativa.FaseJogo.IntroducaoAvancada => 5,
            ControladorNarrativa.FaseJogo.Meio => 20,
            ControladorNarrativa.FaseJogo.MeioAvancado => 30,
            ControladorNarrativa.FaseJogo.ComecoFinal => 50,
            ControladorNarrativa.FaseJogo.Final => 60,
            _ => 0
        };
        PlayerPrefs.SetInt(ChaveKills, killsSalvas);

        // Salva a posição vertical atual do jogador
        if (jogador != null)
            PlayerPrefs.SetFloat(ChaveY, jogador.position.y);

        PlayerPrefs.Save();
    }

    public ControladorNarrativa.FaseJogo CarregarCheckpoint()
    {
        if (PlayerPrefs.HasKey(ChaveFase))
            return (ControladorNarrativa.FaseJogo)PlayerPrefs.GetInt(ChaveFase);

        return ControladorNarrativa.FaseJogo.Introducao; // padrão
    }

    public int CarregarKillsSalvas()
    {
        return PlayerPrefs.GetInt(ChaveKills, 0);
    }

    public void LimparCheckpoint()
    {
        PlayerPrefs.DeleteKey(ChaveFase);
        PlayerPrefs.DeleteKey(ChaveY);
        PlayerPrefs.DeleteKey(ChaveKills);
    }
}
