#region - Imports
using Microsoft.EntityFrameworkCore;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
#endregion

namespace COE000.Portal.NomeProjeto.Data;

public class IdentityBuilderContext : IdentityDbContext<IncriseUserModel>
{
    public IdentityBuilderContext(DbContextOptions<IdentityBuilderContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<IncriseUserModel>
{
    public void Configure(EntityTypeBuilder<IncriseUserModel> builder) 
        => builder.Property(p => p.UserGender).HasColumnName("Gender");
}