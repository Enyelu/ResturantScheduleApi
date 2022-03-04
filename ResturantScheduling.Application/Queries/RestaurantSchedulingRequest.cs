global using MediatR;
using ResturantScheduling.Application.Commands;

namespace ResturantScheduling.Application.Queries
{
    public class GetOpenningAndClosingHoursQuerry : IRequest<OpenCloseRequest>
    {
        public OpenCloseRequest openningAndClosingHoursReqests;
        public GetOpenningAndClosingHoursQuerry(OpenCloseRequest timeReqests)
        {
            openningAndClosingHoursReqests = timeReqests;
        }
    }
}