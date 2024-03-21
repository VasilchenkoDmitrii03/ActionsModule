using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ActionsLib.ActionTypes
{
    public class ActionsMetricTypes
    {
        Dictionary<VolleyActionType, MetricTypeList> _data;
        string _name;
        public ActionsMetricTypes(string name)
        {
            _data = new Dictionary<VolleyActionType, MetricTypeList>();
            _name = name;
            VolleyActionType[] keys = new VolleyActionType[] { VolleyActionType.Serve, VolleyActionType.Reception, VolleyActionType.Set, VolleyActionType.Attack, VolleyActionType.Block, VolleyActionType.Defence };
            foreach(VolleyActionType key in keys)
            {
                _data.Add(key, new MetricTypeList(key.ToString()));
            }
        }
        public void updateList(VolleyActionType type, MetricTypeList mtypelist)
        {
            _data[type] = new MetricTypeList(type.ToString());
            foreach(MetricType mt in  mtypelist)
            {
                _data[type].Add(mt);
            }
        }
        public VolleyActionType[] Keys
        {
            get { return _data.Keys.ToArray(); }
        }
        public MetricTypeList this[VolleyActionType type]
        {
            get
            {
                return _data[type];
            }
        }
    }
}
