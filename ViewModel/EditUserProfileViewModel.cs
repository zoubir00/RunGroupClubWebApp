namespace RunGroupClubWebApp.ViewModel
{
    public class EditUserProfileViewModel
    {
        public string id { get; set; }
        public int Pace { get; set; }
        public int Mileage { get; set; }
        public string? ImageProfileUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
    }
}
