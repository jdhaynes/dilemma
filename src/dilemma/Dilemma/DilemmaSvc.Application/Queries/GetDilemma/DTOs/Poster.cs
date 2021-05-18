using System;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs
{
    public class Poster
    {
        public Guid PosterId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => CalculateAge(DateOfBirth);
        
        // TODO: consumer shouldn't see DOB - make private but allow mapper to set value.

        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}