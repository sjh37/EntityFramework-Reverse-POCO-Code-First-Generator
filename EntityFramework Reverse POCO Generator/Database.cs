

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "postcode"
//     Connection String:      "Data Source=localhost;Initial Catalog=Postcode;Integrated Security=True"

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace EntityFramework_Reverse_POCO_Generator
{
    // ************************************************************************
    // Database context
    public class fred : DbContext
    {
        public IDbSet<DatabasePatchHistory> DatabasePatchHistory { get; set; } // database_patch_history
        public IDbSet<GisBaseData> GisBaseData { get; set; } // GISBaseData
        public IDbSet<GisBaseDataMidpoints> GisBaseDataMidpoints { get; set; } // GISBaseData_Midpoints
        public IDbSet<ImportTmpGisBaseData> ImportTmpGisBaseData { get; set; } // ImportTmp_GISBaseData
        public IDbSet<Locality> Locality { get; set; } // Locality
        public IDbSet<LocalityOrig> LocalityOrig { get; set; } // Locality_orig
        public IDbSet<Master> Master { get; set; } // Master
        public IDbSet<MasterOrig> MasterOrig { get; set; } // Master_orig
        public IDbSet<PostcodeLocation> PostcodeLocation { get; set; } // PostcodeLocation
        public IDbSet<PostcodeLocationImport> PostcodeLocationImport { get; set; } // PostcodeLocation_import
        public IDbSet<Sysdiagrams> Sysdiagrams { get; set; } // sysdiagrams
        public IDbSet<TblLocality> TblLocality { get; set; } // tblLocality
        public IDbSet<TblMaster> TblMaster { get; set; } // tblMaster
        public IDbSet<TblThoroughfare> TblThoroughfare { get; set; } // tblThoroughfare
        public IDbSet<TblTown> TblTown { get; set; } // tblTown
        public IDbSet<Thoroughfare> Thoroughfare { get; set; } // Thoroughfare
        public IDbSet<ThoroughfareOrig> ThoroughfareOrig { get; set; } // Thoroughfare_orig
        public IDbSet<Town> Town { get; set; } // Town
        public IDbSet<TownOrig> TownOrig { get; set; } // Town_orig
        public IDbSet<VwPostcode> VwPostcode { get; set; } // vw_Postcode
        public IDbSet<Zone> Zone { get; set; } // Zone

        static fred()
        {
            Database.SetInitializer<fred>(null);
        }

        public fred()
        {
        }

        public fred(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new DatabasePatchHistoryConfiguration());
            modelBuilder.Configurations.Add(new GisBaseDataConfiguration());
            modelBuilder.Configurations.Add(new GisBaseDataMidpointsConfiguration());
            modelBuilder.Configurations.Add(new ImportTmpGisBaseDataConfiguration());
            modelBuilder.Configurations.Add(new LocalityConfiguration());
            modelBuilder.Configurations.Add(new LocalityOrigConfiguration());
            modelBuilder.Configurations.Add(new MasterConfiguration());
            modelBuilder.Configurations.Add(new MasterOrigConfiguration());
            modelBuilder.Configurations.Add(new PostcodeLocationConfiguration());
            modelBuilder.Configurations.Add(new PostcodeLocationImportConfiguration());
            modelBuilder.Configurations.Add(new SysdiagramsConfiguration());
            modelBuilder.Configurations.Add(new TblLocalityConfiguration());
            modelBuilder.Configurations.Add(new TblMasterConfiguration());
            modelBuilder.Configurations.Add(new TblThoroughfareConfiguration());
            modelBuilder.Configurations.Add(new TblTownConfiguration());
            modelBuilder.Configurations.Add(new ThoroughfareConfiguration());
            modelBuilder.Configurations.Add(new ThoroughfareOrigConfiguration());
            modelBuilder.Configurations.Add(new TownConfiguration());
            modelBuilder.Configurations.Add(new TownOrigConfiguration());
            modelBuilder.Configurations.Add(new VwPostcodeConfiguration());
            modelBuilder.Configurations.Add(new ZoneConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

    // database_patch_history
    public class DatabasePatchHistory
    {
        public virtual int DatabasePatchId { get; set; } // database_patch_id
        public virtual DateTime Applied { get; set; } // applied
        public virtual string CreatedBy { get; set; } // created_by
        public virtual string Description { get; set; } // description
    }

    // GISBaseData
    public class GisBaseData
    {
        public virtual string Postcode { get; set; } // Postcode
        public virtual int RecordNumber { get; set; } // RecordNumber
        public virtual string Outcode { get; set; } // Outcode
        public virtual string Incode { get; set; } // Incode
        public virtual string Type { get; set; } // Type
        public virtual int? Easting { get; set; } // Easting
        public virtual int? Northing { get; set; } // Northing
        public virtual double? Latitude { get; set; } // Latitude
        public virtual double? Longitude { get; set; } // Longitude
        public virtual short? DeliveryPointCount { get; set; } // DeliveryPointCount
    }

    // GISBaseData_Midpoints
    public class GisBaseDataMidpoints
    {
        public virtual string Outcode { get; set; } // Outcode
        public virtual int? Easting { get; set; } // Easting
        public virtual int? Northing { get; set; } // Northing
    }

    // ImportTmp_GISBaseData
    public class ImportTmpGisBaseData
    {
        public virtual string Outcode { get; set; } // Outcode
        public virtual string Incode { get; set; } // Incode
        public virtual string Type { get; set; } // Type
        public virtual int? Easting { get; set; } // Easting
        public virtual int? Northing { get; set; } // Northing
        public virtual double? Latitude { get; set; } // Latitude
        public virtual double? Longitude { get; set; } // Longitude
        public virtual short? DeliveryPointCount { get; set; } // DeliveryPointCount
    }

    // Locality
    public class Locality
    {
        public virtual int LocalityId { get; set; } // LocalityID
        public virtual string Locality_ { get; set; } // Locality
    }

    // Locality_orig
    public class LocalityOrig
    {
        public virtual int LocalityId { get; set; } // LocalityID
        public virtual string Locality { get; set; } // Locality
    }

    // Master
    public class Master
    {
        public virtual string Outcode { get; set; } // Outcode
        public virtual string Incode { get; set; } // Incode
        public virtual int RecordId { get; set; } // RecordID
        public virtual string Type { get; set; } // Type
        public virtual int? TownId { get; set; } // TownID
        public virtual int? LocalityId { get; set; } // LocalityID
        public virtual int? ThoroughfareId { get; set; } // ThoroughfareID
        public virtual int? DependantThoroughfareId { get; set; } // DependantThoroughfareID
        public virtual int? DeliveryPointCount { get; set; } // DeliveryPointCount

        // Foreign keys
        public virtual Town TownFk { get; set; } //  TownId - FkMasterTown
    }

    // Master_orig
    public class MasterOrig
    {
        public virtual string Outcode { get; set; } // Outcode
        public virtual string Incode { get; set; } // Incode
        public virtual int RecordId { get; set; } // RecordID
        public virtual string Type { get; set; } // Type
        public virtual int? TownId { get; set; } // TownID
        public virtual int? LocalityId { get; set; } // LocalityID
        public virtual int? ThoroughfareId { get; set; } // ThoroughfareID
        public virtual int? DependantThoroughfareId { get; set; } // DependantThoroughfareID
        public virtual int? DeliveryPointCount { get; set; } // DeliveryPointCount
    }

    // PostcodeLocation
    public class PostcodeLocation
    {
        public virtual string Postcode { get; set; } // Postcode
        public virtual int CountryId { get; set; } // CountryID
        public virtual double Latitude { get; set; } // Latitude
        public virtual double Longitude { get; set; } // Longitude
        public virtual double? Area { get; set; } // Area
        public virtual double? Radius { get; set; } // Radius
    }

    // PostcodeLocation_import
    public class PostcodeLocationImport
    {
        public virtual string Postcode { get; set; } // Postcode
        public virtual int CountryId { get; set; } // CountryID
        public virtual double Latitude { get; set; } // Latitude
        public virtual double Longitude { get; set; } // Longitude
        public virtual double? Area { get; set; } // Area
        public virtual double? Radius { get; set; } // Radius
    }

    // sysdiagrams
    public class Sysdiagrams
    {
        public virtual string Name { get; set; } // name
        public virtual int PrincipalId { get; set; } // principal_id
        public virtual int DiagramId { get; set; } // diagram_id
        public virtual int? Version { get; set; } // version
        public virtual string Definition { get; set; } // definition
    }

    // tblLocality
    public class TblLocality
    {
        public virtual int NbrLocality { get; set; } // nbrLocality
        public virtual string TxtLocality { get; set; } // txtLocality
    }

    // tblMaster
    public class TblMaster
    {
        public virtual string TxtOutcode { get; set; } // txtOutcode
        public virtual string TxtIncode { get; set; } // txtIncode
        public virtual int NbrRecord { get; set; } // nbrRecord
        public virtual string TxtType { get; set; } // txtType
        public virtual int? NbrTown { get; set; } // nbrTown
        public virtual int? NbrLocality { get; set; } // nbrLocality
        public virtual int? NbrThoroughfare { get; set; } // nbrThoroughfare
        public virtual int? NbrDependantThoroughfare { get; set; } // nbrDependantThoroughfare
        public virtual int? NbrDeliveryPointCount { get; set; } // nbrDeliveryPointCount
    }

    // tblThoroughfare
    public class TblThoroughfare
    {
        public virtual int NbrThoroughfare { get; set; } // nbrThoroughfare
        public virtual string TxtThoroughfare { get; set; } // txtThoroughfare
    }

    // tblTown
    public class TblTown
    {
        public virtual int NbrTown { get; set; } // nbrTown
        public virtual string TxtArea { get; set; } // txtArea
        public virtual string TxtTown { get; set; } // txtTown
        public virtual string TxtCounty { get; set; } // txtCounty
        public virtual string TxtCountyFlag { get; set; } // txtCountyFlag
        public virtual string TxtCountryCode { get; set; } // txtCountryCode
    }

    // Thoroughfare
    public class Thoroughfare
    {
        public virtual int ThoroughfareId { get; set; } // ThoroughfareID
        public virtual string Thoroughfare_ { get; set; } // Thoroughfare
    }

    // Thoroughfare_orig
    public class ThoroughfareOrig
    {
        public virtual int ThoroughfareId { get; set; } // ThoroughfareID
        public virtual string Thoroughfare { get; set; } // Thoroughfare
    }

    // Town
    public class Town
    {
        public virtual int TownId { get; set; } // TownID
        public virtual string Area { get; set; } // Area
        public virtual string Town_ { get; set; } // Town
        public virtual string County { get; set; } // County
        public virtual string CountyFlag { get; set; } // CountyFlag
        public virtual string CountryCode { get; set; } // CountryCode
    }

    // Town_orig
    public class TownOrig
    {
        public virtual int TownId { get; set; } // TownID
        public virtual string Area { get; set; } // Area
        public virtual string Town { get; set; } // Town
        public virtual string County { get; set; } // County
        public virtual string CountyFlag { get; set; } // CountyFlag
        public virtual string CountryCode { get; set; } // CountryCode
    }

    // vw_Postcode
    public class VwPostcode
    {
        public virtual string Postcode { get; set; } // Postcode
        public virtual string Locality { get; set; } // Locality
        public virtual string Thoroughfare { get; set; } // Thoroughfare
        public virtual string Area { get; set; } // Area
        public virtual string Town { get; set; } // Town
        public virtual string County { get; set; } // County
        public virtual string CountryCode { get; set; } // CountryCode
        public virtual string CountyFlag { get; set; } // CountyFlag
    }

    // Zone
    public class Zone
    {
        public virtual string ZoneReference { get; set; } // ZoneReference
        public virtual int CountryId { get; set; } // CountryID
        public virtual double Latitude { get; set; } // Latitude
        public virtual double Longitude { get; set; } // Longitude
    }


    // ************************************************************************
    // POCO Configuration

    // database_patch_history
    public class DatabasePatchHistoryConfiguration : EntityTypeConfiguration<DatabasePatchHistory>
    {
        public DatabasePatchHistoryConfiguration()
        {
            ToTable("dbo.database_patch_history");
            HasKey(x => x.DatabasePatchId);

            Property(x => x.DatabasePatchId).HasColumnName("database_patch_id").IsRequired();
            Property(x => x.Applied).HasColumnName("applied").IsRequired();
            Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("description").IsRequired().HasMaxLength(1000);
        }
    }

    // GISBaseData
    public class GisBaseDataConfiguration : EntityTypeConfiguration<GisBaseData>
    {
        public GisBaseDataConfiguration()
        {
            ToTable("dbo.GISBaseData");
            HasKey(x => x.Postcode);

            Property(x => x.Postcode).HasColumnName("Postcode").IsRequired().HasMaxLength(7);
            Property(x => x.RecordNumber).HasColumnName("RecordNumber").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Outcode).HasColumnName("Outcode").IsOptional().HasMaxLength(4);
            Property(x => x.Incode).HasColumnName("Incode").IsOptional().HasMaxLength(3);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasMaxLength(1);
            Property(x => x.Easting).HasColumnName("Easting").IsOptional();
            Property(x => x.Northing).HasColumnName("Northing").IsOptional();
            Property(x => x.Latitude).HasColumnName("Latitude").IsOptional();
            Property(x => x.Longitude).HasColumnName("Longitude").IsOptional();
            Property(x => x.DeliveryPointCount).HasColumnName("DeliveryPointCount").IsOptional();
        }
    }

    // GISBaseData_Midpoints
    public class GisBaseDataMidpointsConfiguration : EntityTypeConfiguration<GisBaseDataMidpoints>
    {
        public GisBaseDataMidpointsConfiguration()
        {
            ToTable("dbo.GISBaseData_Midpoints");
            HasKey(x => x.Outcode);

            Property(x => x.Outcode).HasColumnName("Outcode").IsRequired().HasMaxLength(4);
            Property(x => x.Easting).HasColumnName("Easting").IsOptional();
            Property(x => x.Northing).HasColumnName("Northing").IsOptional();
        }
    }

    // ImportTmp_GISBaseData
    public class ImportTmpGisBaseDataConfiguration : EntityTypeConfiguration<ImportTmpGisBaseData>
    {
        public ImportTmpGisBaseDataConfiguration()
        {
            ToTable("dbo.ImportTmp_GISBaseData");
            HasKey(x => new {  });

            Property(x => x.Outcode).HasColumnName("Outcode").IsOptional().HasMaxLength(40);
            Property(x => x.Incode).HasColumnName("Incode").IsOptional().HasMaxLength(30);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasMaxLength(1);
            Property(x => x.Easting).HasColumnName("Easting").IsOptional();
            Property(x => x.Northing).HasColumnName("Northing").IsOptional();
            Property(x => x.Latitude).HasColumnName("Latitude").IsOptional();
            Property(x => x.Longitude).HasColumnName("Longitude").IsOptional();
            Property(x => x.DeliveryPointCount).HasColumnName("DeliveryPointCount").IsOptional();
        }
    }

    // Locality
    public class LocalityConfiguration : EntityTypeConfiguration<Locality>
    {
        public LocalityConfiguration()
        {
            ToTable("dbo.Locality");
            HasKey(x => x.LocalityId);

            Property(x => x.LocalityId).HasColumnName("LocalityID").IsRequired();
            Property(x => x.Locality_).HasColumnName("_Locality").IsOptional().HasMaxLength(50);
        }
    }

    // Locality_orig
    public class LocalityOrigConfiguration : EntityTypeConfiguration<LocalityOrig>
    {
        public LocalityOrigConfiguration()
        {
            ToTable("dbo.Locality_orig");
            HasKey(x => x.LocalityId);

            Property(x => x.LocalityId).HasColumnName("LocalityID").IsRequired();
            Property(x => x.Locality).HasColumnName("Locality").IsOptional().HasMaxLength(50);
        }
    }

    // Master
    public class MasterConfiguration : EntityTypeConfiguration<Master>
    {
        public MasterConfiguration()
        {
            ToTable("dbo.Master");
            HasKey(x => new { x.Outcode, x.Incode, x.RecordId });

            Property(x => x.Outcode).HasColumnName("Outcode").IsRequired().HasMaxLength(4);
            Property(x => x.Incode).HasColumnName("Incode").IsRequired().HasMaxLength(3);
            Property(x => x.RecordId).HasColumnName("RecordID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasMaxLength(1);
            Property(x => x.TownId).HasColumnName("TownID").IsOptional();
            Property(x => x.LocalityId).HasColumnName("LocalityID").IsOptional();
            Property(x => x.ThoroughfareId).HasColumnName("ThoroughfareID").IsOptional();
            Property(x => x.DependantThoroughfareId).HasColumnName("DependantThoroughfareID").IsOptional();
            Property(x => x.DeliveryPointCount).HasColumnName("DeliveryPointCount").IsOptional();

            // Foreign keys
            HasOptional(a => a.TownFk).WithMany().HasForeignKey(b => b.TownId); // FkMasterTown
        }
    }

    // Master_orig
    public class MasterOrigConfiguration : EntityTypeConfiguration<MasterOrig>
    {
        public MasterOrigConfiguration()
        {
            ToTable("dbo.Master_orig");
            HasKey(x => new { x.Outcode, x.Incode, x.RecordId });

            Property(x => x.Outcode).HasColumnName("Outcode").IsRequired().HasMaxLength(4);
            Property(x => x.Incode).HasColumnName("Incode").IsRequired().HasMaxLength(3);
            Property(x => x.RecordId).HasColumnName("RecordID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasMaxLength(1);
            Property(x => x.TownId).HasColumnName("TownID").IsOptional();
            Property(x => x.LocalityId).HasColumnName("LocalityID").IsOptional();
            Property(x => x.ThoroughfareId).HasColumnName("ThoroughfareID").IsOptional();
            Property(x => x.DependantThoroughfareId).HasColumnName("DependantThoroughfareID").IsOptional();
            Property(x => x.DeliveryPointCount).HasColumnName("DeliveryPointCount").IsOptional();
        }
    }

    // PostcodeLocation
    public class PostcodeLocationConfiguration : EntityTypeConfiguration<PostcodeLocation>
    {
        public PostcodeLocationConfiguration()
        {
            ToTable("dbo.PostcodeLocation");
            HasKey(x => x.Postcode);

            Property(x => x.Postcode).HasColumnName("Postcode").IsRequired().HasMaxLength(20);
            Property(x => x.CountryId).HasColumnName("CountryID").IsRequired();
            Property(x => x.Latitude).HasColumnName("Latitude").IsRequired();
            Property(x => x.Longitude).HasColumnName("Longitude").IsRequired();
            Property(x => x.Area).HasColumnName("Area").IsOptional();
            Property(x => x.Radius).HasColumnName("Radius").IsOptional();
        }
    }

    // PostcodeLocation_import
    public class PostcodeLocationImportConfiguration : EntityTypeConfiguration<PostcodeLocationImport>
    {
        public PostcodeLocationImportConfiguration()
        {
            ToTable("dbo.PostcodeLocation_import");
            HasKey(x => new { x.Postcode, x.CountryId, x.Latitude, x.Longitude });

            Property(x => x.Postcode).HasColumnName("Postcode").IsRequired().HasMaxLength(20);
            Property(x => x.CountryId).HasColumnName("CountryID").IsRequired();
            Property(x => x.Latitude).HasColumnName("Latitude").IsRequired();
            Property(x => x.Longitude).HasColumnName("Longitude").IsRequired();
            Property(x => x.Area).HasColumnName("Area").IsOptional();
            Property(x => x.Radius).HasColumnName("Radius").IsOptional();
        }
    }

    // sysdiagrams
    public class SysdiagramsConfiguration : EntityTypeConfiguration<Sysdiagrams>
    {
        public SysdiagramsConfiguration()
        {
            ToTable("dbo.sysdiagrams");
            HasKey(x => x.DiagramId);

            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(128);
            Property(x => x.PrincipalId).HasColumnName("principal_id").IsRequired();
            Property(x => x.DiagramId).HasColumnName("diagram_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Version).HasColumnName("version").IsOptional();
            Property(x => x.Definition).HasColumnName("definition").IsOptional();
        }
    }

    // tblLocality
    public class TblLocalityConfiguration : EntityTypeConfiguration<TblLocality>
    {
        public TblLocalityConfiguration()
        {
            ToTable("dbo.tblLocality");
            HasKey(x => new { x.NbrLocality });

            Property(x => x.NbrLocality).HasColumnName("nbrLocality").IsRequired();
            Property(x => x.TxtLocality).HasColumnName("txtLocality").IsOptional().HasMaxLength(50);
        }
    }

    // tblMaster
    public class TblMasterConfiguration : EntityTypeConfiguration<TblMaster>
    {
        public TblMasterConfiguration()
        {
            ToTable("dbo.tblMaster");
            HasKey(x => new { x.TxtOutcode, x.TxtIncode, x.NbrRecord });

            Property(x => x.TxtOutcode).HasColumnName("txtOutcode").IsRequired().HasMaxLength(4);
            Property(x => x.TxtIncode).HasColumnName("txtIncode").IsRequired().HasMaxLength(3);
            Property(x => x.NbrRecord).HasColumnName("nbrRecord").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TxtType).HasColumnName("txtType").IsOptional().HasMaxLength(1);
            Property(x => x.NbrTown).HasColumnName("nbrTown").IsOptional();
            Property(x => x.NbrLocality).HasColumnName("nbrLocality").IsOptional();
            Property(x => x.NbrThoroughfare).HasColumnName("nbrThoroughfare").IsOptional();
            Property(x => x.NbrDependantThoroughfare).HasColumnName("nbrDependantThoroughfare").IsOptional();
            Property(x => x.NbrDeliveryPointCount).HasColumnName("nbrDeliveryPointCount").IsOptional();
        }
    }

    // tblThoroughfare
    public class TblThoroughfareConfiguration : EntityTypeConfiguration<TblThoroughfare>
    {
        public TblThoroughfareConfiguration()
        {
            ToTable("dbo.tblThoroughfare");
            HasKey(x => new { x.NbrThoroughfare });

            Property(x => x.NbrThoroughfare).HasColumnName("nbrThoroughfare").IsRequired();
            Property(x => x.TxtThoroughfare).HasColumnName("txtThoroughfare").IsOptional().HasMaxLength(50);
        }
    }

    // tblTown
    public class TblTownConfiguration : EntityTypeConfiguration<TblTown>
    {
        public TblTownConfiguration()
        {
            ToTable("dbo.tblTown");
            HasKey(x => new { x.NbrTown });

            Property(x => x.NbrTown).HasColumnName("nbrTown").IsRequired();
            Property(x => x.TxtArea).HasColumnName("txtArea").IsOptional().HasMaxLength(2);
            Property(x => x.TxtTown).HasColumnName("txtTown").IsOptional().HasMaxLength(22);
            Property(x => x.TxtCounty).HasColumnName("txtCounty").IsOptional().HasMaxLength(18);
            Property(x => x.TxtCountyFlag).HasColumnName("txtCountyFlag").IsOptional().HasMaxLength(1);
            Property(x => x.TxtCountryCode).HasColumnName("txtCountryCode").IsOptional().HasMaxLength(1);
        }
    }

    // Thoroughfare
    public class ThoroughfareConfiguration : EntityTypeConfiguration<Thoroughfare>
    {
        public ThoroughfareConfiguration()
        {
            ToTable("dbo.Thoroughfare");
            HasKey(x => x.ThoroughfareId);

            Property(x => x.ThoroughfareId).HasColumnName("ThoroughfareID").IsRequired();
            Property(x => x.Thoroughfare_).HasColumnName("_Thoroughfare").IsOptional().HasMaxLength(50);
        }
    }

    // Thoroughfare_orig
    public class ThoroughfareOrigConfiguration : EntityTypeConfiguration<ThoroughfareOrig>
    {
        public ThoroughfareOrigConfiguration()
        {
            ToTable("dbo.Thoroughfare_orig");
            HasKey(x => x.ThoroughfareId);

            Property(x => x.ThoroughfareId).HasColumnName("ThoroughfareID").IsRequired();
            Property(x => x.Thoroughfare).HasColumnName("Thoroughfare").IsOptional().HasMaxLength(50);
        }
    }

    // Town
    public class TownConfiguration : EntityTypeConfiguration<Town>
    {
        public TownConfiguration()
        {
            ToTable("dbo.Town");
            HasKey(x => x.TownId);

            Property(x => x.TownId).HasColumnName("TownID").IsRequired();
            Property(x => x.Area).HasColumnName("Area").IsOptional().HasMaxLength(2);
            Property(x => x.Town_).HasColumnName("_Town").IsOptional().HasMaxLength(22);
            Property(x => x.County).HasColumnName("County").IsOptional().HasMaxLength(18);
            Property(x => x.CountyFlag).HasColumnName("CountyFlag").IsOptional().HasMaxLength(1);
            Property(x => x.CountryCode).HasColumnName("CountryCode").IsOptional().HasMaxLength(1);
        }
    }

    // Town_orig
    public class TownOrigConfiguration : EntityTypeConfiguration<TownOrig>
    {
        public TownOrigConfiguration()
        {
            ToTable("dbo.Town_orig");
            HasKey(x => x.TownId);

            Property(x => x.TownId).HasColumnName("TownID").IsRequired();
            Property(x => x.Area).HasColumnName("Area").IsOptional().HasMaxLength(2);
            Property(x => x.Town).HasColumnName("Town").IsOptional().HasMaxLength(22);
            Property(x => x.County).HasColumnName("County").IsOptional().HasMaxLength(18);
            Property(x => x.CountyFlag).HasColumnName("CountyFlag").IsOptional().HasMaxLength(1);
            Property(x => x.CountryCode).HasColumnName("CountryCode").IsOptional().HasMaxLength(1);
        }
    }

    // vw_Postcode
    public class VwPostcodeConfiguration : EntityTypeConfiguration<VwPostcode>
    {
        public VwPostcodeConfiguration()
        {
            ToTable("dbo.vw_Postcode");
            HasKey(x => new { x.Postcode });

            Property(x => x.Postcode).HasColumnName("Postcode").IsRequired().HasMaxLength(8);
            Property(x => x.Locality).HasColumnName("Locality").IsOptional().HasMaxLength(50);
            Property(x => x.Thoroughfare).HasColumnName("Thoroughfare").IsOptional().HasMaxLength(50);
            Property(x => x.Area).HasColumnName("Area").IsOptional().HasMaxLength(2);
            Property(x => x.Town).HasColumnName("Town").IsOptional().HasMaxLength(22);
            Property(x => x.County).HasColumnName("County").IsOptional().HasMaxLength(18);
            Property(x => x.CountryCode).HasColumnName("CountryCode").IsOptional().HasMaxLength(1);
            Property(x => x.CountyFlag).HasColumnName("CountyFlag").IsOptional().HasMaxLength(1);
        }
    }

    // Zone
    public class ZoneConfiguration : EntityTypeConfiguration<Zone>
    {
        public ZoneConfiguration()
        {
            ToTable("dbo.Zone");
            HasKey(x => new { x.ZoneReference, x.CountryId });

            Property(x => x.ZoneReference).HasColumnName("ZoneReference").IsRequired().HasMaxLength(50);
            Property(x => x.CountryId).HasColumnName("CountryID").IsRequired();
            Property(x => x.Latitude).HasColumnName("Latitude").IsRequired();
            Property(x => x.Longitude).HasColumnName("Longitude").IsRequired();
        }
    }

}

