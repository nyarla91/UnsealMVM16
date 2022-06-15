namespace Presenter.Tutorial
{
    public class FormTutorial : TutorialScreen
    {
        private void Start()
        {
            if (PermanentSave.Data.FormsUnlcoked.Count == 2)
                Show();
        }
    }
}