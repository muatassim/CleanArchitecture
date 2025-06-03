namespace CleanArchitecture.DataInitializer
{
    public static class Initializer
    {
        public static void Run(DataInitializerOptions dataInitializerOptions )
        { 
            ArgumentNullException.ThrowIfNull(dataInitializerOptions);
            if (!dataInitializerOptions.IsValid())
            {
                throw new ArgumentException(dataInitializerOptions.ValidateMessage, nameof(dataInitializerOptions));
            }  
            if (dataInitializerOptions.DeployDacPac)
            {  
                // Resolve the logger from the service provid 
                var databaseCreator = new DatabaseCreator(dataInitializerOptions);
                databaseCreator.Run(); 
            } 
        }
    }
}
