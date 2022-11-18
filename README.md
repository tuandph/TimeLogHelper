# TimeLogHelper

## Usage

```C
static void Main(string[] args)
{
    // Start log
    var stwStart = TimeLogCsvHelper.StartMeasure("Function 1");
    Thread.Sleep(3000);
    stwStart.StopMeasure();

    // Nested Level 2
    var stwStart1 = TimeLogCsvHelper.StartMeasure("Function with nested Level 2", 2);
    Thread.Sleep(1000);
    stwStart1.StopMeasure();

    // Nested Level 3
    var stwStart2 = TimeLogCsvHelper.StartMeasure("Function with nested Level 3", 3);
    Thread.Sleep(1000);
    stwStart2.StopMeasure();

    // Save final result
    TimeLogCsvHelper.SaveCsvFileFromMemories();

    Console.WriteLine("Hello World!");
}
```

## Result
![image](https://user-images.githubusercontent.com/12756406/202612604-f8dfb1c4-8eb8-4af2-8297-d97afabf96e2.png)
