using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasi : MonoBehaviour
{
    public CharacterController myController;
    Animator myAnimator;
    bool melayang, Q, E, Jalan;
    public bool sedangSerang = false;
    GameObject ManuverGear;
    public bool sedang3dm;

    public float timeMelayang = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        ManuverGear = GameObject.FindGameObjectWithTag("3dm");

    }

    // Update is called once per frame
    void Update()
    {
        melayang = false;
        Q = false;
        E = false;
        Jalan = false;
        sedangSerang = false;

        sedang3dm = ManuverGear.GetComponent<ManuverGear>().SedangMelayang;

        if (sedang3dm == true)
        {
            timeMelayang -= 1 * Time.deltaTime;
        }
        else
        {
            timeMelayang = 0.1f;
            if (myController.isGrounded)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Jalan = true;
                }
            }
        }

        if (timeMelayang <= 0)
        {
            melayang = true;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Q = true;
            sedangSerang = true;
        }
        if (Input.GetKey(KeyCode.E))
        {
            E = true;
            sedangSerang = true;
        }
        myAnimator.SetBool("melayang", melayang);
        myAnimator.SetBool("Q", Q);
        myAnimator.SetBool("E", E);
        myAnimator.SetBool("Jalan", Jalan);
    }
}
