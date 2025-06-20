using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebugCheat : MonoBehaviour
{
    private GameEventManager gerenciador;

    void Start()
    {
        gerenciador = Object.FindFirstObjectByType<GameEventManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.F1))
                gerenciador.MudarParaFase(GameEventManager.FaseJogo.Introducao);

            if (Input.GetKeyDown(KeyCode.F2))
                gerenciador.MudarParaFase(GameEventManager.FaseJogo.Meio);

            if (Input.GetKeyDown(KeyCode.F3))
                gerenciador.MudarParaFase(GameEventManager.FaseJogo.Sombria);

            if (Input.GetKeyDown(KeyCode.F4))
                gerenciador.MudarParaFase(GameEventManager.FaseJogo.Final);
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }


        }
    }
}
