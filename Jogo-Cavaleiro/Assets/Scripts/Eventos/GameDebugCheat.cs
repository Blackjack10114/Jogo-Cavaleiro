using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebugCheat : MonoBehaviour
{
    private ControladorNarrativa controlador;

    void Start()
    {
        controlador = Object.FindFirstObjectByType<ControladorNarrativa>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.F1))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa0);

            if (Input.GetKeyDown(KeyCode.F2))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa1);

            if (Input.GetKeyDown(KeyCode.F3))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa2);

            if (Input.GetKeyDown(KeyCode.F4))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa3);
             if (Input.GetKeyDown(KeyCode.F5))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa4);
            if (Input.GetKeyDown(KeyCode.F6))
                controlador.ForcarFase(ControladorNarrativa.FaseJogo.Etapa5_Final);

            //if (Input.GetKeyDown(KeyCode.F6))
            //controlador.MudarParaFase(ControladorNarrativa.FaseJogo.Boss);

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
