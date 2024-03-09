namespace DSRLearn.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : BaseEntity //IdentityUser<Guid>
{
    public string Login { get; set; }
    public virtual UserDetail Detail { get; set; }
    public UserStatus Status { get; set; }
    public virtual ICollection<Debt> DebtsIssued { get; set; }
    public virtual ICollection<Debt> DebtsRecieved { get; set; }
}
