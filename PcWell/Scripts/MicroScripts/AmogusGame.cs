using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmogusGame : MonoBehaviour
{
    static AmogusGame  Instance;
    public int points = 0;
    public CambiarCanvas controlador;

    private void Start()
    {
        controlador = FindObjectOfType<CambiarCanvas>();
    }
    public void AddPoint(){
        points++;
        if (points == 3)
        {
            StartCoroutine(cerrarJuego());
        }
    }

    private void OnDestroy()
    {
        if (points != 3)
        {
            controlador.generador.GenerarBarco(points + 1);
        }else
            controlador.jugador.ganarDinero(10f);
    }
    IEnumerator cerrarJuego()
    {
        GameObject root = this.gameObject.transform.parent.parent.gameObject;
        yield return new WaitForSeconds(0.5f);
        Destroy(root);
    }
}
