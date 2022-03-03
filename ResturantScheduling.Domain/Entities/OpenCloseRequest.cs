namespace ResturantScheduling.Domain.Entities
{
    public class OpenCloseRequest
    {
        public List<OpenClose>? Monday { get; set; }
        public List<OpenClose>? Tuesday { get; set; }
        public List<OpenClose>? Wednesday { get; set; }
        public List<OpenClose>? Thursday { get; set; }
        public List<OpenClose>? Friday { get; set; }
        public List<OpenClose>? Saturday { get; set; }
        public List<OpenClose>? Sunday { get; set; }
    }
}