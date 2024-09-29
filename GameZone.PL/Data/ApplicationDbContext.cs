namespace GameZone.PL.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite P.K => M-M
            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new { e.GameId, e.DeviceId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
