using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = String.Concat("Resultado: ", "0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Actualizar()
    {
        if (!GetComponent<TMPro.TextMeshProUGUI>().enabled)
            GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
    }
}
