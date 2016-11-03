using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;

namespace TextEditor
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Диалоговое окно для отображения файлов
		private readonly OpenFileDialog myOpenFileDialog = new OpenFileDialog();
		private readonly SaveFileDialog saveFileDialog = new SaveFileDialog();

		// Путь к файлу
		private string pathFile;
		private string pathFileSave;

		public MainWindow()
		{
			InitializeComponent();
			myOpenFileDialog.FileOk += OnOpenFileDialogOK;
			saveFileDialog.FileOk += OnSaveFileDialogOK;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			myOpenFileDialog.ShowDialog();
		}

		private void OnOpenFileDialogOK(object sender, EventArgs e)
		{
			pathFile = myOpenFileDialog.FileName;
			DisplayFile();
		}

		private void DisplayFile()
		{
			using (StreamReader file = new StreamReader(pathFile, System.Text.Encoding.Default))
			{
				textBox.Text = file.ReadToEnd();
			}
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			saveFileDialog.ShowDialog();
		}

		private void OnSaveFileDialogOK(object sender, EventArgs e)
		{
			pathFileSave = saveFileDialog.FileName;
			SaveFile();
		}

		private void SaveFile()
		{
			FileStream writer = new FileStream(pathFileSave, FileMode.Create);
			using (StreamWriter file = new StreamWriter(writer, System.Text.Encoding.Default))
			{
				file.Write(textBox.Text);
			}
		}

		private void textBox_TextChanged(object sender, TextChangedEventArgs e){}
	}
}

