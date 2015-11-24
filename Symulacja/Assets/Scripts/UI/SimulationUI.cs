using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimulationUI : MonoBehaviour
{
    public Text MaterialInfo;
    public Text RadiusInfo;
    public Text VelocityInfo;
    public Text AngleInfo;
    public Text MassInfo;
    public Text[] Labels;
    
    void OnEnable()
    {
        MaterialInfo.text = Simulation.Instance.Meteor.GetMaterialName();
        MeteorInfoStruct mis = Simulation.Instance.Meteor.GetInfo();
        RadiusInfo.text = mis.Radius.ToString("0.000 m");
        VelocityInfo.text = mis.Velocity.ToString("0.000 km/s");
        AngleInfo.text = mis.Angle.ToString("0.000 degrees");
        MassInfo.text = mis.Mass.ToString("0 kg");
    }

    void LateUpdate()
    {
        if (Simulation.Instance.Meteor != null)
        {
            MeteorInfoStruct mis = Simulation.Instance.Meteor.GetInfo();
            RadiusInfo.text = mis.Radius.ToString("0.000 m");
            VelocityInfo.text = mis.Velocity.ToString("0.000 km/s");
            AngleInfo.text = mis.Angle.ToString("0.000 degrees");
            MassInfo.text = mis.Mass.ToString("0 kg");
        }
        else
        {
            RadiusInfo.text = "";
            VelocityInfo.text = "";
            AngleInfo.text = "";
            MassInfo.text = "";
            MaterialInfo.text = Simulation.Instance.MeteorVanishCause;
            MaterialInfo.fontSize = 40;
            MaterialInfo.alignment = TextAnchor.MiddleCenter;
            foreach(Text t in Labels)
            {
                t.text = "";
            }
        }
    }
}
