using UnityEngine;

public class GarraController : MonoBehaviour
{
    public Transform pinza1;  // Asigna aquí la Pinza 1 en el Inspector
    public Transform pinza2;  // Asigna aquí la Pinza 2 en el Inspector
    public float velocidadMovimiento = 2f;  // Velocidad de apertura y cierre
    public float aperturaMaxima = 1f;  // Distancia máxima de apertura en el eje X

    private Vector3 posInicialPinza1;
    private Vector3 posInicialPinza2;

    void Start()
    {
        // Guardar posiciones iniciales
        posInicialPinza1 = pinza1.localPosition;
        posInicialPinza2 = pinza2.localPosition;
    }

    void Update()
    {
        // Mover la garra hacia los lados mientras se mantiene presionada la tecla
        if (Input.GetKey(KeyCode.O))
        {
            // Mover pinza1 a la izquierda y pinza2 a la derecha hasta el límite de apertura máxima
            if (pinza1.localPosition.x > posInicialPinza1.x - aperturaMaxima && pinza2.localPosition.x < posInicialPinza2.x + aperturaMaxima)
            {
                pinza1.localPosition = new Vector3(pinza1.localPosition.x - velocidadMovimiento * Time.deltaTime, posInicialPinza1.y, posInicialPinza1.z);
                pinza2.localPosition = new Vector3(pinza2.localPosition.x + velocidadMovimiento * Time.deltaTime, posInicialPinza2.y, posInicialPinza2.z);
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            // Mover pinza1 a la derecha y pinza2 a la izquierda para cerrar
            if (pinza1.localPosition.x < posInicialPinza1.x && pinza2.localPosition.x > posInicialPinza2.x)
            {
                pinza1.localPosition = new Vector3(pinza1.localPosition.x + velocidadMovimiento * Time.deltaTime, posInicialPinza1.y, posInicialPinza1.z);
                pinza2.localPosition = new Vector3(pinza2.localPosition.x - velocidadMovimiento * Time.deltaTime, posInicialPinza2.y, posInicialPinza2.z);
            }
        }
    }
}
