using System;
using System.Collections.Generic;
using UnityEngine;

public class Conector : MonoBehaviour
{

    public Conector conexionFija;
    public bool inicialFixed = false;
    private bool valorBuffer = false;

    public Color modifiedColor = Color.green;
    private Color originalColor = Color.red;
    private MeshRenderer mr;

    private readonly HashSet<Conector> contactos = new HashSet<Conector>();

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        if (!TryGetComponent<Collider>(out var col) || !col.isTrigger)
            Debug.LogWarning("[Conector] Recomiendo un collider con IsTrigger=true");
        if (!GetComponentInParent<Rigidbody>() && !GetComponent<Rigidbody>())
            Debug.LogWarning("[Conector] Alguno de los dos objetos que colisionan debe tener Rigidbody");
    }

    void Update()
    {
        bool baseValor = inicialFixed;

        bool fija = conexionFija != null && conexionFija.algoQueEnviar();

        bool contactosValor = false;
        foreach (var c in contactos)
            contactosValor |= c.algoQueEnviar();

        valorBuffer = baseValor | fija | contactosValor;

        if (mr != null)
            mr.material.color = valorBuffer ? modifiedColor : originalColor;
        Debug.Log($"[{name}] base={baseValor}     |     fija={fija}  {conexionFija} |   contactosValor={contactosValor}     contactos={contactos.Count}        -> {valorBuffer}");
    }
    void OnTriggerEnter(Collider other)
    {
 
        var otroConector = other.GetComponentInParent<Conector>();

        if (otroConector == null)
        {
            Debug.Log($"[Conector] {other.name} no tiene Conector");
            return;
        }
        contactos.Add(otroConector);

    }

    void OnTriggerExit(Collider other)
    {
        var otroConector = other.GetComponentInParent<Conector>();
        if (otroConector != null && contactos.Remove(otroConector))
        {
             Debug.Log($"[Conector] - {gameObject.name} desconectado con {otroConector.name}");
        }
    }

    public virtual bool algoQueEnviar()
    {
        return valorBuffer;
    }

}
