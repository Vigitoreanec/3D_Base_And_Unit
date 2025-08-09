using System.Collections;
using UnityEngine;

public class SpawnerResourse : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Resource _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _max;

    private int _countRes = 0;
    private WaitForSeconds _wait;
    private Vector3 _spawnPosition;
    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            _spawnPosition = new Vector3(Random.Range(_min.position.x,_max.position.x),0.525f, 
                Random.Range(_min.position.z,_max.position.z));

            Instantiate(_prefab, _spawnPosition, Quaternion.identity, _container);
            _countRes++;
            yield return _wait;
        }
    }

    public int GetCountRes()
    {
        return _countRes;
    }
}
