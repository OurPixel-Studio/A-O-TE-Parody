using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuverGear : MonoBehaviour
{
    private LineRenderer lr;
    public Vector3 grapplePoint; //tujuan 3dm
    public LayerMask whatIsGrappleable;
    public Transform gunTip, Kamera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    public bool SedangManuverGear; //ketika manuverGear menempel bendaLain
    public bool SedangMelayang; //ketika sedang berManuverGear

    public int mulaiManuver2; //tombol untuk mulai manuverGear pada kondisi manuver 2
    public int kondisiManuver; //kondisiManuver, apakah 1 tombol atau 2 tombol

    public TimeManager timeManager; //untuk slow mo

    public GameObject badan; //untuk mengambil script Jalan

    public GameObject bendaCheck; //cek manuver gear sedang menempel pada bendalain atau tidak

    bool sedangManuverGear; //untuk melepas manuverGear ketika menempel bendaLain

    private float kec3dm = 75; // kecepatan dari 3dm

    public CharacterController kontrol; //badan karakter untuk mengecek sedang menempel tanah atau tidak

    public float maxGas3dm = 100;
    public float gas3dm; //kapasitas gas untuk 3dm

    public bool SedangIsiGas; 

    float waktuIsiGas = 1.3f;

    public UI3dm ui3dm;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        gas3dm = maxGas3dm;
        ui3dm.Gas3dmMax(gas3dm);
    }

    void Update()
    {
        if (SedangMelayang == true)
        {
            sedang3dm();
            gas3dm -= 15 * Time.deltaTime;
        }

        kec3dm = 75;

        if (gas3dm <= 0.5f)
        {
            SedangManuverGear = false;
            SedangMelayang = false;
            StopGrapple();
            kec3dm = 0;
        }

        if (kontrol.isGrounded)
        {
            if (gas3dm < 100)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    SedangIsiGas = true;
                }
            }
        }

        if (SedangIsiGas == true)
        {
            waktuIsiGas -= 1 * Time.deltaTime;
        }
        if (waktuIsiGas <=0 )
        {
            SedangIsiGas = false;
            gas3dm = 100;
        }
        if (SedangIsiGas == false)
        {
            waktuIsiGas = 1.3f;
        }

        sedangManuverGear = badan.GetComponent<Jalan>().SedangManuberGear;

        kondisi();

        if (sedangManuverGear == true)
        {
            StopGrapple();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            timeManager.DoSlowMotion();
        }

        if (kondisiManuver == 1)
        {

            if (Input.GetMouseButtonUp(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                StopGrapple();
            }
        }
        else
        {

            if (Input.GetMouseButtonUp(mulaiManuver2))
            {
                StartGrapple();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StopGrapple();
        }

        if (SedangManuverGear == true)
        {
            if (!kontrol.isGrounded)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    SedangMelayang = false;
                    player.Translate(Vector3.back * 20 * Time.deltaTime);
                    player.Translate(Vector3.up * 20 * Time.deltaTime);
                    gas3dm -= 5 * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    SedangMelayang = false;
                    player.Translate(Vector3.left * 20 * Time.deltaTime);
                    player.Translate(Vector3.up * 20 * Time.deltaTime);
                    gas3dm -= 5 * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    SedangMelayang = false;
                    player.Translate(Vector3.right * 20 * Time.deltaTime);
                    player.Translate(Vector3.up * 20 * Time.deltaTime);
                    gas3dm -= 5 * Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.D)))
                {
                    gas3dm -= 1 * Time.deltaTime;
                }

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SedangMelayang = true;
            }
        }
        else
        {
            SedangMelayang = false;
        }

        ui3dm.Gas3dm(gas3dm);
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    //kondisi saat manuver gear
    void kondisi()  
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            kondisiManuver = 1;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            kondisiManuver = 2;
        }
        
        
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamera.position, Kamera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            SedangManuverGear = true;

            bendaCheck.transform.position = new Vector3(grapplePoint.x, grapplePoint.y, grapplePoint.z);
        }
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        SedangManuverGear = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 10f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    void sedang3dm()
    {
        player.position = Vector3.MoveTowards(player.position, grapplePoint, kec3dm * Time.deltaTime);
        Debug.Log(kec3dm);
    }
}
