using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
using System;
using System.Collections.Generic;

public class ParametersUI : MonoBehaviour
{
    public Meteor Meteor;

    public Dropdown MaterialDropdown;
    public Slider RadiusSlider;
    public InputField RadiusInputField;
    public Slider InitialVelocitySlider;
    public InputField InitialVelocityInputField;
    public Slider AngleSlider;
    public InputField AngleInputField;

    void OnEnable()
    {
        MaterialDropdown.options = new List<Dropdown.OptionData>();
        MaterialDropdown.options.Add(new Dropdown.OptionData("Ice"));
        MaterialDropdown.options.Add(new Dropdown.OptionData("Iron"));
        MaterialDropdown.options.Add(new Dropdown.OptionData("Rocks"));
        MaterialDropdown.value = 0;
        MaterialDropdown.GetComponentInChildren<Text>().text = MaterialDropdown.options[0].text;

        RadiusSlider.value = Meteor.Radius;
        RadiusInputField.text = Meteor.Radius.ToString();
        InitialVelocitySlider.value = Meteor.InitialVelocity;
        InitialVelocityInputField.text = Meteor.InitialVelocity.ToString();
        AngleSlider.value = Meteor.Angle;
        AngleInputField.text = Meteor.Angle.ToString();
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
                Meteor.gameObject.AddComponent<Rocks>();
                break;
            default:
                Debug.Log("Nothing selected");
                break;
        }

        //Camera.main.transform.parent = Simulation.Instance.Meteor.transform;
        Simulation.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RadiusSliderChange(float value)
    {
        Meteor.Radius = RadiusSlider.value;
        RadiusSlider.value = Meteor.Radius;
        RadiusInputField.text = Meteor.Radius.ToString();
    }

    public void RadiusInputFieldValueChange(string value)
    {
        float val = 0.0f;
        if(float.TryParse(RadiusInputField.text, out val))
        {
            Meteor.Radius = val;
            RadiusSlider.value = Meteor.Radius;
            RadiusInputField.text = Meteor.Radius.ToString();
        }
    }

    public void InitialVelocitySliderChange(float value)
    {
        Meteor.InitialVelocity = InitialVelocitySlider.value;
        InitialVelocitySlider.value = Meteor.InitialVelocity;
        InitialVelocityInputField.text = Meteor.InitialVelocity.ToString();
    }

    public void InitialVelocityInputFieldValueChange(string value)
    {
        float val = 0.0f;
        if (float.TryParse(InitialVelocityInputField.text, out val))
        {
            Meteor.InitialVelocity = val;
            InitialVelocitySlider.value = Meteor.InitialVelocity;
            InitialVelocityInputField.text = Meteor.InitialVelocity.ToString();
        }
    }

    public void AngleSliderChange(float value)
    {
        Meteor.Angle = AngleSlider.value;
        AngleSlider.value = Meteor.Angle;
        AngleInputField.text = Meteor.Angle.ToString();
    }

    public void AngleInputFieldValueChange(string value)
    {
        float val = 0.0f;
        if (float.TryParse(AngleInputField.text, out val))
        {
            Meteor.Angle = val;
            AngleSlider.value = Meteor.Angle;
            AngleInputField.text = Meteor.Angle.ToString();
        }
    }
}
