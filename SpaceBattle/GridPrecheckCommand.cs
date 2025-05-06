using App;

namespace SpaceBattle
{
    public class GridPrecheckCommand(IMoving movingObject)
    {
        private readonly IMoving _movingObject = movingObject;

        public void Execute()
        {
            var grids = Ioc.Resolve<IEnumerable<Grid>>("Game.Grids");
            var objects = Ioc.Resolve<IDictionary<string, object>>("Game.Objects")
                .Values
                .Select(obj => Ioc.Resolve<IMoving>("Adapters.IMovingObject", obj));

            var objIsInOneCell = grids.SelectMany(grid =>
                objects.Where(obj => grid.IsInOneCell(_movingObject.Position, obj.Position))
                    .Select(obj => new { First = _movingObject, Second = obj }));

            objIsInOneCell.ToList().ForEach(e =>
                Ioc.Resolve<App.ICommand>("Commands.CheckCollision", e.First, e.Second).Execute());
        }
    }
}
