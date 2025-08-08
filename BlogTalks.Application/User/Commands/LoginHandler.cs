using BlogTalks.Domain.Reposotories;
using BlogTalks.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.User.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;


        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 
        public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var userByUsername = _userRepository.GetByUsername(request.Username);
            var userByEmail = _userRepository.GetByEmail(request.Email);

            if (userByUsername == null && userByEmail == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = $"User with username '{request.Username}' and email '{request.Email}' not found"
                });
            }

            if (userByUsername == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = $"User with username '{request.Username}' not found"
                });
            }

            if (userByEmail == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = $"User with email '{request.Email}' not found"
                });
            }

            var user = userByUsername ?? userByEmail;
            var passwordToCheck = user.Password;

            if (!PasswordHasher.VerifyPassword(request.Password, passwordToCheck))
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = "Invalid password"
                });
            }

            return Task.FromResult(new LoginResponse
            {
                Token = "" 
            });
        }

    }
}
