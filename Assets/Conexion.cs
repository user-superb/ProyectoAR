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

    void Start()
    {
        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
        }
    }

    void Update()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            LineRenderer line = lines[i];
            GameObject pointB = pointsB[i];
            Transform transformer = transformers[i];

            if (line != null && pointB != null && transformer != null)
            {
                if (!line.enabled) line.enabled = true;

                Vector3 a = new Vector3(transformer.position.x, transformer.position.y, transformer.position.z);
                Vector3 b = pointB.transform.position;
                Vector3 inter1 = new Vector3(b.x, a.y, a.z);
                Vector3 inter2 = new Vector3(b.x, b.y, b.z);

                line.SetPosition(0, a);
                line.SetPosition(1, inter1);
                line.SetPosition(2, inter2);
                line.SetPosition(3, b);
            }
        }
    }

    // Crea un nuevo cable
    public void AgregarCable(Transform startTransform, GameObject endPoint)
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 4;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = lineMaterial;
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.enabled = true;

        // Guardar referencias en las listas
        lines.Add(line);
        pointsB.Add(endPoint);
        transformers.Add(startTransform);
    }

    // Método opcional para agregar por índice de hijo
    public void AgregarCable(int childIndex, GameObject endPoint)
    {
        Transform startTransform = transform.GetChild(childIndex);
        AgregarCable(startTransform, endPoint);
    }

public int GetCableIndex(GameObject endPoint, Transform startTransform)
{
    for (int i = 0; i < pointsB.Count; i++)
    {
        if (pointsB[i] == endPoint && transformers[i] == startTransform)
            return i;
    }
    return -1;
}

public void EliminarCable(int index)
{
    if (index < 0 || index >= lines.Count) return;

    LineRenderer line = lines[index];
    if (line != null)
    {
        Destroy(line); // destruye el componente LineRenderer
    }

    // eliminar de las listas
    lines.RemoveAt(index);
    pointsB.RemoveAt(index);
    transformers.RemoveAt(index);
}

}
