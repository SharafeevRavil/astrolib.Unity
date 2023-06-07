namespace Dataset
{
    public class StarDataCompilation
    {
        public Bsc5StarDto Bsc5Star { get; }
        public double Distance { get; }

        public StarDataCompilation(Bsc5StarDto bsc5Star, double distance)
        {
            Bsc5Star = bsc5Star;
            Distance = distance;
        }
    }
}