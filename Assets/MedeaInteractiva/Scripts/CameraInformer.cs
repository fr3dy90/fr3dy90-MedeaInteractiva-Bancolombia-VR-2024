using UnityEngine;

public class CameraInformer : MonoBehaviour
{
   public void CameraControllerInfo()
   {
      CameraController.Instance.OnIntroFinished();
   }
}
