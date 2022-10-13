using System.Diagnostics;

namespace ScanUtil;

public static class Program
{
    public static void Main(string[] args)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var files = Directory.GetFiles(args[0]);

        ushort jsCounter = 0;
        ushort rmRfDetectsCounter = 0;
        ushort runDllDetectsCounter = 0;
        ushort errorsCounter = 0;

        Parallel.ForEach(files, file =>
        {
            try
            {
                var lines = File.ReadLines(file);

                switch (lines)
                {
                    case { } a when file.Contains(".js") && a.Contains("<script>evil_script()</script>"):
                        jsCounter++;
                        break;
                    case { } b when b.Contains($"rm -rf {args[0]}"):
                        rmRfDetectsCounter++;
                        break;
                    case { } c when c.Contains("Rundll32 sus.dll SusEntry"):
                        runDllDetectsCounter++;
                        break;
                }
            }
            catch (Exception)
            {
                errorsCounter++;
            }
        });

        stopWatch.Stop();

        ScanModel scanModel = new()
        {
            ProcessedFiles = (ushort) files.Length,
            JSDetects = jsCounter,
            RmRfDetects = rmRfDetectsCounter,
            RunDllDetects = runDllDetectsCounter,
            Errors = errorsCounter,
            ExecutionTime = stopWatch.Elapsed
        };

        Console.WriteLine(scanModel.ToString());
    }
}