using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Bridge genérico para UI en World Space que usa tus clases existentes.
public class GateUIBridge_Existing : MonoBehaviour
{
    [Header("Entradas (tus ComportamientoEntradas)")]
    public ComportamientoEntradas entradaA;
    public ComportamientoEntradas entradaB; // puede quedar null en NOT

    [Header("Lógica de compuerta (componente que implementa InterfazComp)")]
    // Unity no serializa interfaces; arrastrá acá el componente (p.ej. AND_Behaviour)
    public MonoBehaviour gateLogicBehaviour;
    private InterfazComp gate; // cacheada en runtime

    [Header("UI")]
    public Button btnToggleA;
    public Button btnToggleB; // opcional: desactívalo en NOT
    public TextMeshProUGUI outText;

    [Header("Opcional")]
    [Tooltip("Si está activo, el botón fuerza el valor con tomarValor() aunque isConnected sea true.")]
    public bool overrideConnectedWithTomarValor = false;

    void Awake()
    {
        // Cachear la interfaz desde el componente arrastrado
        gate = gateLogicBehaviour as InterfazComp;
        if (gate == null && gateLogicBehaviour != null)
            Debug.LogError($"El componente {gateLogicBehaviour.GetType().Name} no implementa InterfazComp.");
    }

    void OnEnable()
    {
        // Enlazar botones si no lo hiciste desde el Inspector
        if (btnToggleA) btnToggleA.onClick.AddListener(ToggleA);
        if (btnToggleB) btnToggleB.onClick.AddListener(ToggleB);
        RefreshUI();
    }

    void OnDisable()
    {
        if (btnToggleA) btnToggleA.onClick.RemoveListener(ToggleA);
        if (btnToggleB) btnToggleB.onClick.RemoveListener(ToggleB);
    }

    void Update()
    {
        RefreshUI();
    }

    public void ToggleA()
    {
        if (!entradaA) return;

        if (overrideConnectedWithTomarValor)
            entradaA.tomarValor(!entradaA.value);  // fuerza aunque esté conectada
        else
            entradaA.onTouch(); // respeta tu regla: no toggle si isConnected == true
    }

    public void ToggleB()
    {
        if (!entradaB) return;

        if (overrideConnectedWithTomarValor)
            entradaB.tomarValor(!entradaB.value);
        else
            entradaB.onTouch();
    }

    private void RefreshUI()
    {
        // Texto OUT
        if (outText && gate != null)
            outText.text = $"OUT: {(gate.GetSalida() ? 1 : 0)}";

        // Interactuable según isConnected
        if (btnToggleA && entradaA)
            btnToggleA.interactable = overrideConnectedWithTomarValor || !entradaA.isConnected;

        if (btnToggleB && entradaB)
            btnToggleB.interactable = overrideConnectedWithTomarValor || !entradaB.isConnected;
    }
}
