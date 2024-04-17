namespace DSRLearn.Services.UserAccount;

using AutoMapper;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>()
            .BeforeMap<UserAccountModelActions>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
    public class UserAccountModelActions : IMappingAction<User, UserAccountModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UserAccountModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(User source, UserAccountModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var user = db.Users.Include(x => x.Profile).FirstOrDefault(x => x.Id == source.Id);
        }
    }
}