using Microsoft.Extensions.DependencyInjection;

namespace AppBindingCommands
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DateTime data = DateTime.Now;
            Preferences.Set("dtAtual", data);
            Preferences.Set("AcaoInicial", $" * App executado as {data}");
        }


        protected override void OnStart()
        {
            base.OnStart();
            Preferences.Set("AcaoStart",$"App iniciado as {DateTime.Now}");
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Preferences.Set("AcaoSleep", $"App iniciado as {DateTime.Now}");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Preferences.Set("AcaoResume", $"App iniciado as {DateTime.Now}");
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}