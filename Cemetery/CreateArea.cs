namespace Cemetery;

public class CreateArea
{
    public Size AreaSize { get; set; }
    static public List<List<int>> FillArea(Size areaSize, Size tileSize)
    {
        List<List<int>> area = new();
        List<int> startEndLine = new();
        List<int> CenterLine = new();

        int countPixelWidth = 0;
        int maxCountPixelWidth = (int)tileSize.Width;

        for (int i = 0; i < areaSize.Width; i++)
        {
            countPixelWidth++;

            startEndLine.Add(1);

            if ((i == 0) ||
                (i == areaSize.Width - 1) ||
                (countPixelWidth == 1) ||
                (countPixelWidth == maxCountPixelWidth))
            {

                if ((countPixelWidth == maxCountPixelWidth))
                {
                    countPixelWidth = 0;
                }
                CenterLine.Add(1);
                continue;

            }
            CenterLine.Add(0);
        }

        int countPixelHeight = 0;
        int maxCountPixelHeight = (int)tileSize.Height;

        for (int i = 0; i < areaSize.Height; i++)
        {
            countPixelHeight++;
            if ((i == 0) ||
                (i == areaSize.Height - 1) ||
                (countPixelHeight == 1) ||
                (countPixelHeight == maxCountPixelHeight))
            {
                if ((countPixelHeight == maxCountPixelHeight))
                {
                    countPixelHeight = 0;
                }
                area.Add(startEndLine.GetRange(0, startEndLine.Count));
                continue;
            }
            area.Add(CenterLine.GetRange(0, CenterLine.Count));
        }


        return area;
    }

    static public List<List<int>> FillAreaWithMonument(List<List<int>> fillArea, List<Monument> monuments)
    {
        List<List<int>> fillAreaLocal = fillArea;
        //foreach (var monument in monuments)
        Parallel.ForEach<Monument>(monuments, (monument) =>
        {
            Point monumentStartPoint = monument.StartPoint;
            Size monumentSize = monument.Size;

            for (int i = (int)(monumentStartPoint.Y - 1); i < monumentStartPoint.Y + monumentSize.Height - 1; i++)
            {
                for (int j = (int)(monumentStartPoint.X - 1); j < monumentStartPoint.X + monumentSize.Width - 1; j++)
                {
                    fillAreaLocal[i][j] = 2;
                }
            }

            for (int i = (int)(monumentStartPoint.Y - 2); i < monumentStartPoint.Y + monumentSize.Height; i++)
            {
                for (int j = (int)(monumentStartPoint.X - 2); j < monumentStartPoint.X + monumentSize.Width; j++)
                {
                    if (fillAreaLocal[i][j] != 2)
                    {
                        fillAreaLocal[i][j] = 1;
                    }
                }
            }
        });

        return fillAreaLocal;
    }


    void PrintArea(List<List<int>> area)
    {
        for (int i = 0; i < area.Count; i++)
        {
            for (int j = 0; j < area[i].Count; j++)
            {
                Console.Write($"{area[i][j]} ");
            }
            Console.WriteLine();
        }
    }
}
