using ScanUtil.Models.Base;

namespace ScanUtil.Models;

public class ScanResponseModel : ScanModel
{
    public bool IsFinished { get; set; } = false;
    public string Directory { get; set; }

    public override string ToString() =>
        "====== Scan result ======\n" +
        $"Directory: {Directory}\n" +
        $"Processed files: {ProcessedFiles}\n" +
        $"JS detects: {JSDetects}\n" +
        $"rm -rf detects: {RmRfDetects}\n" +
        $"Rundll32 detects: {RunDllDetects}\n" +
        $"Errors: {Errors}\n" +
        $"Execution time: {ExecutionTime}\n" +
        "=========================";
}