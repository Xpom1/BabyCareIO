using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBMove : MonoBehaviour
{
    Rigidbody rigidbo;
    Animator anim;
    Transform trans;
    bool grounded;
    float largo;
    Vector3 forcedir;
    Vector3 forcedir2;
    Vector3 forcedir3;
    RaycastHit hit;
    RaycastHit hit2;
    int run;
    Collider coll;
    int timecounter;
    public int idletime;
    float Raycorrection;
    bool active;
    float waitime;
    

    void Start()
    {
        rigidbo = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        coll = GetComponent<Collider>();        
        anim.SetInteger("up", 4);
        run = 0;
        Raycorrection = 0.025f;
        active = true;
        waitime = 2f;
    }

    void Update()
    {
        //Debug.Log("STATE   " + anim.GetInteger("up"));
        Debug.Log("active   " + active);

        //WHEN SIT or LIE DOWN
        if (anim.GetInteger("up") == 2)
        {
            if (Input.GetKey("c"))
            {
                anim.SetInteger("up", 3);
                StartCoroutine("wait", waitime);
            }
            if (Input.anyKey)
            {
                anim.SetInteger("up", 4);
                StartCoroutine("wait", waitime);
            }
        }

        if (anim.GetInteger("up") == 1)
        {
            if (Input.anyKey) anim.SetInteger("up", 3);
            StartCoroutine("wait", waitime);
        }

        //UP or CRAWLING
        if (anim.GetInteger("up") > 2)
        {
            //CHANGE STATE
            if (Input.anyKey) timecounter = 0;
            timecounter++;
            if (timecounter > idletime && anim.GetInteger("up") == 3)
            {
                anim.SetInteger("up", Random.Range(3, 0));
                timecounter = 0;
                StartCoroutine("wait", waitime);
            }
            if (timecounter > idletime && anim.GetInteger("up") == 4)
            {
                anim.SetInteger("up", 2);
                timecounter = 0;
                StartCoroutine("wait", waitime);
            }
            //CRAWL
            if (Input.GetKey("c") && anim.GetInteger("up") == 4)
            {
                if (anim.GetFloat("walk") == 0f) StartCoroutine("wait", waitime);
                anim.SetInteger("up", 3);
                Raycorrection = 0.2f;
            }
            //ADAPT TO THE GROUND WHEN CROWLING
            if (anim.GetInteger("up") == 3)
                transform.rotation = Quaternion.LookRotation(-forcedir, hit.normal + hit2.normal);

            //RUN
            if (Input.GetKey(KeyCode.LeftShift) && run < 30) run++;
            if (!Input.GetKey(KeyCode.LeftShift) && run > 0) run--;
            anim.SetFloat("run", run);

            //MOVE     
            anim.SetFloat("walk", Input.GetAxis("Vertical"));
            
            //TURN           
            anim.SetFloat("turn", Input.GetAxis("Horizontal"));
            if (anim.GetFloat("walk") !=0f && grounded && active)trans.Rotate(0f, Input.GetAxis("Horizontal") * 2f, 0f);                
            trans.Rotate(0f, anim.GetFloat("angle"), 0f);

            //JUMP & STAND UP
            if (Input.GetButtonDown("Jump") && grounded && anim.GetInteger("up") == 4 && active)
            {
                coll.material.dynamicFriction = 0;
                coll.material.staticFriction = 0;
                if (anim.GetFloat("walk") <= 0f)
                {
                    anim.Play("jump");
                }
                if (anim.GetFloat("walk") > 0f)
                {
                    anim.Play("runjumpin");
                }
            }
            if (Input.GetButtonDown("Jump") && grounded && anim.GetInteger("up") == 3 && active)
            {
                if (anim.GetFloat("walk") == 0f) StartCoroutine("wait", waitime);
                Raycorrection = 0.025f;
                anim.SetInteger("up", 4);
                transform.rotation = Quaternion.LookRotation(-new Vector3(forcedir.x, 0f, forcedir.z), Vector3.up);
            }

        }
    }

          
    void FixedUpdate()
    {             
             
            //GROUNDED                       
            if    (Physics.Raycast(transform.position + Vector3.up * 0.04f + trans.right * 0.1f + trans.forward * 0.08f, Vector3.down, 0.07f)
                || Physics.Raycast(transform.position + Vector3.up * 0.04f + trans.right * 0.1f + trans.forward * -0.08f, Vector3.down, 0.07f)
                || Physics.Raycast(transform.position + Vector3.up * 0.04f + trans.right * -0.1f + trans.forward * 0.08f, Vector3.down, 0.07f)
                || Physics.Raycast(transform.position + Vector3.up * 0.04f + trans.right * -0.1f + trans.forward * -0.08f, Vector3.down, 0.07f))
            {
                grounded = true;
                coll.material.dynamicFriction = 1;
                coll.material.staticFriction = 1;                              
            }
            else
            {   grounded = false;
                coll.material.dynamicFriction = 0;
                coll.material.staticFriction = 0;
                Raycorrection = 0.025f;
                anim.SetInteger("up", 4);                
            }
            anim.SetBool("grounded", grounded);

        //UP or CRAWLING
        if (anim.GetInteger("up") > 2)
        {
            //AIR MOVE
            if (!grounded && Input.GetAxis("Vertical") >  0.01f) rigidbo.AddForce(trans.forward * 0.05f, ForceMode.Impulse);
            if (!grounded && Input.GetAxis("Vertical") < -0.01f) rigidbo.AddForce(trans.forward * -0.05f, ForceMode.Impulse);

            // SET THE translation DIRECTION
            forcedir3 = new Vector3(forcedir.x, 0f, forcedir.z);
            if (Physics.Raycast(trans.position + (trans.forward * Raycorrection) + new Vector3(0f, 0.1f, 0f), Vector3.down, out hit))
            {
                forcedir3 = -Vector3.Cross(Vector3.Cross(hit.normal, trans.forward), hit.normal);
                forcedir3.Normalize();
            }
            forcedir2 = new Vector3(forcedir.x, 0f, forcedir.z);
            if (Physics.Raycast(trans.position + (trans.forward * -0.05f) + new Vector3(0f, 0.1f, 0f), Vector3.down, out hit2))
            {
                forcedir2 = -Vector3.Cross(Vector3.Cross(hit.normal, trans.forward), hit2.normal);
                forcedir2.Normalize();
            }
            forcedir = (forcedir3 + forcedir2);            
            forcedir.Normalize();           
                        
            //APLY THE SPEED
            if (grounded) rigidbo.velocity = forcedir * anim.GetFloat("speed") * -1f;                
        }
    }


         

    public void Jump(float jumpforce)
        {
            rigidbo.AddForce(Vector3.up * jumpforce + (trans.forward * anim.GetFloat("run") * 0.125f), ForceMode.Impulse);
        }

    /*public void Activate()
        { 
            active= true;
        }
        */

    public void Straight()
    {
        transform.rotation = Quaternion.LookRotation(-new Vector3(forcedir.x, 0f, forcedir.z), Vector3.up);       
    }

    IEnumerator wait(float wait)
    {
        active = false;
        yield return new WaitForSeconds(wait);
        active = true;
    }

}