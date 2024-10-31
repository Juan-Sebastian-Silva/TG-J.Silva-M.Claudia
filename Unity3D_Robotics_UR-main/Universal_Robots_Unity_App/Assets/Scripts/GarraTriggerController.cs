using UnityEngine;

public class GarraTriggerController : MonoBehaviour
{
    public Transform pinza1;    // Asigna aquí la Pinza 1 en el Inspector
    public Transform pinza2;    // Asigna aquí la Pinza 2 en el Inspector
    public float velocidadMovimiento = 2f;  // Velocidad de apertura y cierre
    public float aperturaMaxima = 1f;        // Distancia máxima de apertura en el eje X

    private Vector3 posInicialPinza1;
    private Vector3 posInicialPinza2;

    private GameObject cajaAgarrada = null;  // Referencia a la caja agarrada

    // Variables para rastrear el estado de contacto de las pinzas
    private GameObject pinza1ContactoObjeto = null;
    private GameObject pinza2ContactoObjeto = null;

    void Start()
    {
        // Guardar posiciones iniciales
        posInicialPinza1 = pinza1.localPosition;
        posInicialPinza2 = pinza2.localPosition;
    }

    void Update()
    {
        // Movimiento de apertura y cierre de la garra
        if (Input.GetKey(KeyCode.O))
        {
            AbrirGarra();
        }

        if (Input.GetKey(KeyCode.C))
        {
            CerrarGarra();
        }
    }

    private void AbrirGarra()
    {
        // Mover pinza1 a la izquierda y pinza2 a la derecha hasta el límite de apertura máxima
        if (pinza1.localPosition.x > posInicialPinza1.x - aperturaMaxima &&
            pinza2.localPosition.x < posInicialPinza2.x + aperturaMaxima)
        {
            pinza1.localPosition += Vector3.left * velocidadMovimiento * Time.deltaTime;
            pinza2.localPosition += Vector3.right * velocidadMovimiento * Time.deltaTime;
        }
    }

    private void CerrarGarra()
    {
        // Mover pinza1 a la derecha y pinza2 a la izquierda para cerrar
        if (pinza1.localPosition.x < posInicialPinza1.x &&
            pinza2.localPosition.x > posInicialPinza2.x)
        {
            pinza1.localPosition += Vector3.right * velocidadMovimiento * Time.deltaTime;
            pinza2.localPosition += Vector3.left * velocidadMovimiento * Time.deltaTime;
        }
    }

    // Métodos para que las PinzaColliders informen sobre el estado de contacto
    public void SetPinzaContacto(string pinzaNombre, GameObject objeto)
    {
        if (pinzaNombre == "Pinza1")
        {
            pinza1ContactoObjeto = objeto;
        }
        else if (pinzaNombre == "pinza2")
        {
            pinza2ContactoObjeto = objeto;
        }

        VerificarYAgarrar();
    }

    public void ClearPinzaContacto(string pinzaNombre, GameObject objeto)
    {
        if (pinzaNombre == "Pinza1" && pinza1ContactoObjeto == objeto)
        {
            pinza1ContactoObjeto = null;
        }
        else if (pinzaNombre == "pinza2" && pinza2ContactoObjeto == objeto)
        {
            pinza2ContactoObjeto = null;
        }

        // Si cualquiera de las pinzas pierde el contacto, soltar la caja
        if (cajaAgarrada != null)
        {
            SoltarCaja();
        }
    }

    private void VerificarYAgarrar()
    {
        // Verificar que ambas pinzas están en contacto y con el mismo objeto
        if (pinza1ContactoObjeto != null &&
            pinza2ContactoObjeto != null &&
            pinza1ContactoObjeto == pinza2ContactoObjeto &&
            cajaAgarrada == null)
        {
            AgarrarCaja(pinza1ContactoObjeto);
        }
    }

    // Función para agarrar la caja
    public void AgarrarCaja(GameObject caja)
    {
        Debug.Log("Caja agarrada por la garra.");
        cajaAgarrada = caja;
        cajaAgarrada.transform.parent = this.transform;  // Asignar la caja como hija de la garra para moverla junto con la garra
        Rigidbody rb = cajaAgarrada.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Desactivar la física mientras está siendo levantada
        }
    }

    // Función para soltar la caja
    public void SoltarCaja()
    {
        if (cajaAgarrada != null)
        {
            Debug.Log("Caja soltada.");
            cajaAgarrada.transform.parent = null;  // Soltar la caja
            Rigidbody rb = cajaAgarrada.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;  // Reactivar la física
            }
            cajaAgarrada = null;
        }
    }
}
