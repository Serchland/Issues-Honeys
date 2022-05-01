using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using MahApps.Metro.Controls;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Issues_Honeys.Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IApplicationCommands _applicationCommands;

        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();
            _applicationCommands = applicationCommands;
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _applicationCommands.NavigateCommand.Execute(CommandParameters.Issues);

            //SERCH00: Only for test pourposes
            ReadConfig();
        }

        private void ReadConfig()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;

            string queryString = "SELECT * FROM dbo.Users;";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                    }
                }
            }
        }
    }
}
