using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private bool _fadeOut;
    
    [SerializeField] private ConoceController _conoceController;
    
    public void ManageScreen()
    {
        StartCoroutine(LoadConoce());
    }
    
    IEnumerator LoadConoce()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
            _conoceController.ManageScreen(_index, _fadeOut, null);
            ReticlePointerController.Instace.ready = false;
        }
    }
}
