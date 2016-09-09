using System.Windows;

namespace Randumizer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public string load_file;
        private void add_element_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Nie wypełniłeś żadnymi danymi!", "Error");
            }
            else
            {
                using (System.IO.StreamWriter file = 
                    new System.IO.StreamWriter(load_file, true))
                {
                    file.WriteLine(textBox.Text);
                    this.Close();
                }
            }
        }
    }
}
