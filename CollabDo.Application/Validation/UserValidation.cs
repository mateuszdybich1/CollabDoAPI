using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;

namespace CollabDo.Application.Validation
{
    public class UserValidation
    {
        private readonly IUserRepository _userRepository;

        public UserValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task ValidateUsername(string username)
        {
            if (await _userRepository.UsernameExists(username))
            {
                throw new ValidationException($"Username: \"{username}\" is taken");
            }
        }

        public async Task ValidateEmail(string email)
        {
            if(!email.EndsWith("uekat.pl"))
            {
                throw new ValidationException($"Email: \"{email}\" has wrong domain");
            }
            if (await _userRepository.EmailExists(email))
            {
                throw new ValidationException($"Email: \"{email}\" is taken");
            }
        }

    }
}
