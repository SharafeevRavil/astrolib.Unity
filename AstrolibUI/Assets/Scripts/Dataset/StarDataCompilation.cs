namespace Dataset
{
    public class StarDataCompilation
    {
        public Bsc5StarDto Bsc5Star { get; }
        public double Distance { get; }
        
        public string Name { get; set; }

        public StarDataCompilation(Bsc5StarDto star, double distance, string name)
        {
            Bsc5Star = star;
            Distance = distance;
            Name = name;
        }
    }
}