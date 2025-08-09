using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isChecked = false;

    public bool IsChecked()
    {
        return _isChecked; 
    }

    public void TacResource()
    {
        _isChecked = true;
    }

    public void OnDestroed()
    {
        Destroy(this.gameObject);
    }
}
