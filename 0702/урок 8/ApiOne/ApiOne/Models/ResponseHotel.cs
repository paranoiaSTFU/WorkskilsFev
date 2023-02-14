using ApiOne.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiOne.Models
{
    public class ResponseHotel
    {
        public ResponseHotel(Hotel hotel)
        {
            Id = hotel.ID;
            Name = hotel.Name;
            CountOfStars = hotel.CountOfStars;
            CountryName = hotel.Country.Name;
            HotelImage = hotel.Hotelimages.ToList().FirstOrDefault()?.ImageSource;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountOfStars { get; set; }
        public string CountryName { get; set; }
        public byte[] HotelImage { get; set; }
    }
}