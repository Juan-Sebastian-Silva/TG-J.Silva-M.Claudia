using UnityEngine;

public class PinzaCollider : MonoBehaviour
{
    private GarraTriggerController garraController;
    private string pinzaNombre;

    void Start()
    {
        // Buscar el script GarraTriggerController en el padre
        garraController = GetComponentInParent<GarraTriggerController>();

        // Asignar el nombre de la pinza basado en el nombre del GameObject
        pinzaNombre = gameObject.name;
    }

    // Cuando algo entra en el collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objeto"))
        {
            garraController.SetPinzaContacto(pinzaNombre, other.gameObject);
        }
    }

    // Cuando algo sale del collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Objeto"))
        {
            garraController.ClearPinzaContacto(pinzaNombre, other.gameObject);
        }
    }
}
