using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Randumizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] t;
        public string sourceCode = "";

        private Window1 form;

        public class items
        {
            public string name { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_position_Click(object sender, RoutedEventArgs e)
        {
            if (sourceCode != "")
            {
                form = new Window1();
                form.load_file = sourceCode;
                form.Show();
            }
            else
                MessageBox.Show("NIE WYBRANO PLIKU");
        }

        private void show_base_Click(object sender, RoutedEventArgs e)
        {
            show_base_fn();
        }

        private void show_base_fn()
        {
            if (sourceCode != "")
            {
                t = File.ReadAllLines(sourceCode);
                List<items> tmp = new List<items>();
                foreach (string _base in t)
                {
                    tmp.Add(new items() { name = _base });
                }
                position_list.ItemsSource = tmp;
            }
            else
                MessageBox.Show("NIE WYBRANO PLIKU");
        }

        private void rnd_Click(object sender, RoutedEventArgs e)
        {
            add_position.IsEnabled = false;
            show_base.IsEnabled = false;
            rnd.IsEnabled = false;
            choose_file.IsEnabled = false;

            if (sourceCode != "")
            {
                int cnt = 0;
                bool flag = to_remove_from_base.IsChecked.Value;
                t = File.ReadAllLines(sourceCode);
                foreach (string _base in t)
                {
                    if (_base.Length >0)
                        ++cnt;
                }
                if (cnt > 0)
                {
                    if (!flag)
                        rand_elements(0);
                    else
                        rand_elements(1);
                }
                else
                    MessageBox.Show("BAZA PUSTA :(");

            }
            else
                MessageBox.Show("NIE WYBRANO PLIKU");

            add_position.IsEnabled = true;
            show_base.IsEnabled = true;
            rnd.IsEnabled = true;
            choose_file.IsEnabled = true;

            show_base_fn();
        }

        private void choose_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "text files (*.txt)|*.txt";
            dialog.FilterIndex = 1;
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                sourceCode = System.IO.Path.GetDirectoryName(dialog.FileName);
                sourceCode += ("\\" + System.IO.Path.GetFileName(dialog.FileName));
                rnd_screen.Text = (sourceCode);
            }

        }

        private void rand_elements(int choose)
        {
            StringBuilder newFile = new StringBuilder();
            int choice = 0;
            t = File.ReadAllLines(sourceCode);
            List<string> tmp = new List<string>();
            foreach (string _base in t)
            {
                tmp.Add(_base);
            }
            List<string> tmp2 = new List<string>();
            tmp2.Clear();
            Random rnd = new Random();
            for (int i = 0; i < 2500; ++i)
            {
                choice = rnd.Next(tmp.Count());
                tmp2.Add(tmp[choice]);
            }

            // ***********************************************

            // removing some elements form base

            if (choose > 0)
            {
                using (StreamWriter writer = new StreamWriter(sourceCode))
                {
                    foreach(string _base in t)
                    {
                        if (_base == tmp[choice])
                            continue;
                        writer.WriteLine(_base);
                    }
                }
            }
            
            // ****************************
            // calculating how many times some element was choosen

            Show_dict(tmp2);

            MessageBox.Show("Wybór padł na: " + tmp[choice]);
        }

        private void Show_dict(List<string> tmp2)
        {
            string u = "";
            var dict = new Dictionary<string, int>();

            foreach (var i in tmp2)
                if (dict.ContainsKey(i))
                    dict[i]++;
                else
                    dict[i] = 1;

            foreach (var p in dict)
                if (p.Value == 1)
                    u = p.Key;

            dict.Remove(u);
            rnd_screen_Copy.Text = "";
            foreach (var d in dict)
                rnd_screen_Copy.Text += (d.Key + " wystepuje " + d.Value + " razy" + Environment.NewLine);
        }
    }

}
