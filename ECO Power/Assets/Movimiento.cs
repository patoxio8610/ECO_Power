using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float poderSalto = 2f;

    public Transform camara;
    public Transform modelo3D;

    public float distanciaSuelo = 0.2f;
    public LayerMask capaPiso;

    private Rigidbody cuerpoRigido;
    private Vector3 ejesEntrada = Vector3.zero;
    private bool pisoColision = true;
    private Transform verificadorPiso;

    private Vector3 movimiento;
    private Vector3 direccionMovimiento;

    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        verificadorPiso = transform.GetChild(0);
    }

    void Update()
    {
        pisoColision = Physics.CheckSphere(verificadorPiso.position, distanciaSuelo, capaPiso, QueryTriggerInteraction.Ignore);

        ejesEntrada = Vector3.zero;
        ejesEntrada.x = Input.GetAxis("Horizontal");
        ejesEntrada.z = Input.GetAxis("Vertical");

        movimiento = Quaternion.Euler(0, camara.transform.eulerAngles.y, 0) * new Vector3(ejesEntrada.x, 0, ejesEntrada.z);
        direccionMovimiento = movimiento.normalized;
        modelo3D.eulerAngles = new Vector3 (0, camara.transform.eulerAngles.y , 0);
        if (Input.GetButtonDown("Jump") && pisoColision)
            cuerpoRigido.AddForce(Vector3.up * Mathf.Sqrt(poderSalto * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        cuerpoRigido.MovePosition(cuerpoRigido.position + direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime);
    }
}
