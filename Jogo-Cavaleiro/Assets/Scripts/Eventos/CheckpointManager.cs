using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform jogador;
    public GameEventManager gameEventManager;

    private const string ChaveFase = "FaseSalva";
    private const string ChaveY = "PosY";

    void Start()
    {
        // Carregar checkpoint salvo
        if (PlayerPrefs.HasKey(ChaveFase))
        {
            var faseSalva = (GameEventManager.FaseJogo)PlayerPrefs.GetInt(ChaveFase);
            gameEventManager.MudarParaFase(faseSalva);

            float posY = PlayerPrefs.GetFloat(ChaveY, jogador.position.y);
            jogador.position = new Vector3(jogador.position.x, posY, jogador.position.z);
            Debug.Log("Checkpoint carregado: Fase " + faseSalva + ", Y: " + posY);

        }
    }

    public void SalvarCheckpoint(GameEventManager.FaseJogo fase)
    {
        PlayerPrefs.SetInt(ChaveFase, (int)fase);
        PlayerPrefs.SetFloat(ChaveY, jogador.position.y);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint salvo!");
    }

    public void LimparCheckpoint()
    {
        PlayerPrefs.DeleteKey(ChaveFase);
        PlayerPrefs.DeleteKey(ChaveY);
    }
}
