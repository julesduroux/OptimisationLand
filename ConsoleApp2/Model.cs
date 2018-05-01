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
        public decimal? score { get; set; }
        public decimal? score_primaire { get; set; }
        public decimal? score_secondaire { get; set; }
        public decimal UnLandTour1 { get; set; }
        public decimal DeuxLandTour2 { get; set; }
        public decimal TroisLandTour3 { get; set; }
        public decimal QuatreLandTour4 { get; set; }
        public decimal CinqLandTour5 { get; set; }
        public decimal UnRougeTour2 { get; set; }
        public decimal UnBleuTour3 { get; set; }
        public decimal DeuxBleuTour5 { get; set; }
        public decimal UnRetardBleuTour2 { get; set; }
        public decimal unRetardRougeTour3 { get; set; }
        public decimal UnRetardBleuTour3 { get; set; }
        public decimal UnRetardBleuTour4 { get; set; }
        public decimal unRetardRougeTour4 { get; set; }
        public decimal UnBleuTour4 { get; set; }
        public decimal unRougeTour3 { get; set; }
        public decimal TroisRougeTour4 { get; set; }
        public decimal TroisRougeTour5 { get; set; }
        public decimal UnRetardBleuTour5 { get; set; }
        public decimal DeuxRetardBleusTour6 { get; set; }
        public decimal UnLandUntapTour1 { get; set; }
        public decimal DeuxLandUntapTour2 { get; set; }
        public decimal TroisLandUntapTour3 { get; set; }
        public decimal QuatreLandUntapTour4 { get; set; }
        public decimal CinqLandUntapTour5 { get; set; }
        public decimal UnBleuTour1 { get; set; }
        public decimal UnBleuTour2 { get; set; }
        public decimal DeuxNoirTour4 { get; set; }
        public decimal DeuxNoirTour5 { get; set; }
        public decimal SansMulligan { get; set; }
        public decimal Mulligan6 { get; set; }
        public decimal Mulligan5 { get; set; }
        public decimal Mulligan4 { get; set; }
        [Key, Column(Order = 1)]
        public short aetherHub { get; set; }
        [Key, Column(Order = 2)]
        public short SommetCraneDragon { get; set; }
        [Key, Column(Order = 3)]
        public short CanyonCroupissant { get; set; }
        [Key, Column(Order = 4)]
        public short SpireBlufCanal { get; set; }
        [Key, Column(Order = 5)]
        public short ChuteSoufre { get; set; }
        [Key, Column(Order = 6)]
        public short CatacombesNoyees { get; set; }
        [Key, Column(Order = 7)]
        public short BassinFetides { get; set; }
        [Key, Column(Order = 8)]
        public short ile { get; set; }
        [Key, Column(Order = 9)]
        public short montagne { get; set; }
        [Key, Column(Order = 10)]
        public short marais { get; set; }
    }

    [Table("Championnat")]
    public class Championnat
    {
        public decimal? score { get; set; }
        public decimal? score_primaire { get; set; }
        public decimal? score_secondaire { get; set; }
        public decimal UnLandTour1 { get; set; }
        public decimal DeuxLandTour2 { get; set; }
        public decimal TroisLandTour3 { get; set; }
        public decimal QuatreLandTour4 { get; set; }
        public decimal CinqLandTour5 { get; set; }
        public decimal UnRougeTour2 { get; set; }
        public decimal UnBleuTour3 { get; set; }
        public decimal DeuxBleuTour5 { get; set; }
        public decimal UnRetardBleuTour2 { get; set; }
        public decimal unRetardRougeTour3 { get; set; }
        public decimal UnRetardBleuTour3 { get; set; }
        public decimal UnRetardBleuTour4 { get; set; }
        public decimal unRetardRougeTour4 { get; set; }
        public decimal UnBleuTour4 { get; set; }
        public decimal unRougeTour3 { get; set; }
        public decimal TroisRougeTour4 { get; set; }
        public decimal TroisRougeTour5 { get; set; }
        public decimal UnRetardBleuTour5 { get; set; }
        public decimal DeuxRetardBleusTour6 { get; set; }
        public decimal UnLandUntapTour1 { get; set; }
        public decimal DeuxLandUntapTour2 { get; set; }
        public decimal TroisLandUntapTour3 { get; set; }
        public decimal QuatreLandUntapTour4 { get; set; }
        public decimal CinqLandUntapTour5 { get; set; }
        public decimal UnBleuTour1 { get; set; }
        public decimal UnBleuTour2 { get; set; }
        public decimal DeuxNoirTour4 { get; set; }
        public decimal DeuxNoirTour5 { get; set; }
        public decimal SansMulligan { get; set; }
        public decimal Mulligan6 { get; set; }
        public decimal Mulligan5 { get; set; }
        public decimal Mulligan4 { get; set; }
        [Key, Column(Order = 1)]
        public short aetherHub { get; set; }
        [Key, Column(Order = 2)]
        public short SommetCraneDragon { get; set; }
        [Key, Column(Order = 3)]
        public short CanyonCroupissant { get; set; }
        [Key, Column(Order = 4)]
        public short SpireBlufCanal { get; set; }
        [Key, Column(Order = 5)]
        public short ChuteSoufre { get; set; }
        [Key, Column(Order = 6)]
        public short CatacombesNoyees { get; set; }
        [Key, Column(Order = 7)]
        public short BassinFetides { get; set; }
        [Key, Column(Order = 8)]
        public short ile { get; set; }
        [Key, Column(Order = 9)]
        public short montagne { get; set; }
        [Key, Column(Order = 10)]
        public short marais { get; set; }
    }
}