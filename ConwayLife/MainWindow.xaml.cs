using System.Windows;
using System.Windows.Threading;

namespace ConwayLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer TickDP { get; init; } = new()
        {
            Interval = TimeSpan.FromMilliseconds(250),
        };

        public MainWindow()
        {
            InitializeComponent();
            this.TickDP.Tick += (s, e) => this.game_grid.Tick();
            this.game_grid.Fill();
        }

        private void GridChangeOnOff(object sender, RoutedEventArgs e)
        {
            this.game_grid.Grid.ShowGridLines = !this.game_grid.Grid.ShowGridLines;
        }


        private void DoTick(object sender, RoutedEventArgs e) => this.game_grid.Tick();

        private void AutoTickOnOff(object sender, RoutedEventArgs e)
        {
            this.SingleTickButton.IsEnabled = this.TickDP.IsEnabled;
            this.TickDP.IsEnabled = !this.TickDP.IsEnabled;
        }

        private void ClearGrid(object sender, RoutedEventArgs e) => this.game_grid.Clear();
    }
}