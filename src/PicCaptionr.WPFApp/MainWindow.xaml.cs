using MahApps.Metro.Controls;
using PicCaptionr.WPFApp.ViewModels;

namespace PicCaptionr.WPFApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
	public MainWindow(MainWindowViewModel mainWindowViewModel)
	{
		InitializeComponent();

		DataContext = mainWindowViewModel;
	}
}