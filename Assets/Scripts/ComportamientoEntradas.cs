using UnityEngine;

public class ComportamientoEntradas : MonoBehaviour
{
    public bool isConnected = false;
    public Color modifiedColor = Color.yellow;
    private Color originalColor;
    private MeshRenderer propiedadesFisicas;
    void Awake()
    {
        propiedadesFisicas = GetComponent<MeshRenderer>();
        if (propiedadesFisicas != null)
            originalColor = propiedadesFisicas.material.color;
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

    public void onTouch()
    {
        isConnected = !isConnected;

        if (propiedadesFisicas != null)
        {
            if (isConnected)
                propiedadesFisicas.material.color = modifiedColor;
            else
                propiedadesFisicas.material.color = originalColor;
        }
         
    }

   
}
