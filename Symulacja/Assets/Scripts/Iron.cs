using UnityEngine;
using System.Collections;

public class Iron : MeteorMaterial
{    
    public Iron()
    {
        Density = 7.874f * 1000.0f;
        SpecificHeat = 4500.0f;
        HeatTransferCoefficient = 55.0f;
        EnthalpyOfVaporization = 6215668.28f;
        Type = MaterialType.Iron;
    }
}
