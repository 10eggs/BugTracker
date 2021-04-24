using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Qa.Queries.GetAvailableQas
{
    public class GetAvailableQasRequest:IRequest<AvailableQasVm>
    {

    }

    public class GetAvailableQasRequestHandler : IRequestHandler<GetAvailableQasRequest, AvailableQasVm>
    {
        public Task<AvailableQasVm> Handle(GetAvailableQasRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
