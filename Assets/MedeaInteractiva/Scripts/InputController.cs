using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvedUI;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IEventSystemHandler
{
#pragma warning disable 0649

    [SerializeField] CurvedUISettings mySettings;
    [SerializeField] Transform pivot;
    [SerializeField] float sensitivity = 0.1f;
#pragma warning restore 0649

    Vector3 lastMouse;
    public bool isMouseMove;
    public bool isDragging;
    Ray myRay;
    RaycastHit hit;
    public GameObject target;

    private static InputController instace;
    public float distance;
    public bool detecting;
    Transform initialDistance;

    public static InputController Instace { get => instace; set => instace = value; }

    void Awake()
    {
        Instace = this;
    }

    void Start()
    {
        lastMouse = Input.mousePosition;
    }

    void Update()
    {
        //OculusController();
        if(isDragging && target != null)
        {
            if(detecting)
                StartCoroutine(_DetectTargetCategory());
            target.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }

    /*void OculusController()
    {
#if UNITY_EDITOR
        if (isMouseMove)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMouse;
            lastMouse = Input.mousePosition;
            pivot.localEulerAngles += new Vector3(-mouseDelta.y, mouseDelta.x, 0) * sensitivity;
        }
#endif

        //pass ray to canvas
        myRay = new Ray(this.transform.position, this.transform.forward);

        CurvedUIInputModule.CustomControllerRay = myRay;
        CurvedUIInputModule.CustomControllerButtonState = Input.GetButton("Fire1");

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.GetComponent<DraggableObject>())
                {
                    if (target == null)
                    {
                        target = hit.collider.gameObject;
                        target.GetComponent<DraggableObject>().SetScale(false);
                    }
                }
            }

            if (target != null)
            {
                isDragging = Input.GetMouseButton(0);

                if (isDragging)
                {
                    Drag(hit);
                }
                else
                {
                    if (target.GetComponent<DraggableObject>().isDetectCategory)
                    {
                        if (target.GetComponent<DraggableObject>().isCorrectCategory)
                        {
                            LevelGame1.Instance.SetScore(target.GetComponent<DraggableObject>().category1);
                            LevelGame1.Instance.AudioPlayRetro(true);
                            Debug.Log("correcto");
                        }
                        else
                        {
                            LevelGame1.Instance.ShowObject();
                            LevelGame1.Instance.AudioPlayRetro(false);
                            Debug.Log("incorrecto");
                        }
                        target.SetActive(false);
                    }
                    else
                    {
                        target.GetComponent<DraggableObject>().SetScale(true);
                    }

                    target = null;
                }
            }
        }
    }
    */

    void Drag(RaycastHit hit)
    {
        if (isDragging)
        {
            target.transform.position = new Vector3(hit.point.x, hit.point.y, target.transform.position.z);
        }
    }

    IEnumerator _SelectTarget(GameObject target)
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            target.GetComponent<DraggableObject>().SetScale(false);
            //initialDistance = target.transform;
            isDragging = true;
            this.target = target;

            ReticlePointerController.Instace.ready = false;
        }
    }

    public void SelectTarget(GameObject target)
    {
        StartCoroutine(_SelectTarget(target));
    }

    public IEnumerator _DetectTargetCategory()
    {
        //detecting = false;
        if (target.GetComponent<DraggableObject>().isDetectCategory)
        {
            float seconds = ReticlePointerController.Instace.maxSliderValue;
            ReticlePointerController.Instace.loading = true;

            yield return new WaitForSeconds(seconds);

            
            
        }
    }

    private void _Detecting()
    {
        
    }

    

}
