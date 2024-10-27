using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZadanieASP1.Models;

namespace ZadanieASP1.Validators
{

    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : ApplicationUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
          
            if (user.DisablePasswordRestrictions)
            {
                return Task.FromResult(IdentityResult.Success);
            }

            var errors = new List<IdentityError>();

            if (password.Length < 8)
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordTooShort",
                    Description = "Hasło musi mieć co najmniej 8 znaków."
                });
            }

            if (!password.Any(char.IsUpper))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordRequiresUppercase",
                    Description = "Hasło musi zawierać co najmniej jedną wielką literę."
                });
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""\:{}|<>]"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordRequiresSpecialCharacter",
                    Description = "Hasło musi zawierać co najmniej jeden znak specjalny."
                });
            }

            return errors.Count == 0 ? Task.FromResult(IdentityResult.Success) : Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}