using UnityEngine;
using System.Collections.Generic;

public class Earth : MonoBehaviour {

    public float Mass = 5.97219e24f;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 0.5f);
    }
}
