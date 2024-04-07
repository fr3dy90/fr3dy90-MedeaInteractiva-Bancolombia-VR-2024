using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class AvatarController : MonoBehaviour
{
   [SerializeField] private string _videoName;
   [SerializeField] private VideoPlayer _videoPlayer;
   [SerializeField] private string _url;
   //[SerializeField] private Vector3 _initialPosition;
   //[SerializeField] private Vector3 _endPosition;
   [SerializeField] private GameObject _avatar;
   
   private void Awake()
   {
      LoadVideo();
   }

   private void LoadVideo()
   {
      _url = Path.Combine(Application.streamingAssetsPath, _videoName + ".mp4");
      _videoPlayer.url = _url;
      _videoPlayer.Prepare(); 
   }  

   public void PlayVideo() 
   {
      _videoPlayer.Play();
      _avatar.SetActive(true);
      StartCoroutine(StartHome());
   }

   IEnumerator StartHome()
   {
      yield return new WaitForSeconds((float)_videoPlayer.length);
      SceneController.Instance.ChangeScene(MomentScene.Home);
      _avatar.gameObject.SetActive(false);
   }
}
