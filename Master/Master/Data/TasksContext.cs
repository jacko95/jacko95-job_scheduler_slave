using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Master.Models
{
    public partial class TasksContext : IdentityDbContext<SiteUser>
    {
        public TasksContext()
        {
        }

        public TasksContext(DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupsNodes> GroupsNodes { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Nodes> Nodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=E:\\repos\\Master\\Master\\Tasks.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Groups_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<GroupsNodes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId)
                    .HasColumnType("INT")
                    .HasColumnName("group_id");

                entity.Property(e => e.NodeId)
                    .HasColumnType("INT")
                    .HasColumnName("node_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsNodes)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.GroupsNodes)
                    .HasForeignKey(d => d.NodeId);
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arguments).HasColumnName("arguments");

                entity.Property(e => e.Command)
                    .IsRequired()
                    .HasColumnName("command");

                entity.Property(e => e.CronString).HasColumnName("cron_string");

                entity.Property(e => e.GroupId)
                    .HasColumnType("INT")
                    .HasColumnName("group_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExitCode)
                    .HasColumnType("INT")
                    .HasColumnName("exit_code");

                entity.Property(e => e.JobId)
                    .HasColumnType("INT")
                    .HasColumnName("job_id");

                entity.Property(e => e.Output).HasColumnName("output");

                entity.Property(e => e.Pid)
                    .HasColumnType("INT")
                    .HasColumnName("pid");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("DATETIME")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.JobId);
            });

            modelBuilder.Entity<Nodes>(entity =>
            {
                entity.HasIndex(e => new { e.Host, e.Port }, "IX_Nodes_host_port")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(40)")
                    .HasColumnName("host");

                entity.Property(e => e.Master)
                    .IsRequired()
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("master");

                entity.Property(e => e.Port)
                    .HasColumnType("INT")
                    .HasColumnName("port");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
