using UnityEngine;
using System.Collections;

public class Simulation : MonoBehaviour
{
    private static Simulation _instance;

    public static Simulation Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Simulation>();
                if(_instance == null)
                {
                    Debug.LogError("Oops, there is no simulation object on scene");
                }
            }

            return _instance;
        }
    }

    public Meteor Meteor;
    public Atmosphere Atmosphere;
    public Earth Earth;
    public CamerasManager CamerasManager;
    public float EarthRadius = 3185.5f; //3185.5

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        EarthRadius = 6371 / Earth.transform.localScale.x;
        float atmosphereScale = 500.0f / EarthRadius;
        Atmosphere.transform.localScale = Earth.transform.localScale + Vector3.one * atmosphereScale;
        Meteor.transform.localScale *= Meteor.Radius / EarthRadius;
        Vector3 direction = Quaternion.Euler(Random.Range(0.01f, 90.0f), Random.Range(0.01f, 90.0f), Random.Range(0.01f, 90.0f)) * Vector3.right;
        Meteor.transform.position = Atmosphere.transform.localScale.x * direction * 0.5f;
        CamerasManager.gameObject.SetActive(true);
        CamerasManager.Init();
        Meteor.gameObject.SetActive(true);
    }
}
