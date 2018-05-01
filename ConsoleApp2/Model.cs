using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ConsoleApp1
{
    using System;

    /// <summary>
    /// The person model.
    /// </summary>
    [Table("Combinaisons")]
    public class Combinaison
    {
        public double UnLandTour1 { get; set; }
        public double DeuxLandTour2 { get; set; }
        public double TroisLandTour3 { get; set; }
        public double QuatreLandTour4 { get; set; }
        public double CinqLandTour5 { get; set; }
        public double UnRougeTour2 { get; set; }
        public double UnBleuTour3 { get; set; }
        public double DeuxBleuTour5 { get; set; }
        public double DeuxNoirTour6 { get; set; }
        public double TroisRougeTour6 { get; set; }
        public double UnLandUntapTour1 { get; set; }
        public double DeuxLandUntapTour2 { get; set; }
        public double TroisLandUntapTour3 { get; set; }
        public double QuatreLandUntapTour4 { get; set; }
        public double CinqLandUntapTour5 { get; set; }
        public double UnBleuTour1 { get; set; }
        public double UnBleuTour2 { get; set; }
        public double DeuxNoirTour4 { get; set; }
        public double DeuxNoirTour5 { get; set; }
        public double SansMulligan { get; set; }
        public double Mulligan6 { get; set; }
        public double Mulligan5 { get; set; }
        public double Mulligan4 { get; set; }
        [Key, Column(Order = 1)]
        public int aetherHub { get; set; }
        [Key, Column(Order = 2)]
        public int SommetCraneDragon { get; set; }
        [Key, Column(Order = 3)]
        public int CanyonCroupissant { get; set; }
        [Key, Column(Order = 4)]
        public int SpireBlufCanal { get; set; }
        [Key, Column(Order = 5)]
        public int ChuteSoufre { get; set; }
        [Key, Column(Order = 6)]
        public int CatacombesNoyees { get; set; }
        [Key, Column(Order = 7)]
        public int BassinFetides { get; set; }
        [Key, Column(Order = 8)]
        public int ile { get; set; }
        [Key, Column(Order = 9)]
        public int montagne { get; set; }
        [Key, Column(Order = 10)]
        public int marais { get; set; }
    }
}