namespace DBOpdracht;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext{
    // (= null!;) Tell the compiler/intelisense to shut up about nullables when not initializing a value 
    public DbSet<Attractie> Attractions {get;set;} = null!;
    public DbSet<Onderhoud> Maintenance {get;set;} = null!;
    public DbSet<Medewerker> Staff {get;set;} = null!;
    public DbSet<Gebruiker> Users {get;set;} = null!;
    public DbSet<Gast> Guests {get;set;} = null!;
    public DbSet<Reservering> Reservations {get;set;} = null!;
    public DbSet<GastInfo> GuestInfo {get;set;} = null!;

    public async Task<bool> Boek(Gast g, Attractie a, DateTimeBereik d){
        var result = Task<bool>.Run(()=> {
            if(a.reservering == null){
                var reservering = new Reservering{gast = g, VindtPlaatsTijdens = d};
                reservering.ReservedAttractions.Add(a);
                g.reservering.Add(reservering);
                return true;
            }
            return false;
        });
        return await result;
        
    }

    protected override void OnModelCreating(ModelBuilder builder){
        //Attractie
            var AttractionConfig = builder.Entity<Attractie>();
            AttractionConfig.ToTable("Attracties");
            AttractionConfig.OwnsMany(ondr => ondr.OnderhoudPunten);
        //Onderhoud
            var MaintenanceConfig = builder.Entity<Onderhoud>();
            MaintenanceConfig.ToTable("Onderhoud_taken");
            MaintenanceConfig.OwnsOne(attr => attr.Target);
        //Medewerker
            var StaffConfig = builder.Entity<Medewerker>();
            StaffConfig.ToTable("Medewerkers");
        //Gebruiker
            var UserConfig = builder.Entity<Gebruiker>();
            UserConfig.ToTable("Gebruikers");
            //UserConfig.HasData(new Gebruiker("GaryV2"));
            UserConfig.HasKey(k => k.Email);
        //Gast
            var GuestConfig = builder.Entity<Gast>();
            GuestConfig.ToTable("Gasten");

        //reservering 
            var ReservationsConfig = builder.Entity<Reservering>();
            ReservationsConfig.OwnsOne(res => res.VindtPlaatsTijdens);

        //Gastinfo
            var GuestInfoconfig = builder.Entity<GastInfo>();
            GuestInfoconfig.OwnsOne(ginfo => ginfo.coordinate);
            GuestInfoconfig.HasKey(k => k.Id);
            GuestInfoconfig.HasOne(g => g.Gast).WithOne(gi => gi.GastInformatie).HasForeignKey<Gast>(g => g.GastinfoId); //One to one with Gast/gastinfo?

        
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder){
        //builder.UseSqlServer("Server=AORUS-15P-W\\SQLEXPRESS;Initial Catalog=Week4DB;Integrated Security=true");
        builder.UseSqlServer("Server=DESKTOP-PRAETOR\\SQLEXPRESS;Initial Catalog=Week4DB;Integrated Security=true");
    }
}