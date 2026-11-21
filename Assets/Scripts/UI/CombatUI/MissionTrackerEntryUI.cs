using TMPro;
using UnityEngine;

public class MissionTrackerEntryUI : MonoBehaviour
{
    [SerializeField] private GameObject _entryActive;
    [SerializeField] private GameObject _markerActive;
    [SerializeField] private GameObject _entryDone;
    [SerializeField] private GameObject _markerDone;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _condition;

    public void Init(string name)
    {
        _name.text = name;
        SetDone(false);
    }

    public void SetCondition(string condition) => _condition.text = condition;
    public void SetDone(bool done)
    {
        _entryActive.SetActive(!done);
        _markerActive.SetActive(!done);
        _entryDone.SetActive(done);
        _markerDone.SetActive(done);
    }
}
