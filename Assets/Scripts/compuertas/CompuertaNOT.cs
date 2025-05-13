public class CompuertaNOT : CompuertaLogica
{
    public override bool CalcularSalida()
    {
        return !inputA; // Solo se usa un input
    }

    private void OnValidate()
    {
        inputB = false; // Desactiva el segundo input visualmente
    }
}
