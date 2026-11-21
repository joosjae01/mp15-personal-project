using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

public class ViewFactory : IViewFactory
{
    [Inject] private IObjectResolver _resolver;

    public Dictionary<int, IView> Create(ViewContext context)
    {
        Dictionary<int, IView> tempDict = new();
        for (int i = 0; i < context.Count; i++)
        {
            IView tempView;
            if (context.Type == ViewType.WeaponSlot)
            {
                tempView = _resolver.Instantiate(context.Prefab, context.Parent).GetComponent<WeaponSlotView>();
                (tempView as WeaponSlotView).Create(i);
                tempDict[i] = tempView;
            }
        }
        return tempDict;
    }
}
