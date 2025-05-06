namespace SpaceBattle
{
    public class Grid(int xOffset, int yOffset, int cellWidth)
    {
        private int XOffset { get; } = xOffset;
        private int YOffset { get; } = yOffset;
        private int CellWidth { get; } = cellWidth;

        public bool IsInOneCell(Vector obj1, Vector obj2)
        {
            var cellX1 = (int)Math.Ceiling((obj1.Coordinates[0] - XOffset) / (double)CellWidth);
            var cellY1 = (int)Math.Ceiling((obj1.Coordinates[1] - YOffset) / (double)CellWidth);
            var cellX2 = (int)Math.Ceiling((obj2.Coordinates[0] - XOffset) / (double)CellWidth);
            var cellY2 = (int)Math.Ceiling((obj2.Coordinates[1] - YOffset) / (double)CellWidth);

            return cellX1 == cellX2 && cellY1 == cellY2;
        }
    }
}
