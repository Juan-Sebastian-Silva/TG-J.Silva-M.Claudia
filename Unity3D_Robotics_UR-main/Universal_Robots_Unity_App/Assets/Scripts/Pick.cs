using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public GameObject handPoint;

    private GameObject pickedObject = null;

    // Update is called once per frame
    void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                // Soltar el objeto
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();

                rb.useGravity = true;
                rb.isKinematic = false;

                pickedObject.transform.SetParent(null); // Desasociar el objeto de la mano

                pickedObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Objeto") && pickedObject == null)
        {
            if (Input.GetKey("e"))
            {
                // Recoger el objeto
                Rigidbody rb = other.GetComponent<Rigidbody>();

                rb.useGravity = false;
                rb.isKinematic = true;

                other.transform.position = handPoint.transform.position;
                other.transform.rotation = handPoint.transform.rotation; // Alinear rotación también

                other.transform.SetParent(handPoint.transform); // Hacer al objeto hijo de la mano

                pickedObject = other.gameObject;
            }
        }
    }
}

