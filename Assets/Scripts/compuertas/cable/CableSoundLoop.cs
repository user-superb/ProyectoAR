using Unity.Mathematics;
using UnityEngine;

public class CableSoundLoop : MonoBehaviour
{
    public AudioSource sonido;
    private LineRenderer cable;

    private bool reproducido = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Logger.Sonido(AudioSettings.GetSpatializerPluginName());
        cable = GetComponent<LineRenderer>();
        if (sonido == null)
        {
            Logger.Warning($"[CableSoundLoop] yo {gameObject.name} veo que no pusiste un audioSource en la configuracion de el sonido, ponele algo carajo ");
        }
        //sonido.loop = true;
        sonido.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cable.enabled && !sonido.isPlaying && !reproducido)
        {
            Logger.Sonido($"[CableSoundLoop] yo {gameObject.name} activo el sonido de electricidad por conectar la compuerta");
            sonido.Play();
            reproducido = true;
        }
        else if (!cable.enabled)
        {
            reproducido = false;
            if (sonido.isPlaying)
            {
                Logger.Sonido($"[CableSoundLoop] yo {gameObject.name} desactivo el sonido de electricidad por conectar la compuerta");
                sonido.Stop();
            }

            
        }

    }
}
