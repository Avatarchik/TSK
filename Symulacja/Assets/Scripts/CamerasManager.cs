using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamerasManager : MonoBehaviour
{
    public List<CameraScript> Cameras;

    private int _currentCamera = 0;

	// Use this for initialization
	public void Init ()
    {
        if(Cameras == null || Cameras.Count == 0)
        {
            Cameras = new List<CameraScript>(FindObjectsOfType<CameraScript>());
        }
        Cameras.Sort((CameraScript c1, CameraScript c2) => {
            int toRet = 0;
            if(c1.CodeToEnable > c2.CodeToEnable)
            {
                toRet = 1;
            }
            else if(c1.CodeToEnable < c2.CodeToEnable)
            {
                toRet = -1;
            }
            return toRet;
        });
        Cameras[_currentCamera].gameObject.SetActive(true);
        for(int i = 1; i < Cameras.Count; ++i)
        {
            Cameras[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.anyKeyDown)
        {
            try
            {
                string tmp = Input.inputString;
                if(Input.inputString.Length == 1 && Input.inputString[0] >= '0' && Input.inputString[0] <= '9')
                {
                    tmp = "Alpha" + Input.inputString;
                }
                KeyCode keyPressed = (KeyCode)System.Enum.Parse(typeof(KeyCode), tmp);
                foreach (CameraScript cs in Cameras)
                {
                    if (cs.CheckIfEnable(keyPressed))
                    {
                        if (cs != Cameras[_currentCamera])
                        {
                            Cameras[_currentCamera].gameObject.SetActive(false);
                            cs.gameObject.SetActive(true);
                        }
                        _currentCamera = Cameras.IndexOf(cs);
                        break;
                    }
                }
            }
            catch
            {

            }
        }
	}
}
