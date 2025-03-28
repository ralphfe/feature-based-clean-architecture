namespace TodoApp.Application.Features;

public abstract class QueryHandler<TRes, TReq>
{
    public abstract TRes Execute(TReq request);
}

public abstract class QueryHandler<TRes>
{
    public abstract TRes Execute();
}
