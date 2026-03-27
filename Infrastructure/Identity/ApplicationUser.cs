using Infrastructure.Persistence.Entities.Members;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public MemberEntity? Member { get; set; }
}
