namespace BudgetManager.Api
{
    public class Customer
    {
        public int Id { get; set; }
        public string firebaseID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public bool isActive { get; set; }
    
    }
}
