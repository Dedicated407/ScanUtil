namespace ScanUtil.Models.Base;

public abstract class BaseModel
{
    public Guid Id { get; private set; }

    public DateTimeOffset Created { get; private set; }

    protected BaseModel()
    {
        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
    }
}