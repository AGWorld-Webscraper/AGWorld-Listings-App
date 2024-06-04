namespace AGWorld_Listings_App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Form1 form;
            if(args.Length != 0 ) 
            {
                form = new Form1(args[0]);
            } else
            {
                form = new Form1();
            }
            
            Application.Run(form);
        }
    }
}