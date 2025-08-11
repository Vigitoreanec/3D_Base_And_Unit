using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private int count = 0;

    public void CountersAdd()
    {

        count++;
        _text.text = count.ToString();

    }

    public int GetCount()
    {
        return count;
    }

    public void MinusByDron()
    {
        count -= 3;
        _text.text = count.ToString();
    }

    public void MinusByBase()
    {
        count -= 5;
        _text.text = count.ToString();
    }
}
