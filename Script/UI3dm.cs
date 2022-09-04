using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3dm : MonoBehaviour
{
    public Text kondisi3dm;
    public Slider gasTotal;
    GameObject ManuverGear;
    int kondisiManuver;
    bool isiGas;
    
    // Start is called before the first frame update
    void Start()
    {
        ManuverGear = GameObject.FindGameObjectWithTag("3dm");
    }

    // Update is called once per frame
    void Update()
    {
        kondisiManuver = ManuverGear.GetComponent<ManuverGear>().kondisiManuver;
        isiGas = ManuverGear.GetComponent<ManuverGear>().SedangIsiGas;

        kondisi3dm.text = "Kondisi Manuver Gear = " + kondisiManuver;

        if (isiGas == true)
        {
            kondisi3dm.text = "Sedang Isi Gas Bray";
        }
    }

    public void Gas3dmMax(float jumlahGas)
    {
        gasTotal.maxValue = jumlahGas;
        gasTotal.value = jumlahGas;
    }

    public void Gas3dm(float jumlahGas)
    {
        gasTotal.value = jumlahGas;
    }
}
