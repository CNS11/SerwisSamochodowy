using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        [NotMapped]
        public string FullName
        {
            get { return Imie + " " + Nazwisko; }
        }
        public string NIP { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwe miejscowosci")]
        [Display(Name = "Miejscowosć")]
        public string Miejscowosc { get; set; }
        [Required(ErrorMessage = "Proszę podać ulicę")]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; }
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Podany kod pocztowy jest w niewlasciwym formacie")]
        [Required(ErrorMessage = "Proszę podać Kod pocztowy")]
        [Display(Name = "Kod pocztowy", Prompt = "39-200")]
        public string Kod_pocztowy { get; set; }
        [Display(Name = "Numer Telefonu")]
        //[RegularExpression("^[0-9]{8})|[0-9]+{12}|[0-9]{11}$", ErrorMessage = "Niepoprawny numer telefonu")]
        public string Numer_telefonu { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Protokol_przyjecia> Protokol_przyjeciaDbSet { get; set; }
        public DbSet<Pozycja_protokolu_przyjecia> Pozycja_protokolu_przyjeciaDbSet { get; set; }
        public DbSet<Pracownicy> PracownicyDbSet { get; set; }
        public DbSet<Samochod> SamochodDbSet { get; set; }
        public DbSet<Potwierdzenie_odbioru> Potwierdzenie_odbioruDbSet { get; set; }
        public DbSet<Czesci> CzesciDbSet { get; set; }
        public DbSet<IdentityUserRole> RoleDbSet { get; set; }
        public DbSet<Modele_has_Czesci> Modele_has_CzesciDbSet { get; set; }
        public System.Data.Entity.DbSet<WebApplication1.Models.Magazyn> Magazyn { get; set; }
      //  public DbSet<Magazyn> MagazynDbSet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
          //  modelBuilder.Entity<UslugaNaprawy_Czesci>().HasKey(x => new { x.IdCzesci, x.Usluga_naprawy });



            //modelBuilder.Entity<pracownicyprojekty>().HasKey(x => new { x.IdPracownik, x.IdProjekt });
            //modelBuilder.Entity<pracownicyprojekty>().HasRequired(x => x.Pracownik).WithMany().HasForeignKey(x => x.IdPracownik).WillCascadeOnDelete(false);
            //modelBuilder.Entity<pracownicyprojekty>().HasRequired(x => x.Projekt).WithMany().HasForeignKey(x => x.IdProjekt);
  
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.Modele> Modeles { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Marki> Markis { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.ZuzyteCzesci> ZuzyteCzescis { get; set; }


        
    }
}