namespace DSRLearn.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public virtual UserProfile Profile { get; set; }
    public UserStatus Status { get; set; }
    public virtual ICollection<Debt> DebtsIssued { get; set; }
    public virtual ICollection<Debt> DebtsRecieved { get; set; }
}
