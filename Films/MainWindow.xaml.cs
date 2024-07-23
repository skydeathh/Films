using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Films
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMaximized = false;
        private double normalWidth = 850;
        private double normalHeight = 650;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.OriginalSource is not ResizeGrip)
            {
                DragMove();
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ToggleSizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMaximized)
            {
                Width = normalWidth;
                Height = normalHeight;
                WindowState = WindowState.Normal;
                isMaximized = false;
            }
            else
            {
                normalWidth = Width;
                normalHeight = Height;
                Width = SystemParameters.PrimaryScreenWidth;
                Height = SystemParameters.PrimaryScreenHeight;
                WindowState = WindowState.Maximized;
                isMaximized = true;
            }
        }
    }
}