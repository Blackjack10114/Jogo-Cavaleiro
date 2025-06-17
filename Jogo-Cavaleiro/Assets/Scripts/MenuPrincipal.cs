using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string CenaJogar;

    public void Jogar()
    {
        SceneManager.LoadScene(CenaJogar);
    }


}
