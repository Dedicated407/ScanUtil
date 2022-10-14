namespace ScanUtil.Models.Base;

public abstract class BaseModel
{
    public Guid Id { get; set; }

    public DateTimeOffset Created { get; set; }

    protected BaseModel()
    {
        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
    }
}