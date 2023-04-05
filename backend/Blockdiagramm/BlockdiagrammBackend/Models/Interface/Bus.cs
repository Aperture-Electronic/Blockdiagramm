namespace BlockdiagrammBackend.Models.Interface
{
    [Serializable]
    public class Bus
    {
        public string Name { get; }

        public BusDirection Direction { get; }

        public Bus(string name, BusDirection direction = BusDirection.Master)
        {
            Name = name;
            Direction = direction;
        }
    }
}
