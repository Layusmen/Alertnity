﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Alertnity.PoliceApi;

namespace Alertnity.PostcodeApi
{
    public class Result
    {
        public string Postcode { get; set; }
        public int Quality { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public string Country { get; set; }
        public string NhsHa { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string EuropeanElectoralRegion { get; set; }
        public string PrimaryCareTrust { get; set; }
        public string Region { get; set; }
        public string Lsoa { get; set; }
        public string Msoa { get; set; }
        public string Incode { get; set; }
        public string Outcode { get; set; }
        public string ParliamentaryConstituency { get; set; }
        public string ParliamentaryConstituency2024 { get; set; }
        public string AdminDistrict { get; set; }
        public string Parish { get; set; }
        public object AdminCounty { get; set; }
        public string DateOfIntroduction { get; set; }
        public string AdminWard { get; set; }
        public object Ced { get; set; }
        public string Ccg { get; set; }
        public string Nuts { get; set; }
        public string Pfa { get; set; }
        public Codes Codes { get; set; }
        public double Distance { get; set; }
    }
}
