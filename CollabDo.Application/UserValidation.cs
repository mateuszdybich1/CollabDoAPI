
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using System.Threading.Tasks;

namespace CollabDo.Application
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
            if( await _userRepository.UsernameExists(username))
            {
                throw new ValidationException(String.Format("Username: \"{0}\" is taken", username));
            }
        }

        public async Task ValidateEmail(string email)
        {
            if (await _userRepository.EmailExists(email))
            {
                throw new ValidationException(String.Format("Email: \"{0}\" is taken", email));
            }
        }

    }
}
