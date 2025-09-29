using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CableConector : MonoBehaviour
{
    [Header("Extremos")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Curva (opcional)")]
    public bool usarCurva = false;
    [Range(3, 64)] public int segmentos = 16;
    public float sag = 0.02f;

    [Header("Dirección: Gradiente")]
    public bool usarGradiente = true;
    public Color colorInicio = Color.green;  // A
    public Color colorFin = Color.red;       // B

    [Header("Dirección: Flecha")]
    public bool usarFlecha = true;
    public GameObject flechaPrefab;          // un cono/flechita unlit
    public float flechaEscala = 1f;
    public float flechaOffset = 0.01f;       // separa la flecha del final del cable
    private GameObject flechaInst;

    [Header("Dirección: Flujo (animación de textura)")]
    public bool animarTextura = false;
    public float velocidadFlujo = 0.5f;      // + hacia B, - invierte

    private LineRenderer line;
    private Material matInst; // instancia del material para animar sin afectar a otros

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.enabled = true;

        // Gradiente A→B
        if (usarGradiente)
        {
            var g = new Gradient();
            g.SetKeys(
                new[] {
                    new GradientColorKey(colorInicio, 0f),
                    new GradientColorKey(colorFin, 1f)
                },
                new[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f)
                }
            );
            line.colorGradient = g;
        }

        // Instanciar flecha si corresponde
        if (usarFlecha && flechaPrefab != null)
        {
            flechaInst = Instantiate(flechaPrefab);
            flechaInst.transform.localScale = Vector3.one * flechaEscala;
        }

        // Material instanciado para animar offset sin tocar el asset
        if (animarTextura && line.sharedMaterial != null)
        {
            matInst = new Material(line.sharedMaterial);
            line.material = matInst; // usa la instancia
            // Consejo: poné el material en Unlit con textura tileable (flechas/lineas)
            // y Texture Mode = "Tile" en el LineRenderer para ver el flujo.
            line.textureMode = LineTextureMode.Tile;
        }
    }

    void LateUpdate()
    {
        if (puntoA == null || puntoB == null)
        {
            line.positionCount = 0;
            if (flechaInst) flechaInst.SetActive(false);
            return;
        }

        if (!usarCurva)
        {
            if (line.positionCount != 2) line.positionCount = 2;
            line.SetPosition(0, puntoA.position);
            line.SetPosition(1, puntoB.position);
        }
        else
        {
            if (segmentos < 3) segmentos = 3;
            line.positionCount = segmentos;

            Vector3 a = puntoA.position;
            Vector3 b = puntoB.position;
            Vector3 medio = (a + b) * 0.5f;
            Vector3 control = medio + Vector3.down * sag;

            for (int i = 0; i < segmentos; i++)
            {
                float t = i / (segmentos - 1f);
                Vector3 p1 = Vector3.Lerp(a, control, t);
                Vector3 p2 = Vector3.Lerp(control, b, t);
                Vector3 p = Vector3.Lerp(p1, p2, t);
                line.SetPosition(i, p);
            }
        }

        // Flecha mirando de A → B (en el extremo B)
        if (usarFlecha && flechaInst != null)
        {
            flechaInst.SetActive(true);
            Vector3 end = line.GetPosition(line.positionCount - 1);
            Vector3 penultimate = line.GetPosition(Mathf.Max(0, line.positionCount - 2));
            Vector3 dir = (end - penultimate).normalized;

            flechaInst.transform.position = end + dir * flechaOffset;
            if (dir.sqrMagnitude > 0.0001f)
                flechaInst.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        // Animación de textura (flujo A→B)
        if (animarTextura && matInst != null)
        {
            Vector2 ofs = matInst.mainTextureOffset;
            ofs.x += Time.deltaTime * velocidadFlujo;
            matInst.mainTextureOffset = ofs;
        }
    }
}
