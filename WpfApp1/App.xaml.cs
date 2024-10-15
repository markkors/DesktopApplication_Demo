using System.Configuration;
using System.Data;
using System.Windows;
using WpfLibrary1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // make connection to the library
        public static DBContext dbContext = new DBContext();
    }

}
