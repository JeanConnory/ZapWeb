﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZapWeb.Database;

namespace ZapWeb.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20210922231029_AddAlteracaMensagem")]
    partial class AddAlteracaMensagem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("ZapWeb.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Usuarios");

                    b.HasKey("Id");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("ZapWeb.Models.Mensagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataCriacao");

                    b.Property<string>("NomeGrupo");

                    b.Property<string>("Texto");

                    b.Property<int>("UsuarioId");

                    b.Property<string>("UsuarioJson");

                    b.HasKey("Id");

                    b.ToTable("Mensagens");
                });

            modelBuilder.Entity("ZapWeb.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsOnline");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
