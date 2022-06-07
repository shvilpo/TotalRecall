using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

internal delegate void Feedback(Int32 value);
class Program
{
    static void Main(string[] args)
    {
        StaticDelegatDemo();
    }

    private static void StaticDelegatDemo()
    {
        Console.WriteLine("--- Static Delegat Demo ---");
        Counter(1, 3, new Feedback(Program.FeedbackToConsole));
        Counter(1, 3, new Feedback(FeedbackToMsgBox));

    }
    private static void Counter(Int32 from, Int32 to, Feedback fb)
    {
        for (Int32 val = from; val <= to; val++)
        {
            // Если указаны методы обратного вызова, вызываем их
            if (fb != null)
                fb(val);
        }
    }

    private static void FeedbackToConsole(Int32 value)
    {
        Console.WriteLine("Item=" + value);
    }
    private static void FeedbackToMsgBox(Int32 value)
    {
        MessageBox.Show("Item=" + value);
    }
    private void FeedbackToFile(Int32 value)
    {
        using (StreamWriter sw = new StreamWriter("Status", true))
        {
            sw.WriteLine("Item=" + value);
        }
    }
}
