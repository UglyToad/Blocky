namespace Blocky.SandBox.Wpf
{
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }

        private void ReImage(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel) DataContext).CalculateImage();
        }
    }
}
