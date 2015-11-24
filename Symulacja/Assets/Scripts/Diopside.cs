using UnityEngine;
using System.Collections;

public class Diopside : MeteorMaterial
{
    public Diopside()
    {
        Density = 3.4f * 1000.0f;
        SpecificHeat = 916.49f;
        HeatTransferCoefficient = 2.4f;
        EnthalpyOfVaporization = 4874246.36f;
        Type = MaterialType.Diopside;
    }
}
