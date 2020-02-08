using AutoMapper;

namespace northwind.common.mapping
{
  public interface IMapExplicitly
  {
    public void RegisterMappings(IProfileExpression profile);
  }

}