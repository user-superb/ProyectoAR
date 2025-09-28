using System;
using UnityEngine;

public class UpdaterLineRenderer : MonoBehaviour
{
    // Objeto destino al cual vamos a dibujarle la línea
    public GameObject pointB;

    // Bandera para activar/desactivar la línea
    public Boolean habilitarLinea = false;

    // Referencia al transform hijo y al componente LineRenderer
    private Transform transformer;
    private LineRenderer line;

    // Se ejecuta al iniciar (antes del primer Update)
    void Start()
    {
        // Busca el componente LineRenderer en el mismo objeto
        line = GetComponent<LineRenderer>();

        // La línea va a tener 4 puntos (A → inter1 → inter2 → B)
        line.positionCount = 4;

        // Grosor de la línea
        line.startWidth = (float)0.01;
        line.endWidth = (float)0.01;

        // Corrección para el editor (Unity a veces ignora cast a float sin la 'f')
        #if UNITY_EDITOR
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
        #endif

        // Toma el segundo hijo de este objeto (índice 1) como punto A de inicio
        transformer = transform.GetChild(1);
    }

    // Se ejecuta una vez por frame
    void Update()
    {
        if (habilitarLinea) // Si la línea está habilitada
        {
            // Si el LineRenderer no está visible → lo activamos
            if (!line.isVisible)
                line.enabled = true;

            // Solo dibujar si existe el punto B
            if (pointB != null)
            {
                // Posición del punto A (transformer)
                Vector3 a = new Vector3(transformer.position.x,
                                        transformer.position.y,
                                        transformer.position.z);

                // Posición del punto B
                Vector3 b = pointB.transform.position;

                // Calcula dos puntos intermedios para que la línea sea "esquinada":
                // inter1: se mueve en X hasta la posición de B, manteniendo Y y Z de A
                Vector3 inter1 = new Vector3(b.x, a.y, a.z);

                // inter2: baja/sube en Y hasta la posición de B, pero ya con X y Z de B
                Vector3 inter2 = new Vector3(b.x, b.y, b.z);

                // Define los 4 puntos en el LineRenderer
                line.SetPosition(0, a);       // inicio (A)
                line.SetPosition(1, inter1);  // primer codo (X)
                line.SetPosition(2, inter2);  // segundo codo (Y)
                line.SetPosition(3, b);       // final (B)
            }
        }
        else
        {
            // "Peligroso": esto desactiva la línea si estaba visible
            if (line.isVisible)
                line.enabled = false;
        }
    }

    // Devuelve si la línea está activa o no
    public bool activo()
    {
        return habilitarLinea;
    }

    // Método para actualizar el punto B dinámicamente
    public void actualizarPuntoB(GameObject b)
    {
        // Si el nuevo punto es null → deshabilita la línea
        if (b == null)
            habilitarLinea = false;
        else
            habilitarLinea = true;

        // Guarda la nueva referencia
        pointB = b;
    }
}
