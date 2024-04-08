using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ConwayLife.Controls
{
    /// <summary>
    /// Logica di interazione per GameGrid.xaml
    /// </summary>
    public partial class GameGrid : UserControl
    {
        private readonly static (int x, int y)[] NEIGHBORS = new (int, int)[] {
            (-1, -1),  // Above left
            (-1, 0),  // Above
            (-1, 1),  // Above right
            (0, -1),  // Left
            (0, 1),  // Right
            (1, -1),  // Below left
            (1, 0),  // Below
            (1, 1),  // Below right
        };


        public GameGrid()
        {
            InitializeComponent();
        }

        private Dictionary<(int X, int Y), (Rectangle Ceil, int Count)> GetState()
        {
            return this.game_grid.Children.OfType<Rectangle>()
            .ToDictionary(
                x => (Grid.GetRow(x), Grid.GetColumn(x)),
                x => (x, 0)
            );
        }

        public Grid Grid => this.game_grid;

        public void Fill()
        {
            for (int i = 0; i < this.game_grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < this.game_grid.ColumnDefinitions.Count; j++)
                {
                    var rect = new Rectangle()
                    {
                        Tag = CeilState.Dead
                    };

                    this.game_grid.Children.Add(rect);
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                }
            }
        }

        private void ChangeLifeState(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Rectangle rect)
                throw new NotImplementedException();
            rect.Tag = rect.Tag is CeilState.Dead ? CeilState.Alive : CeilState.Dead;
        }


        public void Tick()
        {
            var src = this.GetState();

            foreach (var i in src)
            {
                if (i.Value.Ceil.Tag is CeilState.Dead)
                    continue;

                foreach (var j in GameGrid.NEIGHBORS)
                {
                    var k = (i.Key.X + j.x, i.Key.Y + j.y);
                    if (!src.TryGetValue(k, out var item))
                        continue;

                    src[k] = (item.Ceil, item.Count + 1);
                }
            }

            foreach ((Rectangle Ceil, int Count) in src.Values)
            {
                if (Count < 2 && Ceil.Tag is CeilState.Alive)
                    Ceil.Tag = CeilState.Dead;
                else if (Ceil.Tag is CeilState.Alive && (Count == 2 || Count == 3))
                    ;
                else if (Count > 3 && Ceil.Tag is CeilState.Alive)
                    Ceil.Tag = CeilState.Dead;
                else if (Ceil.Tag is CeilState.Dead && Count == 3)
                    Ceil.Tag = CeilState.Alive;
            }
        }

        public void Clear()
        {
            foreach (var i in this.game_grid.Children.OfType<Rectangle>())
                i.Tag = CeilState.Dead;
        }

    }
}
