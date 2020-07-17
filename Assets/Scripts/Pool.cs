using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _countObject;
    private List<Transform> _objects = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < _countObject; i++)
        {
            GameObject gameObject = Instantiate(_template, transform);
            _objects.Add(gameObject.transform);
            gameObject.transform.SetParent(transform);
            gameObject.SetActive(false);
        }
    }

    public List<Transform> GetElementList()
    {
        return _objects;
    }

    public Transform GetElementByIndex(int index)
    {
        return _objects[index];
    }

    public Transform GetAvailableElement()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i].gameObject.activeSelf == false)
            {
                return _objects[i];
            }
        }
        return null;
    }

    public List<Transform> GetAvailableElements(int count)
    {
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < _objects.Count; i++)
        {
            if (count <= 0) break;
            if (_objects[i].gameObject.activeSelf == false)
            {
                list.Add(_objects[i]);
                count--;
            }
        }
        return list;
    }

    public void MoveFirstElementToEndList()
    {
        Transform listElement = _objects[0];
        _objects.Remove(listElement);
        _objects.Add(listElement);
    }

}
