using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
