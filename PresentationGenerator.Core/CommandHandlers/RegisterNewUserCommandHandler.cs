using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Repositories;

namespace PresentationGenerator.Core.CommandHandlers
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand>
    {
        private IUserRepository userRepository;

        public RegisterNewUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Handle(RegisterNewUserCommand command)
        {
            User newUser = new User(command.Username, command.Password);
            userRepository.Add(newUser);
        }
    }
}
