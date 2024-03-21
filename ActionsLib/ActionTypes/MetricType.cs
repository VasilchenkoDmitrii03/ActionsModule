using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
namespace ActionsLib
{
    [XmlInclude(typeof(MetricType))]
    [Serializable]
    public class MetricType
    {
        string _name;
        string _description;
        Type _valueType;
        bool _isCheckable;
        Dictionary<object, string> _acceptableValuesNames;
        public MetricType()
        {
            _name = "unknown";
            _description = "unknown";
            _valueType = typeof(int);
            _isCheckable = true;
            _acceptableValuesNames = new Dictionary<object, string>();
        }
        public MetricType(string name, string desctiprion = "unknown", Type type = null, Dictionary<object, string> acceptableValues = null, bool isCheckable = true)
        {
            _name = name;
            _description = desctiprion;
            _valueType = type;
            _acceptableValuesNames = acceptableValues;
            _isCheckable = isCheckable;
        }
       
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public Type ValueType
        {
            get { return _valueType; }
        }
        public List<object> AcceptableValues
        {
            get
            {
                return new List<object>(_acceptableValuesNames.Keys);
            }
        }
        public Dictionary<object, string> AcceptableValuesNames
        {
            get
            {
                return _acceptableValuesNames;
            }
            set
            {
                _acceptableValuesNames = value;
            }
        }
        public string this[object value]
        {
            get
            {
                try
                {
                    return _acceptableValuesNames[value];
                }
                catch
                {
                    throw new Exception($"Non acceptable value({value.ToString()}) in MetricType: {_name}");
                    return "unknown";
                }
            }
        }


      

        public void Save(string filename)
        {
            
            using (StreamWriter sw = new StreamWriter(filename))
            {
                Save(sw);
            }
        }
        public void Save(StreamWriter sw)
        {
            sw.WriteLine(JsonSerializer.Serialize(_name));
            sw.WriteLine(JsonSerializer.Serialize(_description));
            sw.WriteLine(JsonSerializer.Serialize(_valueType.ToString()));
            sw.WriteLine(JsonSerializer.Serialize(_isCheckable));
            sw.WriteLine(JsonSerializer.Serialize(ObjectArrayToString(_acceptableValuesNames.Keys.ToArray())));
            sw.WriteLine(JsonSerializer.Serialize(_acceptableValuesNames.Values));
        }
        public static MetricType Load(string filename)
        {
            using (StreamReader sr = new StreamReader(filename)) 
            {
                return Load(sr);
            }

        }
        public static MetricType Load(StreamReader sr)
        {
            string name, description;
            Type type;
            bool isCheckable;
            string[] values, keys;
            name = JsonSerializer.Deserialize<string>(sr.ReadLine());
            description = JsonSerializer.Deserialize<string>(sr.ReadLine());
            type = Type.GetType(JsonSerializer.Deserialize<string>(sr.ReadLine()));
            isCheckable = JsonSerializer.Deserialize<bool>(sr.ReadLine());
            keys = JsonSerializer.Deserialize<string[]>(sr.ReadLine());
            values = JsonSerializer.Deserialize<string[]>(sr.ReadLine());
            Dictionary<object, string> _values = new Dictionary<object, string>();
            for (int i = 0; i < keys.Length; ++i)
            {
                _values.Add((object)JsonSerializer.Deserialize(keys[i], type), values[i]);
            }
            return new MetricType(name, description, type, _values, isCheckable);
        }
    
        public bool isAcceptableValue(object value)
        {
            return _isCheckable || _acceptableValuesNames.ContainsKey(value);
        }
        public static bool operator ==(MetricType l, MetricType r)
        {
            bool result = l._name == r._name && l._description == r._description && l._valueType == r._valueType;
            return result;
        }
        public static bool operator !=(MetricType l, MetricType r)
        {
            return !(l == r);
        }

        public override string ToString()
        {
            string res = _name + ": " + _description;
            return res;
        }

        private string[] ObjectArrayToString(Object[] array)
        {
            string[] res = new string[array.Length];
            for(int i= 0;i <array.Length;i++)
            {
                res[i] = array[i].ToString();
            }
            return res;
        }
    }
    public class IntegerMetricType : MetricType
    {
        public IntegerMetricType() : base("unkown", "unknown", typeof(int), new Dictionary<object, string>(), true)
        {

        }
        public IntegerMetricType(string name = "unknown", string desctiprion = "unknown", Dictionary<object, string> acceptableValues = null) : base(name, desctiprion, typeof(int), acceptableValues, true)
        {

        }
        public static IntegerMetricType createIntegerMetricType(string name, string description, int[] acceptableValues, string[] names)
        {
            if (acceptableValues.Length < 1) throw new Exception($"No elements in acceptable values in metric type {name}");
            if (acceptableValues.Length != names.Length) throw new Exception($"Number of values does not match the number of names in metric type {name}");
            Dictionary<object, string> dict = new Dictionary<object, string>();
            for (int i = 0; i < acceptableValues.Length; ++i)
            {
                dict.Add(acceptableValues[i], names[i]);
            }
            return new IntegerMetricType(name, description, dict);
        }
        public static IntegerMetricType createIntegerMetricType(string name, string description, int[] acceptableValues)
        {
            if (acceptableValues.Length < 1) throw new Exception($"No elements in acceptable values in metric type {name}");
            string[] names = new string[acceptableValues.Length];
            for (int i = 0; i < acceptableValues.Length; ++i)
            {
                names[i] = acceptableValues[i].ToString();
            }
            Dictionary<object, string> dict = new Dictionary<object, string>();
            for (int i = 0; i < acceptableValues.Length; ++i)
            {
                dict.Add(acceptableValues[i], names[i]);
            }
            return new IntegerMetricType(name, description, dict);
        }



    }
    public class PointMetricType : MetricType
    {
        public PointMetricType(string name = "unknown", string desctiprion = "unknown") : base(name, desctiprion, typeof(Point), null, false) { }
        public static PointMetricType createPointMetricType(string name, string description)
        {
            return new PointMetricType(name, description);
        }
    }
}
