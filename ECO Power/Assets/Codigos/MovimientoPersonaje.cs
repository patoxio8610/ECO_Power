using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
 
    public float velocidad = 6;
    public float tiempoRotacion = 0.1f;
 
    public float fuerzaSalto = 1.0f;
    public float gravedad = 9.81f;

    private float velocidadGiro;
    private float fuerzaGravedad = 0;

    private CharacterController controladorCuerpo;
    void Start()
    {
        controladorCuerpo = GetComponent<CharacterController>();
    }
   
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;
        if (direccion.magnitude >= 0.1f)
        {
            float anguloDeseado = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + Camera.main.gameObject.transform.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloDeseado, ref velocidadGiro, tiempoRotacion);
            transform.rotation = Quaternion.Euler(0, angulo, 0);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloDeseado, 0f) * Vector3.forward;
            controladorCuerpo.Move(direccionMovimiento.normalized * velocidad * Time.deltaTime);
        }

        if (controladorCuerpo.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                fuerzaGravedad = 0;
                fuerzaGravedad = fuerzaSalto;
            }
        }
        else { 
            fuerzaGravedad -= gravedad * Time.deltaTime; 
        }
        
        controladorCuerpo.Move(new Vector3(0, fuerzaGravedad, 0) * Time.deltaTime);
    }
}
