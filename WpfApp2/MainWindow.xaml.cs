using System.Text.Json;
using System.Windows;
using WpfApp2.Models;
using WpfLibrary1.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBContext dbContext = new DBContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // get JSON data from the library and deserialize it
            string JSON = dbContext.GetAllData();
            // Deserialiseer JSON naar een Student object
            List<Student>? students = new List<Student>();

            students = JsonSerializer.Deserialize<List<Student>>(JSON);
            if (students != null)
            {
                MessageBox.Show($"{students.Count.ToString()} studenten");
                // Use the students object
            }
        }
    }
}