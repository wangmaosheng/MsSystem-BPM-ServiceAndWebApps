using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Identity
{
    public class ProfileService: IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException("Subject is null.");
            var sub = context.Subject.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Subject) ?? throw new ArgumentNullException("Subject id is null.");
            if (sub == null)
            {
                throw new NullReferenceException("Sub is not allowed to be null.");
            }
            if (!int.TryParse(sub.Value, out int iSubjectId))
            {
                throw new NullReferenceException("Subject id is not allowed to be null.");
            }
            context.IssuedClaims = subject.Claims.ToList();
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException("Subject is null.");
            var sub = context.Subject.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Subject) ?? throw new ArgumentNullException("Subject id is null.");
            if (sub == null)
            {
                throw new NullReferenceException("Sub is not allowed to be null.");
            }
            context.IsActive = int.TryParse(sub.Value, out int iSubjectId);
            return Task.CompletedTask;
        }
    }
}
