namespace DBModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentType> StudentTypes { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherType> TeacherTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("RolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Students)
                .Map(m => m.ToTable("StudentSubject").MapLeftKey("StudentId").MapRightKey("SubjectId"));

            modelBuilder.Entity<StudentType>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.StudentType)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Teacher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TeacherType>()
                .HasMany(e => e.Teachers)
                .WithOptional(e => e.TeacherType)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<DBModel.RegistrationObject> RegistrationObjects { get; set; }
    }
}
