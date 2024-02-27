using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float sensibilidadMouse = 2f;
    public float rotacionHorizontal = 0;
    private float rotacionVertical = 0f;
    [SerializeField] private LayerMask _layerButtons;
    [SerializeField] private InputController _inputController;

    void Update()
    {
        // Obtener la entrada del mouse para la rotación horizontal
        rotacionHorizontal += Input.GetAxis("Mouse X") * sensibilidadMouse;

        // Obtener la entrada del mouse para la rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadMouse;

        // Limitar la rotación vertical entre ciertos ángulos para evitar que la cámara gire completamente
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90f, 90f);

        // Aplicar la rotación vertical a la cámara
        transform.localRotation = Quaternion.Euler(rotacionVertical, rotacionHorizontal, 0f);

        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, _layerButtons);
        _inputController.distance = hit.transform != null ? Vector3.Distance(transform.position, hit.point) : .7f;
    }
}
