namespace Fawn.DAL.EFCore.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class AppContextDesignFactory : IDesignTimeDbContextFactory<FawnAppContext>
    {
        public FawnAppContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FawnAppContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=FawnDatabase;User Id=postgres;Password=mysecretpassword;");

            return new FawnAppContext(builder.Options);
        }
    }
}