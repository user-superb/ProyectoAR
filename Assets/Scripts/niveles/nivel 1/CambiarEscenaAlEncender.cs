using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaAlEncender : MonoBehaviour
{
    public string nombreEscenaSiguiente = "nivel 2"; // Nombre de la escena a cargar
    private IOutputProvider comp; // o el tipo de tu script de compuerta

    void Start()
    {
        comp = GetComponent<IOutputProvider>();
    }

    void Update()
    {
        if (comp != null && !comp.GetSalida()) // o comp.salida == true
        {
            Debug.Log("Compuerta encendida, cambiando de escena...");
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}
