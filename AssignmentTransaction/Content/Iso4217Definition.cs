using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentTransaction.Content
{
    public sealed class Iso4217Definition
    {
        private readonly string _code;
        private readonly int _number;
        private readonly int _exponent;
        public bool Found { get; set; }

        public string Code
        {
            get { return _code; }
        }

        public int Number
        {
            get { return _number; }
        }

        public int Exponent
        {
            get { return _exponent; }
        }

        private Iso4217Definition() { }

        public Iso4217Definition(string code, int number, int exponent)
        {
            _code = code;
            _number = number;
            _exponent = exponent;
            Found = true;
        }

        public static Iso4217Definition NotFound()
        {
            return new Iso4217Definition { Found = false };
        }
    }
}