
using UnityEngine;
using UnityEngine.UI;

public class Dron : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _base;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _cargo;

    private int _currentWayPoint = 0;
    private bool _isHaveResourse = false;
    private bool _isHaveCommand = false;
    private Transform _target;
    private Resource _tempResourse;
   

    private void Update()
    {
        if (_isHaveCommand)
        {
            MoveTarget(_target);
        }
        else
        {
            if (_isHaveResourse)
            {
                MoveTarget(_base);
            }
            else if (!_isHaveResourse && !_isHaveCommand)
            {

                FreeMove();
            }
            else
            {
                MoveTarget(_target);
            }
        }
        
    }

    private void MoveTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        transform.LookAt(target.position);
    }

    private void FreeMove()
    {
        transform.position = Vector3.MoveTowards( transform.position, 
            _points[_currentWayPoint].position, _speed * Time.deltaTime);
        transform.LookAt(_points[_currentWayPoint]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Point point))
        {
            _currentWayPoint = ++_currentWayPoint % _points.Length;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out CommandCentr component))
        {

            if (_isHaveResourse)
            {
                UnloadCargo();
                component.AddCount();

                component.AddDron(this);
                
            }
            
        }
        else if(collision.gameObject.TryGetComponent(out Resource resources))
        {
            if(_target != null)
            {
                if (resources.transform.position == _target.position)
                {
                    LoadCargo(resources);
                }
            }
           
        }
    }

    private void UnloadCargo()
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.TryGetComponent(out Resource resource))
            {
                _tempResourse = resource;
                _tempResourse.transform.parent = null;
                _isHaveResourse = false;
                _target = null;
                resource.OnDestroed();
                //Destroy(_tempResourse);
            }
        }
        
        
    }

    private void LoadCargo(Resource resource)
    {
        resource.transform.SetParent(this.transform);
        resource.transform.position = _cargo.position;
        _isHaveCommand = false;
        _isHaveResourse = true;
    }

    public void TakeCommand(Transform resourse)
    {
        _target = resourse;
        _isHaveCommand = true;
    }
}
