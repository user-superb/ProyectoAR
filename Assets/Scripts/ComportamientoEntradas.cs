using UnityEngine;
using Oculus.Interaction;

public class ComportamientoEntradas : MonoBehaviour
{
    public bool isConnected = false;
    public bool value = false; //verdadero valor booleano
    public Color modifiedColor = Color.green;
    private Color originalColor = Color.red;
    private MeshRenderer propiedadesFisicas;
    private RayInteractable _ray;
    void Awake()
    {
        _ray = GetComponent<RayInteractable>();
        if (_ray != null)
        {
            // ¿Alguien me "seleccionó" con el rayo?
            _ray.WhenPointerEventRaised += onTouch;
            //_ray.WhenUnselect += OnUnselect;
        }

        propiedadesFisicas = GetComponent<MeshRenderer>();
        if (propiedadesFisicas != null)
        {
            originalColor = Color.red;
            modifiedColor = Color.green;
        }

        else
            Debug.LogWarning("Error. Mesh no encontrado en " + gameObject.name + ". No se cargaran las propiedades del objeto.");
        this.checkOnProperties();
    }
    void Update()
    {

    }
    void checkOnProperties()
    {
        //metodo algo innecesario, chequea si las cosas que active manualmente en Unity siguen configuradas correctamente
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            capsuleCollider.isTrigger = true;
        }

        Rigidbody cuerpo = GetComponent<Rigidbody>();
        if (cuerpo == null)
        {
            cuerpo = gameObject.AddComponent<Rigidbody>();
        }
        cuerpo.isKinematic = true;
        cuerpo.useGravity = false;
    }
    public void EsConnected(bool val)
    { //Agregué este método para que pueda ser modificado por otro objeto.
        isConnected = val;
    }
    public void tomarValor(bool val)
    { //recibe un valor booleano y lo actualiza en función del parámetro recibido
        value = val;
        if (propiedadesFisicas != null)
        {
            if (value)
                propiedadesFisicas.material.color = modifiedColor;
            else
                propiedadesFisicas.material.color = originalColor;
        }
    }
    public void onTouch(PointerEvent evt)
    {
        if (!isConnected) //Si la compuerta está conectada no se puede modificar el valor de entrada tocándola.
        {
            value = !value;

            if ((propiedadesFisicas != null) & (evt.Type == PointerEventType.Select))
            {
                if (value)
                    propiedadesFisicas.material.color = modifiedColor;
                else
                    propiedadesFisicas.material.color = originalColor;
            }
        }
    }

    public void onTouch()
    {
        if (!isConnected) //Si la compuerta está conectada no se puede modificar el valor de entrada tocándola.
        {
            value = !value;

            if (propiedadesFisicas != null)
            {
                if (value)
                    propiedadesFisicas.material.color = modifiedColor;
                else
                    propiedadesFisicas.material.color = originalColor;
            }
        }
    }
    public void tocarEntrada()
    {
        if (!isConnected) //Si la compuerta está conectada no se puede modificar el valor de entrada tocándola.
        {
            value = !value;

            if (propiedadesFisicas != null)
            {
                if (value)
                    propiedadesFisicas.material.color = modifiedColor;
                else
                    propiedadesFisicas.material.color = originalColor;
            }
        }
    }
}
