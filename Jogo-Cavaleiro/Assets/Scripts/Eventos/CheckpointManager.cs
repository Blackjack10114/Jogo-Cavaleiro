using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform jogador;
    public ControladorNarrativa controladorNarrativa; // novo!

    private const string ChaveFase = "FaseSalva";
    private const string ChaveY = "PosY";

    void Start()
    {
        if (PlayerPrefs.HasKey(ChaveFase))
        {
            var faseSalva = (ControladorNarrativa.FaseJogo)PlayerPrefs.GetInt(ChaveFase);

            // Aplica a fase no ControladorNarrativa
            controladorNarrativa.MudarParaFase(faseSalva);

            float posY = PlayerPrefs.GetFloat(ChaveY, jogador.position.y);
            jogador.position = new Vector3(jogador.position.x, posY, jogador.position.z);

            Debug.Log("Checkpoint carregado: Fase " + faseSalva + ", Y: " + posY);
        }
    }

    public void SalvarCheckpoint(ControladorNarrativa.FaseJogo fase)
    {
        PlayerPrefs.SetInt(ChaveFase, (int)fase);
        PlayerPrefs.SetFloat(ChaveY, jogador.position.y);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint salvo!");
    }

    public ControladorNarrativa.FaseJogo CarregarCheckpoint()
    {
        if (PlayerPrefs.HasKey(ChaveFase))
            return (ControladorNarrativa.FaseJogo)PlayerPrefs.GetInt(ChaveFase);

        return ControladorNarrativa.FaseJogo.Introducao; // padrão caso não tenha nada salvo
    }

    public void LimparCheckpoint()
    {
        PlayerPrefs.DeleteKey(ChaveFase);
        PlayerPrefs.DeleteKey(ChaveY);
    }
}
