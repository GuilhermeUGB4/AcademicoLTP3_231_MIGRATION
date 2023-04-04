using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Data
{
    public class AcademicoContext : DbContext
    {
        public AcademicoContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<Instituicao> Instituicoes { get; set; }
    }
}
