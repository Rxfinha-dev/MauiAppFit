using MauiAppFit.Helpers;

namespace MauiAppFit
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper database;
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                if (database == null) {
                    database = new SQLiteDatabaseHelper(                    
                        Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                            "XamAppFit.db3"
                        )
                    );
                    
                   
                }

                return database;
            }
            
        }//Fecha Database...
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}