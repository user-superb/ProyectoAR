using UnityEngine;

public static class Logger
{
    // 🔹 Configuraciones globales
    public static bool mostrarConectando = false;
    public static bool mostrarResumenConectando = true;
    public static bool mostrarExtendidoDesconectando = false;
    public static bool mostrarDesconectando = false;  
    public static bool mostrarResumenDesconectando = true;
    public static bool mostrarSonido = true;
    public static bool mostrarSonidoextendido = true;
    


    public static bool mostrarSonidoActivado = true;
    public static bool mostrarSonidoDesactivado = true;
    // 🔹 Métodos por categoría
    public static void Conectando(string mensaje)
    {
        if (mostrarConectando)
            Debug.Log("<color=#00FFFF>[Conectando]</color> " + mensaje);
    }


    public static void ResumenConectando(string mensaje)
    {
        if (mostrarResumenConectando)
            Debug.Log("<color=#00FF00>[ResumenConectando]</color> " + mensaje);
    }

    public static void ExtendidoDesconectando(string mensaje)
    {
        if (mostrarExtendidoDesconectando)
            Debug.Log("<color=#FFA500>[ExtendidoDesconectando]</color> " + mensaje);
    }
    public static void Desconectando(string mensaje)
    {
        if (mostrarDesconectando)
            Debug.Log("<color=#FFA500>[Desconectando]</color> " + mensaje);
    }

    public static void ResumenDesconectando(string mensaje)
    {
        if (mostrarResumenDesconectando)
            Debug.Log("<color=#FFA500>[ResumenDesconectando]</color> " + mensaje);
    }


    public static void Sonido(string mensaje)
    {
        if (mostrarSonido)
            Debug.Log("<color=#00FF7F>[Sonido]</color> " + mensaje);
    }

    public static void SonidoExtendido(string mensaje)
    {
        if (mostrarSonidoextendido)
            Debug.Log("<color=#1E90FF>[Sonidoextendido]</color> " + mensaje);
    }

    // 🔹 Errores o advertencias
    public static void Error(string mensaje)
    {
        Debug.LogError("[ERROR] " + mensaje);
    }

    public static void Warning(string mensaje)
    {
        Debug.LogWarning("[ADVERTENCIA] " + mensaje);
    }
}
