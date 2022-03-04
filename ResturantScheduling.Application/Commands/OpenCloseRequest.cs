using ResturantScheduling.Application.Responses;
using ResturantScheduling.Domain.Entities;

namespace ResturantScheduling.Application.Commands
{
    public class OpenCloseRequest : IRequest<OpenCloseResponse>
    {
        public List<OpenClose> Monday { get; set; }
        public List<OpenClose> Tuesday { get; set; }
        public List<OpenClose> Wednesday { get; set; }
        public List<OpenClose> Thursday { get; set; }
        public List<OpenClose> Friday { get; set; }
        public List<OpenClose> Saturday { get; set; }
        public List<OpenClose> Sunday { get; set; }
    }
}
