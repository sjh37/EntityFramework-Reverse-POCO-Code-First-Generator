
// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `MyDbContext`
//     Connection String:      `Data Source=(local);Initial Catalog=SimpleTest;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator`

using System;
using System.Data.Entity.ModelConfiguration;

namespace fred.Model
{
    public class VwCustomers
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual int CustomerId { get; set; }
    }

    public class SchemaTable
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    public class TestSchemaTable
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    public class GroupTestMaster
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class GroupTestDetail
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int Number { get; set; }
        public virtual int MasterId { get; set; }

        // Foreign keys
        public virtual GroupTestMaster MasterFk { get; set; } //  MasterId - FkGroupTestDetailGroupTestMaster
    }

    public class RuntimeTable
    {
        public virtual int Id { get; set; }
    }

    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual int Age { get; set; }
    }

    public class Orders
    {
        public virtual int OrderId { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int CustomerId { get; set; }

        // Foreign keys
        public virtual Customers CustomerFk { get; set; } //  CustomerId - FkOrdersCustomers
    }

    public class OrderItems
    {
        public virtual int OrderItemId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int ItemId { get; set; }
        public virtual int Quantity { get; set; }

        // Foreign keys
        public virtual Orders OrderFk { get; set; } //  OrderId - FkOrderItemsOrders
        public virtual Items ItemFk { get; set; } //  ItemId - FkOrderItemsItems
    }

    public class Images
    {
        public virtual int Id { get; set; }
        public virtual byte[] TheImage { get; set; }
    }

    public class Items
    {
        public virtual int ItemId { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
    }

    public class Customers
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
    }

    public class PagingTest
    {
        public virtual int Id { get; set; }
        public virtual int Dummy { get; set; }
    }

    public class Blobs
    {
        public virtual int Id { get; set; }
        public virtual string Data { get; set; }
    }

    public class EnumTest
    {
        public virtual int Id { get; set; }
        public virtual int Flag { get; set; }
    }

    public class DeleteTest
    {
        public virtual int Id { get; set; }
    }

    public class DecimalTest
    {
        public virtual int Id { get; set; }
        public virtual decimal Value { get; set; }
    }

}

namespace fred.ModelConfiguration
{
    using Model;

    public class VwCustomersConfiguration : EntityTypeConfiguration<VwCustomers>
    {
        public VwCustomersConfiguration()
        {
            ToTable("dbo.VwCustomers");

            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Address).HasColumnName("Address").IsRequired().HasMaxLength(200);
            Property(x => x.CustomerId).HasColumnName("CustomerId").IsRequired();
        }
    }

    public class SchemaTableConfiguration : EntityTypeConfiguration<SchemaTable>
    {
        public SchemaTableConfiguration()
        {
            ToTable("dbo.SchemaTable");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsRequired().HasMaxLength(100);
        }
    }

    public class TestSchemaTableConfiguration : EntityTypeConfiguration<TestSchemaTable>
    {
        public TestSchemaTableConfiguration()
        {
            ToTable("test.SchemaTable");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsRequired().HasMaxLength(100);
        }
    }

    public class GroupTestMasterConfiguration : EntityTypeConfiguration<GroupTestMaster>
    {
        public GroupTestMasterConfiguration()
        {
            ToTable("dbo.GroupTestMaster");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        }
    }

    public class GroupTestDetailConfiguration : EntityTypeConfiguration<GroupTestDetail>
    {
        public GroupTestDetailConfiguration()
        {
            ToTable("dbo.GroupTestDetail");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Date).HasColumnName("Date").IsRequired();
            Property(x => x.Number).HasColumnName("Number").IsRequired();
            Property(x => x.MasterId).HasColumnName("MasterId").IsRequired();

            // Foreign keys
            HasRequired(a => a.MasterFk).WithMany().HasForeignKey(b => b.MasterId); // FkGroupTestDetailGroupTestMaster
        }
    }

    public class RuntimeTableConfiguration : EntityTypeConfiguration<RuntimeTable>
    {
        public RuntimeTableConfiguration()
        {
            ToTable("dbo.RuntimeTable");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
        }
    }

    public class UsersConfiguration : EntityTypeConfiguration<Users>
    {
        public UsersConfiguration()
        {
            ToTable("dbo.Users");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(100);
            Property(x => x.Age).HasColumnName("Age").IsRequired();
        }
    }

    public class OrdersConfiguration : EntityTypeConfiguration<Orders>
    {
        public OrdersConfiguration()
        {
            ToTable("dbo.Orders");

            Property(x => x.OrderId).HasColumnName("OrderId").IsRequired();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsRequired();
            Property(x => x.CustomerId).HasColumnName("CustomerId").IsRequired();

            // Foreign keys
            HasRequired(a => a.CustomerFk).WithMany().HasForeignKey(b => b.CustomerId); // FkOrdersCustomers
        }
    }

    public class OrderItemsConfiguration : EntityTypeConfiguration<OrderItems>
    {
        public OrderItemsConfiguration()
        {
            ToTable("dbo.OrderItems");

            Property(x => x.OrderItemId).HasColumnName("OrderItemId").IsRequired();
            Property(x => x.OrderId).HasColumnName("OrderId").IsRequired();
            Property(x => x.ItemId).HasColumnName("ItemId").IsRequired();
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();

            // Foreign keys
            HasRequired(a => a.OrderFk).WithMany().HasForeignKey(b => b.OrderId); // FkOrderItemsOrders
            HasRequired(a => a.ItemFk).WithMany().HasForeignKey(b => b.ItemId); // FkOrderItemsItems
        }
    }

    public class ImagesConfiguration : EntityTypeConfiguration<Images>
    {
        public ImagesConfiguration()
        {
            ToTable("dbo.Images");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.TheImage).HasColumnName("TheImage").IsRequired().HasMaxLength(2147483647);
        }
    }

    public class ItemsConfiguration : EntityTypeConfiguration<Items>
    {
        public ItemsConfiguration()
        {
            ToTable("dbo.Items");

            Property(x => x.ItemId).HasColumnName("ItemId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Price).HasColumnName("Price").IsRequired();
        }
    }

    public class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersConfiguration()
        {
            ToTable("dbo.Customers");

            Property(x => x.CustomerId).HasColumnName("CustomerId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Address).HasColumnName("Address").IsRequired().HasMaxLength(200);
        }
    }

    public class PagingTestConfiguration : EntityTypeConfiguration<PagingTest>
    {
        public PagingTestConfiguration()
        {
            ToTable("dbo.PagingTest");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Dummy).HasColumnName("Dummy").IsRequired();
        }
    }

    public class BlobsConfiguration : EntityTypeConfiguration<Blobs>
    {
        public BlobsConfiguration()
        {
            ToTable("dbo.Blobs");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Data).HasColumnName("Data").IsRequired();
        }
    }

    public class EnumTestConfiguration : EntityTypeConfiguration<EnumTest>
    {
        public EnumTestConfiguration()
        {
            ToTable("dbo.EnumTest");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Flag).HasColumnName("Flag").IsRequired();
        }
    }

    public class DeleteTestConfiguration : EntityTypeConfiguration<DeleteTest>
    {
        public DeleteTestConfiguration()
        {
            ToTable("dbo.DeleteTest");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
        }
    }

    public class DecimalTestConfiguration : EntityTypeConfiguration<DecimalTest>
    {
        public DecimalTestConfiguration()
        {
            ToTable("dbo.DecimalTest");

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Value).HasColumnName("Value").IsRequired();
        }
    }

}

