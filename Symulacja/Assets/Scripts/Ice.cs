using UnityEngine;
using System.Collections;

public class Ice : MeteorMaterial
{
    public Ice()
    {
        Density = 0.9167f;
        SpecificHeat = 2100.0f;
        HeatTransferCoefficient = 2.18f;
        EnthalpyOfVaporization = 2500712.59f;
        Type = MaterialType.Ice;
    }
}
