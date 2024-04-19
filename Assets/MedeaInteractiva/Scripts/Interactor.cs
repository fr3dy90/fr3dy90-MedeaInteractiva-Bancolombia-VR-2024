using UnityEngine;
using UnityEngine.Serialization;

public class Interactor : MonoBehaviour
{
   [Header("Item")] 
   [SerializeField] private Transform initialItemPostion;
   [SerializeField] private LayerMask _layerObject;
   [SerializeField] private LayerMask _layerValidator;

   [FormerlySerializedAs("isDragging")]
   [Header("System")] 
   [SerializeField] private bool _isDragging = false;
   [SerializeField] private Transform _target;
   [SerializeField] private Transform _validation;
   [SerializeField] private Camera _cam;
   [Range(0, 1f)] [SerializeField] private float _distanceFromCamera;
   [Range( 0, 100)] [SerializeField] private float _lerpSpeed;

   private Vector3 _vel;

   private void Update()
   {
      if (Input.GetMouseButton(0))
      {
         if (GetHit(_layerObject).transform != null)
         {
            if(_target == null) _target = GetHit(_layerObject).collider.transform;
         }

         if (GetHit(_layerValidator).transform != null)
         {
            _validation = GetHit(_layerValidator).transform;
         }
         else
         {
            _validation = null;
         }

         if (_target != null)
         {
            Vector3 worldPosition = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera));
            if (_validation != null)
            {
               _target.position = SetTargetPosition(_validation.position);
            }
            else
            {
               _target.position = SetTargetPosition(worldPosition);
            }
         }
      }

      if (Input.GetMouseButtonUp(0))
      {
         if (_target != null)
         {
            _target.GetComponent<DraggableObject>().OnRelease();
            _target = null;
         }
         _validation = null;
      }
   }

   private RaycastHit GetHit(LayerMask _layer)
   {
      Vector3 firstPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane);
      Vector3 lastPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.farClipPlane);
      Vector3 worldFirstPos = _cam.ScreenToWorldPoint(firstPoint);
      Vector3 worldLastPos = _cam.ScreenToWorldPoint(lastPoint);
      Physics.Raycast(worldFirstPos, worldLastPos - worldFirstPos, out RaycastHit hit,50F, _layer);
      //Debug.DrawRay();
      return hit;
   }

   public Vector3 SetTargetPosition(Vector3 _desirePosition)
   {
      return  Vector3.SmoothDamp(_target.position, _desirePosition, ref _vel, Time.deltaTime * _lerpSpeed);
   }
}
