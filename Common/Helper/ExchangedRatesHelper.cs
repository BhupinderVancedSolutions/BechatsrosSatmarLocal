using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class Rates
    {
        public double USD { get; set; }
    }
    public class ExchangedRatesHelper
    {
        public bool Success { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public string date { get; set; }
        public Rates Rates { get; set; }
        public double result { get; set; }
    }
}
