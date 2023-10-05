namespace FictionalCompany.Models
{
    public class AddUserRequest
    {
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Phonenumber { get; set; }
        public string SkillSets { get; set; }
        public string? Hobby { get; set; }
    }
}
