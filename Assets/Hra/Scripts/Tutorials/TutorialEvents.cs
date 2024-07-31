using System;

public static class TutorialEvents
{
    public static event Action<string> OnItemClicked;
    public static void OnItemClickedInvoke(string friendlyId)
    {
        OnItemClicked?.Invoke(friendlyId);
    }

    public static event Action OnElementClicked;
    public static void OnElementClickedInvoke()
    {
        OnElementClicked?.Invoke();
    }
}
