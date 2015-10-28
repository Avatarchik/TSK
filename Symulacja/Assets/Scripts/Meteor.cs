using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    private float _radius;
    private float _initialVelocity;
    private float _angle;
    private MeteorMaterial _material;
    
    public float Radius
    {
        get { return _radius; }
        set
        {
            _radius = Mathf.Clamp(value, 0.01f, 100.0f);
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
    }

    void OnDisable()
    {
        Destroy(GetComponent<MeteorMaterial>());
    }

    void Update()
    {
        Vector3 diff = transform.position - Simulation.Instance.Earth.transform.position;
        diff.x = 0.0f;
        diff.z = 0.0f;
        diff.Normalize();
        _velocity = Quaternion.Euler(0.0f, 0.0f, -_angle) * diff;
        _velocity *= InitialVelocity;

        transform.position -= _velocity * Time.deltaTime;
    }
}
