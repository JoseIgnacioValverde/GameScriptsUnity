using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene("Dormitorio");
    }
    public void PantallaControles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void ToLaboratorio() {
		SceneManager.LoadScene("Laboratorio");
	}

	public void ToAyuntamiento() {
		SceneManager.LoadScene("Ayuntamiento");
	}
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
