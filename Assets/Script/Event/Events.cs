using System;
using System.Reflection;
using UnityEngine;

namespace LockStep
{
    public partial class Events
    {
        // �������Ҫ�����Ǻ���Ķ��ľͲ���ʵ�����ˣ�ֱ���ã�
        static Events()
        {
            FieldInfo[] fields = typeof(Events).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                // IsSubclassOf ����Ƿ�������������
                if (field.FieldType.IsSubclassOf(typeof(IEvent)))
                {
                    // ����ķ�������ֵ��������õĶ���Ϊ��̬�ĵ�һ����������Ϊnull
                    // Activator.CreateInstance ʵ��������,field.FieldType���������
                    //Debug.Log(field.Name);
                    field.SetValue(null, Activator.CreateInstance(field.FieldType, field.Name));
                }
            }
        }
    }
}
