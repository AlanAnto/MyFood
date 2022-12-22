namespace MyFood.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string LocationName { get; set; }
    }
}
