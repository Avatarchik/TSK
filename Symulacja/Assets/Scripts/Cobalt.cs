using UnityEngine;
using System.Collections;

public class Cobalt : MeteorMaterial
{
    public Cobalt()
    {
        Density = 8.9f * 1000.0f;
        SpecificHeat = 4200.0f;
        HeatTransferCoefficient = 69.0f;
        EnthalpyOfVaporization = 6317066.19f;
        Type = MaterialType.Cobalt;
    }
}
