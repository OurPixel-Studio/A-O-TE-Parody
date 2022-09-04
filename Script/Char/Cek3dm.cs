using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cek3dm : MonoBehaviour
{
    public GameObject ManuverGear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Benda Lain")
        {
            Debug.Log("nempel");
        }
    }
    private void OnTriggerExit(Collider other)
    {
            ManuverGear.GetComponent<ManuverGear>().StopGrapple();
            Debug.Log("LEPAS");
    }
}
