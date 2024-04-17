using System.ComponentModel.DataAnnotations;

namespace DSRLearn.Context.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
