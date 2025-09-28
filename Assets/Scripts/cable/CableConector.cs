using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CableConector : MonoBehaviour
{
    public Transform puntoA;          // Asigná TipA
    public Transform puntoB;          // Asigná TipB

    [Header("Curva (opcional)")]
    public bool usarCurva = false;    // ON = cable con “panza”
    [Range(3, 64)] public int segmentos = 16;
    public float sag = 0.02f;         // “caída” de la panza (metros)

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.enabled = true;
    }

    void LateUpdate()
    {
        if (puntoA == null || puntoB == null)
        {
            line.positionCount = 0;
            return;
        }

        if (!usarCurva)
        {
            // Cable recto (2 puntos)
            if (line.positionCount != 2) line.positionCount = 2;
            line.SetPosition(0, puntoA.position);
            line.SetPosition(1, puntoB.position);
        }
        else
        {
            // Cable con “panza” (curva Bezier simple)
            if (segmentos < 3) segmentos = 3;
            line.positionCount = segmentos;

            Vector3 a = puntoA.position;
            Vector3 b = puntoB.position;
            Vector3 medio = (a + b) * 0.5f;

            // Agregamos “sag” hacia abajo (mundo Y-)
            Vector3 control = medio + Vector3.down * sag;

            for (int i = 0; i < segmentos; i++)
            {
                float t = i / (segmentos - 1f);
                // Bezier cuadrática: Lerp(Lerp(a, control, t), Lerp(control, b, t), t)
                Vector3 p1 = Vector3.Lerp(a, control, t);
                Vector3 p2 = Vector3.Lerp(control, b, t);
                Vector3 p = Vector3.Lerp(p1, p2, t);
                line.SetPosition(i, p);
            }
        }
    }
}
