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
            var userByEmail = _userRepository.GetByEmail(request.Email);
            var userByUsername = _userRepository.GetByUsername(request.Username);

            if (userByEmail == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = $"Email {request.Email} not found"
                });
            }
            if (userByUsername == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Token = $"Username {request.Username} not found"
                });
            }

            if (!PasswordHasher.VerifyPassword(request.Password, userByUsername.Password))
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
