namespace ResturantScheduling.ExtensionMethods
{
    public static class MediatRExtension
    {
        public static void AddMediatRR(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}