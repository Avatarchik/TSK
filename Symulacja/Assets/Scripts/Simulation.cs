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
    public float Scale = 3185.5f;
    public float AirPressure = 1013.25f;
    public float GConst = 6.6740831e-11f;
    
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
        Scale = 6371 / Earth.transform.localScale.x;
        //float meteorRadius = Meteor.Radius;
        //meteorRadius -= 20.0f;
        //meteorRadius /= 280.0f;
        //Meteor.transform.localScale = Vector3.one * Mathf.Lerp(0.1f, 3.0f, meteorRadius);
        Meteor.transform.localScale = 0.01f * Meteor.Radius * Vector3.one;
        CamerasManager.gameObject.SetActive(true);
        CamerasManager.Init();
        Meteor.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    public float CurrentTemperature(Vector3 meteorPosition)
    {
        float temp = 0.0f;

        float meteorDistanceFromEarth = Vector3.Distance(Earth.transform.position, meteorPosition);
        float relativeDistance = meteorDistanceFromEarth - Earth.transform.localScale.x;
        float atmRelativeScale = Atmosphere.transform.localScale.x - Earth.transform.localScale.x;

        temp = Mathf.Lerp(25.0f, -150.0f, relativeDistance / atmRelativeScale);

        temp += 274.15f;

        return temp;
    }

    private void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
