namespace Dataset
{
    public class Bsc5StarDto
    {
        public int HrNumber { get; }
        public string AltName { get; }
        public double Ra { get; }
        public double Dec { get; }
        public double VMag { get; }
        public double Bv { get; }
        public string SpecType { get; }

        public Bsc5StarDto(int hrNumber, string altName, double ra, double dec, double vMag, double bv, string specType)
        {
            HrNumber = hrNumber;
            AltName = altName;
            Ra = ra;
            Dec = dec;
            VMag = vMag;
            Bv = bv;
            SpecType = specType;
        }
    }
}