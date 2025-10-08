using UnityEngine;

public class electro : Conector
{
    void Start()
    {
        inicialFixed = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool algoQueEnviar()
    {
        return true;
    }
}
