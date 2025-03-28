namespace TodoApp.Application.Features;

public abstract class CommandHandler<TRes, TReq>
{
    public abstract TRes Execute(TReq request);
}