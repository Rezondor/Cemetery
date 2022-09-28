using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Cemetery;
public partial class MainWindow : Window
{
    Size areaSize;
    Size tileSize;
    Size pixelSize;
    List<List<int>> area;

    List<Monument> monuments = new List<Monument>()
    {
        new Monument(new Point(90,35),new Size(70,150)),
        new Monument(new Point(240,35),new Size(70,150)),
    };

    public MainWindow()
    {
        InitializeComponent();
        tileSize = new Size(40, 40);
        areaSize = new Size(350, 250);
        pixelSize = new Size(2, 2);
        MainCanvas.Height = areaSize.Height * pixelSize.Height;
        MainCanvas.Width = areaSize.Width * pixelSize.Width;
        CreateAreaFunc();
        DrawArea(area ,MainCanvas);
    }
    



    public void CreateAreaFunc()
    {
        area = CreateArea.FillAreaWithMonument(CreateArea.FillArea(areaSize, tileSize), monuments);
    }

    public void DrawArea(List<List<int>> area, Canvas canvas)
    {
        for (int i = 0; i < area.Count; i++)
        {
            for (int j = 0; j < area[i].Count; j++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = pixelSize.Width,
                    Height = pixelSize.Height
                };

                SolidColorBrush a = new SolidColorBrush(
                    area[i][j] switch
                    {
                        0 => Colors.White,
                        1 => Colors.Black,
                        2 => Colors.Red,
                        _ => Colors.White,
                    }
                );

                rectangle.Fill = a;
                canvas.Children.Add(rectangle);

                SetPosition(new Point(j*pixelSize.Width ,i* pixelSize.Height), rectangle);

            }
        }
    }

    private void SetPosition(Point point, UIElement element)
    {
        Canvas.SetLeft(element, point.X);
        Canvas.SetTop(element, point.Y);
    }

}
