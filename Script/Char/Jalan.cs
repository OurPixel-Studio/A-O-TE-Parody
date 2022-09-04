using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalan : MonoBehaviour
{
    CharacterController Kontrol;

    public float kecepatan = 5f;
    public float loncat = 10f;
    public float gravitasi = -20f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool diTanah;

    public GameObject lukaJatuh;
    GameObject Titan;

    bool TerAmbil;
    public bool habisJatuh;

    public float timer = 0.5f ;

    GameObject manuverGear;

    public bool sedangManuverGear;

    public bool SedangManuberGear;

    // Start is called before the first frame update
    void Start()
    {
        Kontrol = GetComponent<CharacterController>();
        
        manuverGear = GameObject.FindGameObjectWithTag("3dm");
    }

    // Update is called once per frame
    void Update()
    {
        Titan = GameObject.FindGameObjectWithTag("Titan");
        
        kecepatan = 5;
        sedangManuverGear = manuverGear.GetComponent<ManuverGear>().SedangManuverGear;
        TerAmbil = Titan.GetComponent<TitanAI>().ambilKarakter;

        Gerak();

        diTanah = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(diTanah&&velocity.y < 0)
        {
            velocity.y = -5;
        }

        if (timer <= 0)
        {
            lukaJatuh.SetActive(false);
        }

        if(TerAmbil == true)
        {
            kecepatan = 0.2f;
        }
    }

    void Gerak()
    {
        kecepatan = 5;

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        Vector3 bergerak = transform.right * z + transform.forward * x;

        Kontrol.Move( bergerak * kecepatan * Time.deltaTime);

        velocity.y += gravitasi * Time.deltaTime;

        Kontrol.Move(velocity * Time.deltaTime);

        // ini bagian buat loncat
        if (sedangManuverGear == false)
        {
            if (Kontrol.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    velocity.y = loncat;
                }
                betulinBadan();
            }
            else
            {
                //ni bagian nambah kecepatan 

                if (Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.D))
                {
                    if (habisJatuh == false)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            kecepatan = 10;
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            kecepatan = 2;
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * 15 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * 15 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * 15 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * 15 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = loncat - 8;
            }

        }
    }

    void betulinBadan()
    {
        if(transform.eulerAngles.z > 0.3)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            lukaJatuh.SetActive(true);
            habisJatuh = true;
        }

        if(habisJatuh == true)
        {
            timer -= 1 * Time.deltaTime;
            kecepatan = kecepatan / 5;
            
        }

        if(timer <= 0.0)
        {
            lukaJatuh.SetActive(false);
            timer = 0.5f;
            habisJatuh = false;
            kecepatan = kecepatan * 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Kontrol.isGrounded)
        {
            if (other.gameObject.tag == "Benda Lain")
            {
                SedangManuberGear = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SedangManuberGear = false;
    }
}
