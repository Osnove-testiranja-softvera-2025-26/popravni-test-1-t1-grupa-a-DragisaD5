

namespace OTS2026_PT1_GrupaA.Models
{
    public class Map
    {
        public Field[,] Fields { get; set; }
        public static readonly int MapSize = 30;

        public Map()
        {
            Fields = new Field[MapSize, MapSize];
        }

        public void InitializeMap()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Fields[i, j] = new Field(Zone.Invalid);
                }
            }

            CreateRectangleZone(Zone.Pond, 20, 0, 10, 25);
            CreateRectangleZone(Zone.Land, 0, 5, 12, 25);
            CreateRectangleZone(Zone.Land, 12, 10, 8, 10);

        }


        private void CreateRectangleZone(Zone zone, int upperLeftCornerX, int upperLeftCornerY, int width, int height)
        {
            for (int i = upperLeftCornerX; i < upperLeftCornerX + width; i++)
            {
                for (int j = upperLeftCornerY; j < upperLeftCornerY + height; j++)
                {
                    Fields[i, j].Zone = zone;
                }
            }
        }

        public void AddContentToFieldOnPosition(FieldContent content, Position position)
        {
            Field field = Fields[position.X, position.Y];

            if (!field.Zone.Equals(Zone.Invalid))
            {
                if ((field.Zone.Equals(Zone.Land) && content.Equals(FieldContent.Fish)) || (field.Zone.Equals(Zone.Pond) && content.Equals(FieldContent.Bait)))
                    return;
                else
                    field.Content = content;
            }
        }

        public void EmptyTileOnPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            Fields[x, y].Content = FieldContent.Empty;

        }
    }
}
