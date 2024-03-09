using System.ComponentModel.DataAnnotations;

namespace DSRLearn.Context.Entities
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
