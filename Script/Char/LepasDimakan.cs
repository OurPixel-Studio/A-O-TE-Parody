using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LepasDimakan : MonoBehaviour
{
    public GameObject PanelDimakan;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TanganTitan")
        {
            if (GameObject.FindGameObjectWithTag("Titan").GetComponent<TitanAmbilCharacter>().ambil == true)
            {
                PanelDimakan.SetActive(true);
            }
        }
    }

}
