using Application.Common.Interfaces;
using MediatR.Pipeline;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ICurrentUserService _currentUserService;
        public LoggingBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var username = _currentUserService.UserEmail;
            Debug.WriteLine($"Loggin behavior involed! Actual user is: {username}");
        }
    }
}
