using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerControler : MonoBehaviour
{
    private new Rigidbody rb;//Fisicas de nuestro personaje
    private float speed = 10f;
    private Animator ani;

    //[SerializeField] private Transform CameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //float MouseX = Input.GetAxis("Mouse X");
        //float MouseY = Input.GetAxis("Mouse Y");

        if (horizontal != 0f || vertical != 0f)
        { 
            

            Vector3 direccion = transform.forward*vertical + transform.right*horizontal;

            //transform.rotation = CameraRotation.rotation;
            rb.MovePosition(transform.position + direccion * speed * Time.deltaTime);


            
            ani.SetBool("isMove", true);
        } else
        {
            ani.SetBool("isMove", false);
        }
            
        
        
    }
}
