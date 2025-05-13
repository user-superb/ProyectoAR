using UnityEngine;
using UnityEngine.Windows;

public class CompuertaAND : CompuertaLogica
{
  public override bool CalcularSalida()
    {
        return inputA && inputB;
    }
}