﻿// <auto-generated />
using System;
using ImageCompression.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageCompression.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageCompression.Model.Entities.Image", b =>
                {
                    b.Property<Guid>("imageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("blobImage");

                    b.Property<string>("filename");

                    b.Property<DateTime>("processingEnd");

                    b.Property<DateTime>("processingStart");

                    b.Property<float>("sizeAfterCompression");

                    b.Property<float>("sizeBeforeCompression");

                    b.HasKey("imageID");

                    b.ToTable("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
