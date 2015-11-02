using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public KeyCode CodeToEnable;

    public bool CheckIfEnable(KeyCode kc)
    {
        return kc == CodeToEnable;
    }
}
