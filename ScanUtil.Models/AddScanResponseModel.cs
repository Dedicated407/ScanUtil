using ScanUtil.Models.Base;

namespace ScanUtil.Models;

public class AddScanResponseModel : BaseModel
{
    public bool IsFinished { get; set; } = false;
    public string Directory { get; set; }
    public string? Result { get; set; }
}