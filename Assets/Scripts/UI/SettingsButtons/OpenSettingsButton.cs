public class OpenSettingsButton : SettingsButton
{
    protected override void ChangeMenuState()
    {
        Panel.SetActive(true);

        if (MouseDrag != null)
            MouseDrag.enabled = false;

        if (TouchDrag != null)
            TouchDrag.enabled = false;
    }
}