public class CloseSettingsButton : SettingsButton
{
    protected override void ChangeMenuState()
    {
        Panel.SetActive(false);

        if (MouseDrag != null)
            MouseDrag.enabled = true;

        if (TouchDrag != null)
            TouchDrag.enabled = true;
    }
}