using Microsoft.EntityFrameworkCore;

namespace northwind.domain
{
  public interface IDbContext
  {
    DbContext Instance { get; }
  }
}