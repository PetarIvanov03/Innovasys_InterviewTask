namespace InnovasysApp.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public AddressViewModel Address { get; set; }

        public string? Note { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddressViewModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public GeoViewModel Geo { get; set; }
    }

    public class GeoViewModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

}
