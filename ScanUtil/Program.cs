using System.Diagnostics;
using ScanUtil.Models;

namespace ScanUtil;

public static class Program
{
    private static string _path = "";
    private static int _jsCounter = 0;
    private static int _rmRfDetectsCounter = 0;
    private static int _runDllDetectsCounter = 0;

    public static void Main(string[] args)
    {
        var stopWatch = Stopwatch.StartNew();
        _path = args[0];

        var files = Directory.GetFiles(_path);
        var errorsCounter = 0;

        Parallel.ForEach(files, file =>
        {
            try
            {
                FindSuspiciousFile(file);
            }
            catch (Exception)
            {
                Interlocked.Increment(ref errorsCounter);
            }
        });

        stopWatch.Stop();

        ScanModel scanModel = new()
        {
            ProcessedFiles = files.Length,
            JSDetects = _jsCounter,
            RmRfDetects = _rmRfDetectsCounter,
            RunDllDetects = _runDllDetectsCounter,
            Errors = errorsCounter,
            ExecutionTime = stopWatch.Elapsed
        };

        Console.WriteLine(scanModel.ToString());
    }

    
    /// <summary>
    /// Поиск в файле подозрительной строки.
    /// </summary>
    /// <param name="file">Файл в директории</param>
    private static void FindSuspiciousFile(string file)
    {
        string? line;
        using var reader = File.OpenText(file);
        while ((line = reader.ReadLine()) != null)
        {
            var increment = line switch
            {
                var x when file.Contains(".js") && x.Contains("<script>evil_script()</script>") => Interlocked.Increment(ref _jsCounter),
                var x when x.Contains($"rm -rf {_path}") => Interlocked.Increment(ref _rmRfDetectsCounter),
                var x when x.Contains("Rundll32 sus.dll SusEntry") => Interlocked.Increment(ref _runDllDetectsCounter),
                _ => -1
            };

            if (increment != -1)
            {
                break;
            }
        }
    }
}