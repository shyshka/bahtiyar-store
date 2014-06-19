using System.ComponentModel;
using System.Windows;

namespace Bahtiar.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            BahtiarViewModel.LoadCategories();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            BahtiarViewModel.SaveLocal();
        }
    }
}
