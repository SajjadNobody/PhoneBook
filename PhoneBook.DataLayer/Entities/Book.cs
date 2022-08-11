namespace PhoneBook.DataLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        public User UserBook { get; set; }
    }
}
