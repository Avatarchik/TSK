using UnityEngine;
using System.Collections;

public abstract class MeteorMaterial : MonoBehaviour
{
    public float Density;   //[g/cm3]
    public float SpecificHeat;  //[J/kgK]
    public float HeatTransferCoefficient;   //[]
    public float EnthalpyOfVaporization;    //[J/kg]
}
