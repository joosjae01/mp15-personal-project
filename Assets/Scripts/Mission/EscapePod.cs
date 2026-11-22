using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapePod : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _commandLength;

    public List<Command> Sequence { get; private set; } = new();

    public string Desc => "비컨 요청";
    private PodCommandInput _input;
    private PlayerController _player;
    private bool _used;

    private Command[] _commandPool =
    {
        Command.UP, Command.DOWN, Command.LEFT, Command.RIGHT
    };
    
    public void Init(PodCommandInput input, PlayerController player)
    {
        _input = input;
        _player = player;
    }

    public void Generate()
    {
        Sequence.Clear();
        for(int i = 0; i < _commandLength; i++)
        {
            Sequence.Add(_commandPool[UnityEngine.Random.Range(0, _commandPool.Length)]);
        }
    }

    public void Interact()
    {
        if (_used || _input.IsRunning) return;
        _input.Begin(Sequence, OnSuccess, OnCancel);
    }

    private void OnSuccess()
    {
        _used = true;
        GameObject beacon = Instantiate(_prefab);
        _player.HoldBeacon(beacon);
    }

    private void OnCancel()
    {

    }
}
