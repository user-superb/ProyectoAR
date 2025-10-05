using UnityEngine;
using UnityEngine.SceneManagement;
public class ganasteNivel : MonoBehaviour
{

    public string nombreEscenaSiguiente;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ganaste()
    {
        Debug.Log("[ganasteNivel] cambiando de escena...");
        SceneManager.LoadScene(nombreEscenaSiguiente);
    }
}
