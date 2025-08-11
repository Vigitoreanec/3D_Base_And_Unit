using UnityEngine;

public class CreateBase : MonoBehaviour
{
    [SerializeField] private CommandCentr _base;

    private void Update()
    {
        StartPosition();
    }

    private void StartPosition()
    {
        if (Input.GetMouseButtonDown(0) && _base.IsCreateBase())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 vector = hit.point;
                if (_base != null)
                {
                    _base.ClickPosition(vector);
                    Debug.Log("Create Base" + vector.ToString());
                }
                else
                {
                    Debug.Log("Can`t Create Base");
                }
            }
        }
    }
}
