using Microsoft.EntityFrameworkCore;
using SchoolRoomTemperature.Entities;

namespace SchoolRoomTemperature.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Classroom> Classrooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>().ToTable("Classrooms");
        base.OnModelCreating(modelBuilder);
    }
}
