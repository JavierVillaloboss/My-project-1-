using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad;
    public float saltoF;
    public int saltoD;
    public LayerMask capaSuelo;
        
        
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool Derecha = true;
    private int saltosR;
    private Animator animator;
    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosR = saltoD;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Promov();
        salto();
        CheckForGround();
    }

    bool suelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f,Vector2.down,0.2f,capaSuelo);
        return raycastHit.collider != null;
    }
    
    
    void salto()
    {
        animator.SetFloat("Jump",rigidbody.velocity.y);
        if (suelo())
        {
            saltosR = saltoD;
        }
        if (Input.GetKeyDown(KeyCode.Space) && saltosR > 0)
        {
            saltosR--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up* saltoF,ForceMode2D.Impulse);
        }
    }
    
    void Promov(){
        //movimiento
        float inputMov = Input.GetAxis("Horizontal");

        if(inputMov != 0f)
        {
            animator.SetBool("Running",true);
        }
        else
        {
            animator.SetBool("Running",false);
        }
        
        rigidbody.velocity = new Vector2(inputMov * velocidad, rigidbody.velocity.y);
        
        GestionarOr(inputMov);
    }

    void GestionarOr(float inputMov)
    {
        if ( (Derecha == true && inputMov < 0) || (Derecha == false && inputMov  >0 ))
        {
            Derecha = !Derecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void CheckForGround()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Suelo")))
        {
            animator.SetBool("Grounder",true);
        }
        else
        {
            animator.SetBool("Grounder",false);
        }
    }
}
