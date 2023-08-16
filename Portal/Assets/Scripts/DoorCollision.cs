using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorCollision : MonoBehaviour
{
    public Material[] mat;
    private bool checkIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Camera"))
        {
            if (checkIn)
            {
                foreach (var m in mat)
                    m.SetInt("stest", (int)CompareFunction.Equal);

                checkIn = false;
            }
            else
            {
                foreach (var m in mat)
                    m.SetInt("stest", (int)CompareFunction.NotEqual);

                checkIn = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var m in mat)
            m.SetInt("stest", (int)CompareFunction.Equal);
    }
}
