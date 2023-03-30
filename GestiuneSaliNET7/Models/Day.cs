﻿namespace GestiuneSaliNET7.Models
{
    public class Day
    {
        public int Id { get; set; } = 0;
        public List<ReservationModel> Reservations { get; set; }

        public Day() 
        {
            Reservations = new List<ReservationModel>
            {
                new ReservationModel()
            };
        }
    }
}
