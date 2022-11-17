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
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace WpfApp2
{
    
    public partial class MainWindow : Window
    {

        string directPath;
        string[] files;
        public string anzBuchstabenStr;
        public int anzBuchstaben;
        List<string> filesNewNames = new List<string>();

        public MainWindow()
        {

            InitializeComponent();
        }

        


        private void btnClose(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnOpenDiretory(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directPath = fbd.SelectedPath;
            
            }
                if (directPath != null)
                {
                    files = Directory.GetFiles(directPath);


                    lbFilesInDirectory.Items.Clear();
                    for (int i = 0; i < files.Length; i++)
                    {
                        lbFilesInDirectory.Items.Add(i + 1 + ". " + System.IO.Path.GetFileName(files[i]));
                    }
                }
         }
        

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


      

        private void aenderVorschau(object sender, RoutedEventArgs e)                            //TODO: sicherung falls neue benennung ist nicht eindeutig (es kann nicht 2 dateien mit gleichen Namen geben)
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
                        filesNewNames.Add(newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                }


            }
            else if (rbAmEnde.IsChecked == true && tbAnzBuchst.Text.Length != 0)               //AM ENDE KURZEN
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
                        int endeString = laengeBenenn - anzBuchstaben;
                        string newFilename = fileName.Remove(endeString, anzBuchstaben);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename); 
                        filesNewNames.Add(newFilename);
                    }

                }

            }
            else if (tbAltText1.Text.Length != 0 && tbAltText2.Text.Length != 0)                                                //ALTER TEXT 1 und ALTER TEXT 2 ersätzt
            {
                lbFilesNewNames.Items.Clear();
                int anzBuchstaben = tbAltText2.Text.Length;

                string altText1 = tbAltText1.Text;
                string neuText1 = tbNeuText1.Text;
                string altText2 = tbAltText2.Text;
                string neuText2 = tbNeuText2.Text;

                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);
                    string newFilename2;
                    string newFilename1;

                    int laengeBenenn1 = fileName.IndexOf(altText1);                                                                 
                    int leangeBenenn2 = laengeBenenn1 + altText1.Length;                                                                                       
                    int laengeBenenn3 = fileName.IndexOf(altText2);                                                                
                    int leangeBenenn4 = laengeBenenn3 + altText2.Length;

                    if (laengeBenenn1 >= 0)                     //alter text1 gefunden
                    {
                        newFilename1 = fileName.Remove(laengeBenenn1, fileName.Length - laengeBenenn1) + neuText1 + fileName.Substring(leangeBenenn2);
                        if (laengeBenenn3 > 0)                  //alter text2 gefunden
                        {
                            newFilename2 = newFilename1.Remove(laengeBenenn3, fileName.Length - laengeBenenn3) + neuText2 + newFilename1.Substring(leangeBenenn4);
                            newFilename1 = newFilename2;
                        }

                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename1);
                        filesNewNames[i] = newFilename1;
                    }     
                    else if (laengeBenenn1<0 && laengeBenenn3>=0)
                    {
                        newFilename2 = fileName.Remove(laengeBenenn3, fileName.Length - laengeBenenn3) + neuText2 + fileName.Substring(leangeBenenn4);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename2);
                        filesNewNames[i] = newFilename2;

                    }


                    else
                        {
                            string newFilename = fileName;
                            lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                            filesNewNames.Add(newFilename);
                    }
                            }
            }
            else if (tbAltText1.Text.Length != 0)                                                //ALTER TEXT 1
            {
                lbFilesNewNames.Items.Clear();
               

                string altText1 = tbAltText1.Text;
                string neuText1 = tbNeuText1.Text;
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);

                    int laengeBenenn1 = fileName.IndexOf(altText1);                                                                //die Länge der Benennung vor dem alter Text 1  
                    int leangeBenenn2 = laengeBenenn1 + altText1.Length;                                                                                       //Index der Stelle nach dem alter Text 1 
                    if (laengeBenenn1 >= 0)
                    {
                        string newFilename = fileName.Remove(laengeBenenn1, fileName.Length - laengeBenenn1) + neuText1 + fileName.Substring(leangeBenenn2);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                }

            }
            else if (tbAltText2.Text.Length != 0)                                                //ALTER TEXT 2
            {
                lbFilesNewNames.Items.Clear();
                
                string altText2 = tbAltText2.Text;
                string neuText2 = tbNeuText2.Text;
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = System.IO.Path.GetFileName(files[i]);

                    int laengeBenenn1 = fileName.IndexOf(altText2);                                                                //die Länge der Benennung vor dem alter Text 1  
                    int leangeBenenn2 = laengeBenenn1 + altText2.Length;                                                                                       //Index der Stelle nach dem alter Text 1 
                    if (laengeBenenn1 >= 0)
                    {
                        string newFilename = fileName.Remove(laengeBenenn1, fileName.Length - laengeBenenn1) + neuText2 + fileName.Substring(leangeBenenn2);
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                    else
                    {
                        string newFilename = fileName;
                        lbFilesNewNames.Items.Add(i + 1 + ". " + newFilename);
                        filesNewNames.Add(newFilename);
                    }
                }
            }
            
            else if (files == null)
            {
                System.Windows.MessageBox.Show("Choose directory -> DIRECTORY OPEN");

            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < files.Length; i++)

            {
                string neuPfad = System.IO.Path.GetDirectoryName(files[i]) + "\\" + filesNewNames[i];
                if (files[i] != neuPfad)                                                                                //wenn Änderungen: alte Bennen nicht gleich neue Dateiname
                {
                    
                    File.Copy(files[i], neuPfad);                                                                       //neue Datei erstellt
                    File.Delete(files[i]);                                                                              //alte Datei gelöscht
                    
                }
                

            }
            filesNewNames.Clear();
            lbFilesNewNames.Items.Clear();
            tbAltText1.Clear();
            tbAltText2.Clear();
            tbNeuText1.Clear();
            tbNeuText2.Clear();
            tbAnzBuchst.Clear();


            if (directPath != null)                                             //listbox alt wird aktualisiert mit neuen Benennungen
            {
                files = Directory.GetFiles(directPath);


                lbFilesInDirectory.Items.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    lbFilesInDirectory.Items.Add(i + 1 + ". " + System.IO.Path.GetFileName(files[i]));
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}