using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
using System;
using System.Collections.Generic;

public class ParametersUI : MonoBehaviour
{
    public Meteor Meteor;
    public GameObject SimulationUI;

    public Dropdown MaterialDropdown;
    public Slider RadiusSlider;
    public Text RadiusText;
    public Slider VelocitySlider;
    public Text VelocityText;
    public Slider AngleSlider;
    public Text AngleText;

    void OnEnable()
    {
        MaterialDropdown.options = new List<Dropdown.OptionData>();
        MaterialDropdown.options.Add(new Dropdown.OptionData("Ice"));
        MaterialDropdown.options.Add(new Dropdown.OptionData("Iron"));
        MaterialDropdown.options.Add(new Dropdown.OptionData("Cobalt"));
        MaterialDropdown.options.Add(new Dropdown.OptionData("Diopside"));
        MaterialDropdown.value = 0;
        MaterialDropdown.GetComponentInChildren<Text>().text = MaterialDropdown.options[0].text;

        Meteor.Radius = 0.1f;
        Meteor.InitialVelocity = 0.1f;
        Meteor.Angle = 0.1f;

        RadiusSlider.value = Meteor.Radius;
        RadiusText.text = RadiusSlider.value.ToString("0.000 m");
        VelocitySlider.value = Meteor.InitialVelocity;
        VelocityText.text = VelocitySlider.value.ToString("0.000 km/s");
        AngleSlider.value = Meteor.Angle;
        AngleText.text = AngleSlider.value.ToString("0.000 degrees");
    }

    public void StartSimulation()
    {
        int i = MaterialDropdown.value;
        switch(i)
        {
            case 0:
                Meteor.gameObject.AddComponent<Ice>();
                break;
            case 1:
                Meteor.gameObject.AddComponent<Iron>();
                break;
            case 2:
                Meteor.gameObject.AddComponent<Cobalt>();
                break;
            case 3:
                Meteor.gameObject.AddComponent<Diopside>();
                break;
            default:
                Debug.Log("Nothing selected");
                break;
        }

        Simulation.Instance.gameObject.SetActive(true);
        SimulationUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RadiusSliderChange(float value)
    {
        Meteor.Radius = RadiusSlider.value;
        RadiusSlider.value = Meteor.Radius;
        RadiusText.text = Meteor.Radius.ToString("0.000 m");
    }

    public void VelocitySliderChange(float value)
    {
        Meteor.InitialVelocity = VelocitySlider.value;
        VelocitySlider.value = Meteor.InitialVelocity;
        VelocityText.text = Meteor.InitialVelocity.ToString("0.000 km/s");
    }

    public void AngleSliderChange(float value)
    {
        Meteor.Angle = AngleSlider.value;
        AngleSlider.value = Meteor.Angle;
        AngleText.text = Meteor.Angle.ToString("0.000 degrees");
    }
}
