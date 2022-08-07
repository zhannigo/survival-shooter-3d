using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Factory
{
  public interface IUIFactory : IService
  { 
    Task CreateUIRoot(); 
    void CreateShop();
  }
}