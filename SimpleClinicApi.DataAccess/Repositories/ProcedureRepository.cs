using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public class ProcedureRepository(ClinicDbContext context)
      : GenericRepository<Procedure, ClinicDbContext>(context), IProcedureRepository;
}