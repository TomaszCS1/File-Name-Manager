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
//using System.Windows.Forms;



namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string filePath;
        string directPath;
        string[] files;
        public string anzBuchstabenStr;
        public int anzBuchstaben;

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
            if (openFileDialogDir.ShowDialog() == true)
            {
                filePath = openFileDialogDir.FileName;
                directPath = System.IO.Path.GetDirectoryName(filePath);



                files = Directory.GetFiles(directPath);


                lbFilesInDirectory.Items.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    lbFilesInDirectory.Items.Add(i + 1 + ". " + System.IO.Path.GetFileName(files[i]));

                }
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fbd = new FolderBrowserDialog;


        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // string copySelected = lbFilesInDirectory.SelectedItem.ToString();
            //tbName.Text = copySelected;
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lbFilesNewNames.Items.Add(tbName.Text);
            //int c = lbFilesNewNames.Items.Count;
            //Console.WriteLine("NAME TEMP= "+c);     



        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //test
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
          
        }

        private void aenderVorschau(object sender, RoutedEventArgs e)
        {


            if (rbAmAnfang.IsChecked == true && tbAnzBuchst.Text.Length != 0)                    //AM ANFANG KURZEN
            {
                lbFilesNewNames.Items.Clear();
                anzBuchstabenStr = tbAnzBuchst.Text;
                anzBuchstaben = Int32.Parse(anzBuchstabenStr);
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);
                    int laengeBenenn = fileName.IndexOf(".");

                    if (laengeBenenn >= anzBuchstaben)
                    {
                        int endeString = laengeBenenn - anzBuchstaben;
                        string newFilename = fileName.Remove(0, anzBuchstaben);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    }
                }


            } else if (rbAmEnde.IsChecked == true && tbAnzBuchst.Text.Length != 0)               //AM ENDE KURZEN
            {
                anzBuchstabenStr = tbAnzBuchst.Text;
                anzBuchstaben = Int32.Parse(anzBuchstabenStr);
                lbFilesNewNames.Items.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);
                    int laengeBenenn = fileName.IndexOf(".");
                    if (laengeBenenn > anzBuchstaben)
                    {
                        int endeString =laengeBenenn - anzBuchstaben;
                        string newFilename = fileName.Remove(endeString, anzBuchstaben);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    } else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    }
                                    
                }

            } else if (tbAltText1.Text.Length !=0)                                                //ALTER TEXT 1
            {
                lbFilesNewNames.Items.Clear();
                int anzBuchstaben = tbAltText1.Text.Length;

                string altText1 = tbAltText1.Text;
                string neuText1 = tbNeuText1.Text;
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);

                    int laengeBenenn1 = fileName.IndexOf(altText1);                                                                //die Länge der Benennung vor dem alter Text 1  
                    int leangeBenenn2 = laengeBenenn1 + altText1.Length;                                                                                       //Index der Stelle nach dem alter Text 1 
                    if (laengeBenenn1 > 0)
                    {
                        string newFilename = fileName.Remove(laengeBenenn1, fileName.Length-laengeBenenn1) + neuText1 + fileName.Substring(leangeBenenn2);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                    }
                }
                
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

         private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void tbAltText1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}