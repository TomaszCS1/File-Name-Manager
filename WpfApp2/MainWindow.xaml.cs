using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;



namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string filePath;
        string directPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
           
            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                tb1.Text = filePath;
                                             
            }

        }
              

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOpenDiretory(object sender, RoutedEventArgs e)
        {
            
            
            OpenFileDialog openFileDialogDir = new OpenFileDialog();
            if (openFileDialogDir.ShowDialog()==true)
            {
                filePath = openFileDialogDir.FileName;
               directPath = System.IO.Path.GetDirectoryName(filePath);



                string[] files = Directory.GetFiles(directPath);


                lbFilesInDirectory.Items.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    lbFilesInDirectory.Items.Add(i+1 + ". " + System.IO.Path.GetFileName(files[i]));
                    lbFilesNewNames.Items.Add(i + 1 + ". " + System.IO.Path.GetFileName(files[i]));
                }
            }
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fbd = new FolderBrowserDialog;


        }

      
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string copySelected = lbFilesInDirectory.SelectedItem.ToString();
            tbName.Text = copySelected;
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbFilesNewNames.Items.Add(tbName.Text);
            //int c = lbFilesNewNames.Items.Count;
            //Console.WriteLine("NAME TEMP= "+c);     



        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //test
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
