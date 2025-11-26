using System;
using System.Collections.Generic;
using UnityEngine;

public class Conexion : MonoBehaviour
{
    // Listas para manejar múltiples cables
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<GameObject> pointsB = new List<GameObject>();
    private List<Transform> transformers = new List<Transform>();

    // Propiedades por defecto
    public float lineWidth = 0.05f;
    public Color lineColor = Color.red;
    public Material lineMaterial;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 4;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = lineMaterial;
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.enabled = true;


        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
        }
    }

    void Update()
    {

    }
}
