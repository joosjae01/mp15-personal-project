using System.Collections.Generic;
using UnityEngine;

public interface IViewFactory
{
    public Dictionary<int, IView> Create(ViewContext context);
}
