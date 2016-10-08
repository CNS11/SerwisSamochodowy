namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrlsc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Czesci",
                c => new
                    {
                        IdCzesci = c.Int(nullable: false, identity: true),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ilosc = c.Int(nullable: false),
                        PotwierdzenieOdbioruRefId = c.Int(nullable: false),
                        MagazynRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCzesci)
                .ForeignKey("dbo.Magazyn", t => t.MagazynRefId)
                .ForeignKey("dbo.Potwierdzenie_odbioru", t => t.PotwierdzenieOdbioruRefId)
                .Index(t => t.PotwierdzenieOdbioruRefId)
                .Index(t => t.MagazynRefId);
            
            CreateTable(
                "dbo.Magazyn",
                c => new
                    {
                        IdCzesci = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Numer_Seryjny = c.String(),
                        Cena_Zakupu = c.Single(nullable: false),
                        test = c.Int(nullable: false),
                        SugCenaSprz = c.Single(nullable: false),
                        StanMagazynowy = c.Int(nullable: false),
                        ModeleRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCzesci)
                .ForeignKey("dbo.Modele", t => t.ModeleRefId)
                .Index(t => t.ModeleRefId);
            
            CreateTable(
                "dbo.Modele",
                c => new
                    {
                        IdModelu = c.Int(nullable: false, identity: true),
                        NazwaModelu = c.String(nullable: false),
                        MarkiRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdModelu)
                .ForeignKey("dbo.Marki", t => t.MarkiRefId)
                .Index(t => t.MarkiRefId);
            
            CreateTable(
                "dbo.Marki",
                c => new
                    {
                        IdMarki = c.Int(nullable: false, identity: true),
                        NazwaMarki = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdMarki);
            
            CreateTable(
                "dbo.Potwierdzenie_odbioru",
                c => new
                    {
                        IdPotwierdzenia_odbioru = c.Int(nullable: false, identity: true),
                        Wycena = c.Single(nullable: false),
                        Data_odbioru = c.DateTime(nullable: false),
                        KlientRefId = c.String(maxLength: 128),
                        SamochodRefId = c.Int(nullable: false),
                        ProtokolPrzyjeciaRefId = c.Int(nullable: false),
                        PracownikRefId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdPotwierdzenia_odbioru)
                .ForeignKey("dbo.AspNetUsers", t => t.KlientRefId)
                .ForeignKey("dbo.AspNetUsers", t => t.PracownikRefId)
                .ForeignKey("dbo.Protokol_przyjecia", t => t.ProtokolPrzyjeciaRefId)
                .ForeignKey("dbo.Samochod", t => t.SamochodRefId)
                .Index(t => t.KlientRefId)
                .Index(t => t.SamochodRefId)
                .Index(t => t.ProtokolPrzyjeciaRefId)
                .Index(t => t.PracownikRefId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Imie = c.String(nullable: false),
                        Nazwisko = c.String(nullable: false),
                        NIP = c.String(),
                        Miejscowosc = c.String(nullable: false),
                        Ulica = c.String(nullable: false),
                        Kod_pocztowy = c.String(nullable: false),
                        Numer_telefonu = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Protokol_przyjecia",
                c => new
                    {
                        IdProtokolu_przyjecia = c.Int(nullable: false, identity: true),
                        Data_przyjecia = c.DateTime(nullable: false),
                        Czy_gotowe = c.Boolean(nullable: false),
                        Czy_Odebrane = c.Boolean(nullable: false),
                        KlientRefId = c.String(maxLength: 128),
                        SamochodRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProtokolu_przyjecia)
                .ForeignKey("dbo.AspNetUsers", t => t.KlientRefId)
                .ForeignKey("dbo.Samochod", t => t.SamochodRefId)
                .Index(t => t.KlientRefId)
                .Index(t => t.SamochodRefId);
            
            CreateTable(
                "dbo.Pozycja_protokolu_przyjecia",
                c => new
                    {
                        IdPozycji_protokolu_przyjecia = c.Int(nullable: false, identity: true),
                        Opis_usterki = c.String(),
                        Uwagi = c.String(),
                        Protokol_przyjeciaRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPozycji_protokolu_przyjecia)
                .ForeignKey("dbo.Protokol_przyjecia", t => t.Protokol_przyjeciaRefId)
                .Index(t => t.Protokol_przyjeciaRefId);
            
            CreateTable(
                "dbo.Samochod",
                c => new
                    {
                        IdSamochodu = c.Int(nullable: false, identity: true),
                        Marka = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Rocznik = c.String(nullable: false),
                        Numer_rejestracyjny = c.String(nullable: false, maxLength: 450),
                        Numer_VIN = c.String(nullable: false, maxLength: 450),
                        ModelRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSamochodu)
                .ForeignKey("dbo.Modele", t => t.ModelRefId)
                .Index(t => t.Numer_rejestracyjny, unique: true)
                .Index(t => t.Numer_VIN, unique: true)
                .Index(t => t.ModelRefId);
            
            CreateTable(
                "dbo.Modele_has_Czesci",
                c => new
                    {
                        ModeleRefId = c.Int(nullable: false),
                        MagazynRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModeleRefId, t.MagazynRefId })
                .ForeignKey("dbo.Magazyn", t => t.MagazynRefId)
                .ForeignKey("dbo.Modele", t => t.ModeleRefId)
                .Index(t => t.ModeleRefId)
                .Index(t => t.MagazynRefId);
            
            CreateTable(
                "dbo.Pracownicy",
                c => new
                    {
                        IdPracownika = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Stanowisko = c.String(),
                        Data_zatrudnienia = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdPracownika);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ZuzyteCzesci",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCzesci = c.Int(nullable: false),
                        ilosc = c.Int(nullable: false),
                        PotwierdzenieOdbioruRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Potwierdzenie_odbioru", t => t.PotwierdzenieOdbioruRefId)
                .Index(t => t.PotwierdzenieOdbioruRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZuzyteCzesci", "PotwierdzenieOdbioruRefId", "dbo.Potwierdzenie_odbioru");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Modele_has_Czesci", "ModeleRefId", "dbo.Modele");
            DropForeignKey("dbo.Modele_has_Czesci", "MagazynRefId", "dbo.Magazyn");
            DropForeignKey("dbo.Protokol_przyjecia", "SamochodRefId", "dbo.Samochod");
            DropForeignKey("dbo.Potwierdzenie_odbioru", "SamochodRefId", "dbo.Samochod");
            DropForeignKey("dbo.Samochod", "ModelRefId", "dbo.Modele");
            DropForeignKey("dbo.Pozycja_protokolu_przyjecia", "Protokol_przyjeciaRefId", "dbo.Protokol_przyjecia");
            DropForeignKey("dbo.Potwierdzenie_odbioru", "ProtokolPrzyjeciaRefId", "dbo.Protokol_przyjecia");
            DropForeignKey("dbo.Protokol_przyjecia", "KlientRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Potwierdzenie_odbioru", "PracownikRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Potwierdzenie_odbioru", "KlientRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Czesci", "PotwierdzenieOdbioruRefId", "dbo.Potwierdzenie_odbioru");
            DropForeignKey("dbo.Czesci", "MagazynRefId", "dbo.Magazyn");
            DropForeignKey("dbo.Modele", "MarkiRefId", "dbo.Marki");
            DropForeignKey("dbo.Magazyn", "ModeleRefId", "dbo.Modele");
            DropIndex("dbo.ZuzyteCzesci", new[] { "PotwierdzenieOdbioruRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Modele_has_Czesci", new[] { "MagazynRefId" });
            DropIndex("dbo.Modele_has_Czesci", new[] { "ModeleRefId" });
            DropIndex("dbo.Samochod", new[] { "ModelRefId" });
            DropIndex("dbo.Samochod", new[] { "Numer_VIN" });
            DropIndex("dbo.Samochod", new[] { "Numer_rejestracyjny" });
            DropIndex("dbo.Pozycja_protokolu_przyjecia", new[] { "Protokol_przyjeciaRefId" });
            DropIndex("dbo.Protokol_przyjecia", new[] { "SamochodRefId" });
            DropIndex("dbo.Protokol_przyjecia", new[] { "KlientRefId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Potwierdzenie_odbioru", new[] { "PracownikRefId" });
            DropIndex("dbo.Potwierdzenie_odbioru", new[] { "ProtokolPrzyjeciaRefId" });
            DropIndex("dbo.Potwierdzenie_odbioru", new[] { "SamochodRefId" });
            DropIndex("dbo.Potwierdzenie_odbioru", new[] { "KlientRefId" });
            DropIndex("dbo.Modele", new[] { "MarkiRefId" });
            DropIndex("dbo.Magazyn", new[] { "ModeleRefId" });
            DropIndex("dbo.Czesci", new[] { "MagazynRefId" });
            DropIndex("dbo.Czesci", new[] { "PotwierdzenieOdbioruRefId" });
            DropTable("dbo.ZuzyteCzesci");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pracownicy");
            DropTable("dbo.Modele_has_Czesci");
            DropTable("dbo.Samochod");
            DropTable("dbo.Pozycja_protokolu_przyjecia");
            DropTable("dbo.Protokol_przyjecia");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Potwierdzenie_odbioru");
            DropTable("dbo.Marki");
            DropTable("dbo.Modele");
            DropTable("dbo.Magazyn");
            DropTable("dbo.Czesci");
        }
    }
}
