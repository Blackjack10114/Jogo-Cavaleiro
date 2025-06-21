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
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.Introducao);

            if (Input.GetKeyDown(KeyCode.F2))
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.IntroducaoAvancada);

            if (Input.GetKeyDown(KeyCode.F3))
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.Meio);

            if (Input.GetKeyDown(KeyCode.F4))
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.MeioAvancado);

            if (Input.GetKeyDown(KeyCode.F5))
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.ComecoFinal);
            if (Input.GetKeyDown(KeyCode.F6))
                controlador.MudarParaFase(ControladorNarrativa.FaseJogo.Final);

            //if (Input.GetKeyDown(KeyCode.F7))
            //controlador.MudarParaFase(ControladorNarrativa.FaseJogo.Boss);

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
