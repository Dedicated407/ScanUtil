namespace ScanUtil;

public class ScanModel
{
    public int ProcessedFiles { get; init; }
    public int JSDetects { get; init; }
    public int RmRfDetects { get; init; }
    public int RunDllDetects { get; init; }
    public int Errors { get; init; }
    public TimeSpan ExecutionTime { get; init; }

    public override string ToString() =>
        "====== Scan result ======\n" +
        $"Processed files: {ProcessedFiles}\n" +
        $"JS detects: {JSDetects}\n" +
        $"rm -rf detects: {RmRfDetects}\n" +
        $"Rundll32 detects: {RunDllDetects}\n" +
        $"Errors: {Errors}\n" +
        $"Execution time: {ExecutionTime}\n" +
        "=========================";
}