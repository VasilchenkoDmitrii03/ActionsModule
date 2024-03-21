
using ActionsLib;
using System.Xml.Serialization;
class Program
{
    List<MetricType> _metricTypes = new List<MetricType>();

    public static void Main(string[] args)
    {
        MetricTypeList t = addSampleMetrics();
        t.Save(@"C:\Dmitrii\Test\228.bin");
        MetricTypeList a = MetricTypeList.Load(@"C:\Dmitrii\Test\228.bin");
        a.Save(@"C:\Dmitrii\Test\229.bin");
    }


    public static MetricTypeList addSampleMetrics()
    {
        MetricTypeList tmp = new MetricTypeList("test");
        IntegerMetricType position = IntegerMetricType.createIntegerMetricType("position", "position on the playground", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "1", "2", "3", "4", "5", "6" });
        IntegerMetricType quality = IntegerMetricType.createIntegerMetricType("quality", "quality of any action from 1-6", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "=", "-", "/", "!", "+", "#" });
        tmp.Add(position);
        tmp.Add(quality);
        return tmp;
    }
}