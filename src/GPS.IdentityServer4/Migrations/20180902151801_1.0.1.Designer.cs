﻿// <auto-generated />
using System;
using GPS.IdentityServer4.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GPS.IdentityServer4.Migrations
{
    [DbContext(typeof(GPSIdentityServerDbContext))]
    [Migration("20180902151801_1.0.1")]
    partial class _101
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("GPS.IdentityServer4.Models.GPS_RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimsJson");

                    b.Property<string>("ClientIp");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("OldClaimsJson");

                    b.Property<string>("OldToken");

                    b.Property<string>("Remark");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("GPS_RefreshToken");
                });

            modelBuilder.Entity("GPS.IdentityServer4.Models.GPS_Token", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimsJson");

                    b.Property<string>("ClientIp");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Remark");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("GPS_Token");
                });

            modelBuilder.Entity("GPS.IdentityServer4.Models.GPS_Token_History", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimsJson");

                    b.Property<string>("ClientIp");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Remark");

                    b.Property<int>("ResultCode");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("GPS_Token_History");
                });
#pragma warning restore 612, 618
        }
    }
}