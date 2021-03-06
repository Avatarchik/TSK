﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct MeteorInfoStruct
{
    public float Velocity;
    public float Radius;
    public float Angle;
    public float Mass;
}

public class Meteor : MonoBehaviour
{
    public GameObject Explosion;

    public LineRenderer LineLeft;
    public Material IceMaterial;
    public Material IronMaterial;
    public Material CobaltMaterial;
    public Material DiopsideMaterial;
    public ParticleSystem IceParticles;

    public ParticleSystem IronParticles;
    public ParticleSystem CobaltParticles;
    public ParticleSystem DiopsideParticles;

    private ParticleSystem _particles;
    private float _radius;
    private float _initialVelocity;
    private float _angle;
    private float _cx = 0.25f;
    private MeteorMaterial _material;
    private float _lastTime = 0.0f;

    private List<Vector3> _points = new List<Vector3>();
    
    public float Radius
    {
        get { return _radius; }
        set
        {
            _radius = Mathf.Clamp(value, 20.0f, 300.0f);
        }
    }

    public float InitialVelocity
    {
        get { return _initialVelocity; }
        set
        {
            _initialVelocity = Mathf.Clamp(value, 12.0f, 72.0f);
        }
    }

    public float Angle
    {
        get { return _angle; }
        set
        {
            _angle = Mathf.Clamp(value, 30.0f, 90.0f);
        }
    }

    private Vector3 _velocity;

    void OnEnable()
    {
        _material = GetComponent<MeteorMaterial>();
        if(_material == null)
        {
            return;
        }
        Renderer myRenderer = GetComponent<Renderer>();

        CobaltParticles.gameObject.SetActive(false);
        DiopsideParticles.gameObject.SetActive(false);
        IceParticles.gameObject.SetActive(false);
        IronParticles.gameObject.SetActive(false);

        switch(_material.Type)
        {
            case MaterialType.Cobalt:
                myRenderer.material = CobaltMaterial;
                _particles = CobaltParticles;
                CobaltParticles.gameObject.SetActive(true);
                break;
            case MaterialType.Diopside:
                myRenderer.material = DiopsideMaterial;
                _particles = DiopsideParticles;
                DiopsideParticles.gameObject.SetActive(true);
                break;
            case MaterialType.Ice:
                myRenderer.material = IceMaterial;
                _particles = IceParticles;
                IceParticles.gameObject.SetActive(true);
                break;
            case MaterialType.Iron:
                myRenderer.material = IronMaterial;
                _particles = IronParticles;
                IronParticles.gameObject.SetActive(true);
                break;
        }
        Vector3 dir = Quaternion.Euler(0.0f, 0.0f, Angle) * Vector3.up;
        _velocity = dir * InitialVelocity;
        _particles.gameObject.transform.forward = -_velocity.normalized;
        _particles.emissionRate = (int)(3000.0f * transform.localScale.x);
        _particles.maxParticles = (int)(30000.0f * transform.localScale.x);
        _points.Add(transform.position);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Simulation.Instance.MeteorVanishCause = "Killed by user";
            Destroy(gameObject);
        }

        float prevMass = _material.Density * 4.0f / 3.0f * Mathf.PI * Radius * Radius * Radius;
        float currentTemp = Simulation.Instance.CurrentTemperature(transform.position);
        float airDensity = Simulation.Instance.AirPressure / (currentTemp * 287.05f);
        float velocityValue = _velocity.magnitude;
        float crossSectionalArea = Mathf.PI * Radius * Radius;
        float deltaMass = ((_material.HeatTransferCoefficient * airDensity * velocityValue * velocityValue * velocityValue * 1e9f * crossSectionalArea) / (2 * _material.EnthalpyOfVaporization)) * Time.deltaTime;
        float currentMass = prevMass - deltaMass;
        currentMass = Mathf.Clamp(currentMass, 0.0f, float.MaxValue);

        Vector3 prevVelo = _velocity;
        float gconst = Simulation.Instance.GConst;
        float earthMass = Simulation.Instance.Earth.Mass;
        float distance = Vector3.Distance(transform.position, Simulation.Instance.Earth.transform.position) * 1000.0f;
        Vector3 meteorToEarth = Simulation.Instance.Earth.transform.position - transform.position;
        meteorToEarth.Normalize();
        Vector3 deltaVelo = (gconst * earthMass / (distance * distance * Simulation.Instance.Scale) * meteorToEarth * 1e2f + 1/(2 * prevMass) * _cx * airDensity * velocityValue * velocityValue * 1e6f * crossSectionalArea * -_velocity.normalized) * Time.deltaTime;
        Vector3 currentVelo = prevVelo + deltaVelo * 1e-3f;

        float le = -0.5f * 0.004f * velocityValue * velocityValue * velocityValue * (currentMass - prevMass);
        le *= 1e9f;
        float digitCount = (float)Math.Ceiling(Mathf.Log10(le));
        float tmp = Mathf.Pow(10.0f, digitCount);
        le /= tmp;
        Color c = _particles.startColor;
        c.a = le;
        _particles.startColor = c;

        _radius = Mathf.Pow((3.0f * currentMass) / (4.0f * Mathf.PI * _material.Density), 1.0f / 3.0f);
        _velocity = currentVelo;
        _angle = Vector3.Angle(_velocity, Vector3.up);
        _particles.startSize = Mathf.Clamp(transform.localScale.x * 2.36f * 5.0f, 0.1f, float.MaxValue);

        if (float.IsNaN(_velocity.x) || float.IsNaN(_velocity.y) || float.IsNaN(_velocity.z))
        {
            _velocity = Vector3.zero;
        }
        else
        {
            _particles.gameObject.transform.forward = -_velocity.normalized;
        }
    }

    void FixedUpdate()
    {
        if(_radius < 0.01f)
        {
            Simulation.Instance.MeteorVanishCause = "Burned";
            Destroy(gameObject);
        }
        
        transform.localScale = 0.01f * _radius * Vector3.one;

        if (Time.time - _lastTime > 0.1f)
        {
            _points.Add(transform.position);
            _lastTime = Time.time;
        }

        if(!(float.IsNaN(_velocity.x) || float.IsNaN(_velocity.y) || float.IsNaN(_velocity.z)))
        {
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }

    void OnDestroy()
    {
        _points.Add(transform.position);
        LineLeft.SetVertexCount(_points.Count);
        for(int i = 0; i < _points.Count; ++i)
        {
            LineLeft.SetPosition(i, _points[i]);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Earth"))
        {
            GameObject explosionInstance = (GameObject)Instantiate(Explosion, col.contacts[0].point, Quaternion.identity);
            explosionInstance.transform.localScale = transform.localScale * 42.7f;
            Simulation.Instance.MeteorVanishCause = "Earth hit";
            Destroy(gameObject);
        }
    }

    public string GetMaterialName()
    {
        return _material.ToString();
    }

    public MeteorInfoStruct GetInfo()
    {
        MeteorInfoStruct mis = new MeteorInfoStruct();
        mis.Angle = _angle;
        mis.Radius = _radius;
        mis.Velocity = _velocity.magnitude;
        mis.Mass = _material.Density * 4.0f / 3.0f * Mathf.PI * Radius * Radius * Radius;

        return mis;
    }
}
