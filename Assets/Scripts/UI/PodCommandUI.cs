using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PodCommandUI : MonoBehaviour
{
    [SerializeField] private CommandArrow _prefab;
    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _panel;

    private List<CommandArrow> _arrows = new();

    public void Show(List<Command> sequence)
    {
        foreach(CommandArrow arrow in _arrows)
        {
            Destroy(arrow.gameObject);
        }
        _arrows.Clear();

        foreach(Command cmd in sequence)
        {
            CommandArrow arrow = Instantiate(_prefab, _root);
            arrow.SetDirection(cmd);
            _arrows.Add(arrow);
        }

        SetProgress(0);
        _panel.SetActive(true);
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }

    public void SetProgress(int index)
    {
        for(int i = 0; i < _arrows.Count; i++)
        {
            _arrows[i].SetActive(i < index);
        }
    }
}
