using Unity.Behavior;
using UnityEngine;

public abstract class BlackBoardController
{
    protected BehaviorGraphAgent _agent;
    protected abstract void SetBoard();
}
