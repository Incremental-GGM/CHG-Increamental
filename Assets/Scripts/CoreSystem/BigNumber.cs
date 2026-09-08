using UnityEngine;

public struct BigNumber
{
        /*public double Mantissa => mantissa;
        public long Exponent => exponent;
        
        [SerializeField] private double mantissa;
        [SerializeField] private long exponent;

        public BigNumber(double mantissa, long exponent)
        {
                this.mantissa = mantissa;
                this.exponent = exponent;
        }
        
        public override string ToString()
        {
                return $"{mantissa:0.###}e{exponent}";
        }

        /*public static BigNumber operator +(BigNumber a, BigNumber b)
        {
                double bigD, smallD;
                if (a.mantissa > b.mantissa) { bigD = a.mantissa; smallD = b.mantissa; }
                else { bigD = b.mantissa; smallD = a.mantissa; }
                
                //수가 작은 애 걸로 잘라 주는데, 1과 100이 있으면 1과 0.01로? 그러면 더할 때
                #1#
                        
        }*/
}