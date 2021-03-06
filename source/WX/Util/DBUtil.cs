﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WX.Util {
    public class DBUtil {
        /**
         * DataRow 转 实体类
         * model: 实体类
         * row: DataRow
         * throwErr: 无法匹配数据类型时是否抛出异常
         * @Return: 实体类
         * @Time: 2020.1.2
         */
        public static T data2Model<T>(T model, DataRow row, bool throwErr = true) {
            // 通过反射获取对象的属性
            PropertyInfo[] list = model.GetType().GetProperties();
            foreach(var item in list) {
                // 属性名
                string name = item.Name;
                // 数据类型
                string type = item.PropertyType?.Name??"null";
                switch(type) {
                    case "string":
                    case "String": {
                        try {
                            item.SetValue(model, row[name]?.ToString()??"");
                        } catch(Exception ex) {
                            Console.WriteLine($"DBUtil: {ex.Message}");
                            item.SetValue(model, null);
                        }
                        break;
                    }
                    case "int":
                    case "int32":
                    case "Int32": {
                        try {
                            item.SetValue(model, Convert.ToInt32(row[name]));
                        } catch(Exception ex) {
                            Console.WriteLine($"DBUtil: {ex.Message}");
                            item.SetValue(model, 0);
                        }
                        break;
                    }
                    case "DateTime": {
                        try {
                            item.SetValue(model, row[name]);
                        } catch(Exception ex) {
                            Console.WriteLine($"DBUtil: {ex.Message}");
                            item.SetValue(model, null);
                        }
                        break;
                    }
                    default: {
                        Console.WriteLine($"{name}: {type}");
                        if(throwErr) {
                            throw new Exception("无法匹配对应类型");
                        }
                        item.SetValue(model, null);
                        break;
                    }
                }
            }

            return model;
        }

        /**
         * DataTable 转 实体类列表
         * model: 实体类列表
         * table: DataTable
         * throwErr: 无法匹配数据类型时是否抛出异常
         * @Return: 实体类列表
         * @Time: 2020.1.2
         */
        public static List<T> data2List<T>(List<T> lis, DataTable table, bool throwErr = true) {
            DataRowCollection rows = table.Rows;
            foreach(DataRow row in rows) {
                // 创建泛型对象
                T newT = Activator.CreateInstance<T>();
                lis.Add(data2Model<T>(newT, row, throwErr));
            }
            return lis;
        }

    }
}
