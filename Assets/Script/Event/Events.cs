using System;
using System.Reflection;
using UnityEngine;

namespace LockStep
{
    public partial class Events
    {
        // 这个的主要功能是后面的订阅就不用实例化了，直接用！
        static Events()
        {
            FieldInfo[] fields = typeof(Events).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                // IsSubclassOf 检测是否是这个类的子类
                if (field.FieldType.IsSubclassOf(typeof(IEvent)))
                {
                    // 反射的方法设置值，如果设置的对象为静态的第一个参数设置为null
                    // Activator.CreateInstance 实例化对象,field.FieldType对象的类型
                    //Debug.Log(field.Name);
                    field.SetValue(null, Activator.CreateInstance(field.FieldType, field.Name));
                }
            }
        }
    }
}
