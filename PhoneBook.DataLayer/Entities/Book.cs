namespace PhoneBook.DataLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        // if we want to add relation we can add such thing 
        public User UserBook { get; set; }
    }
}
