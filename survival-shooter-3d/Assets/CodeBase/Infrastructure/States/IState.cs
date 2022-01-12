namespace CodeBase.Infrastructure.States
{
  public interface IExitableState
  {
    void Exit();
  }
  
  public interface IState:IExitableState
  {
    void Enter();
  }

  public interface IPayLoadedState<TPayLoad>:IExitableState
  {
    void Enter(TPayLoad payLoad);
  }
}