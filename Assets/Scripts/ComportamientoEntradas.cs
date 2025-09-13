using UnityEngine;

public class ComportamientoEntradas : MonoBehaviour
{
    public bool isConnected = false;
    public bool value = false; //verdadero valor booleano
    public Color modifiedColor = Color.green;
    private Color originalColor = Color.red;
    private MeshRenderer propiedadesFisicas;
    void Awake()
    {
        propiedadesFisicas = GetComponent<MeshRenderer>();
        if (propiedadesFisicas != null)
            originalColor = Color.red;
        else
            Debug.LogWarning("Error. Mesh no encontrado en " + gameObject.name + ". No se cargaran las propiedades del objeto.");
        this.checkOnProperties();
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
    public void EsConnected(){ //Agregué este método para que pueda ser modificado por otro objeto.
        isConnected = !isConnected;
    }
    public void tomarValor(bool val){ //recibe un valor booleano y lo actualiza en función del parámetro recibido
        value = val;
            if (propiedadesFisicas != null)
            {
                if (value)
                    propiedadesFisicas.material.color = modifiedColor;
                else
                    propiedadesFisicas.material.color = originalColor;
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

   
}
