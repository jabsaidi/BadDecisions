using System;
using System.IO;
using System.Text;
using System.Reflection;
using TestAPI.DLA.Model;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TestAPI.DLA.Repository
{
    public class Persistor<T>
            where T : BaseEntity, new()
    {
        private string _fileName;
        private static ConcurrentDictionary<Type, PropertyInfo[]> _dic = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public Persistor(string fileName)
        {
            _fileName = fileName;
        }

        public T Update(T objToUpdate)
        {
            DeSerialize(objToUpdate);

            string newLine = ToCsv(objToUpdate);
            int currLine = GetLineToModify(objToUpdate);

            ReplaceLine(currLine, newLine);
            return objToUpdate;
        }

        public bool Delete(long id)
        {
            T objToDelete = GetById(id);
            int lineToRemove = GetLineToModify(objToDelete);
            return DeleteLine(lineToRemove);
        }

        public T Create(T toCreate)
        {
            DeSerialize(toCreate);
            string toLog = ToCsv(toCreate);
            LogData(toLog);
            return toCreate;
        }

        public T GetById(long id)
        {
            T foundobject = new T();
            using (var sr = new StreamReader(_fileName))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var splitted = line.Split(";\t");
                    string identificator = splitted[splitted.Length - 1];

                    if (identificator == id.ToString())
                    {
                        foundobject = SerializeToObject(line, foundobject);
                        break;
                    }
                }
            }
            return foundobject;
        }

        public List<T> GetAll()
        {
            T obj = new T();
            List<T> list = new List<T>();
            string[] lines = File.ReadAllLines(_fileName);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                    continue;
                list.Add(SerializeToObject(lines[i], obj));
            }
            return list;
        }

        public T GetByDecision(string decision)
        {
            T foundobject = new T();
            using (var sr = new StreamReader(_fileName))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var splitted = line.Split(";");
                    string identificator = splitted[0];

                    if (identificator == decision)
                    {
                        foundobject = SerializeToObject(line, foundobject);
                        break;
                    }
                }
            }
            return foundobject;
        }

        public static PropertyInfo[] DeSerialize(T obj)
        {
            obj = new T();
            Type t = obj.GetType();

            if (_dic.ContainsKey(t))
                return _dic[t];
            else
            {
                PropertyInfo[] properties = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                _dic.TryAdd(t, properties);
                return _dic[t];
            }
        }

        public string ToCsv(T obj)
        {
            Type t = typeof(T);
            PropertyInfo[] properties = _dic[t];

            StringBuilder line = new StringBuilder();
            StringBuilder csvdata = new StringBuilder();

            foreach (var p in properties)
            {
                if (line.Length > 0)
                    line.Append(";\t");

                var x = p.GetValue(obj);

                if (x != null)
                    line.Append(x.ToString());
            }
            csvdata.AppendLine(line.ToString());
            return csvdata.ToString();
        }

        public void LogData(string row)
        {
            using (var sw = new StreamWriter(_fileName, true))
            {
                sw.WriteLine(row);
            }
        }

        public T SerializeToObject(string csv, T obj)
        {
            string[] csvValues = csv.Split(";");
            PropertyInfo[] properties = DeSerialize(obj);

            for (int i = 0; i < csvValues.Length - 1; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
                    string value = csvValues[i];
                    Type propType = properties[j].PropertyType;
                    var propValue = ConvertToType(propType, value);

                    properties[j].SetValue(obj, propValue);
                    i++;
                }
            }
            return obj;
        }

        private int GetLineToModify(T objToUpdate)
        {
            int currLine = 1;
            string line = string.Empty;

            using (var sr = new StreamReader(_fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var splitted = line.Split(";");
                    string identificator = splitted[splitted.Length - 1];
                    if (identificator == objToUpdate.Id.ToString())
                        break;
                    currLine++;
                }
            }
            return currLine;
        }

        public void ReplaceLine(int toReplace, string newLine)
        {
            string[] lines = File.ReadAllLines(_fileName);

            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                {
                    if (currentLine == toReplace)
                        sw.WriteLine(newLine);
                    else
                        sw.WriteLine(lines[currentLine - 1]);
                }
            }
        }

        private bool DeleteLine(int lineToRemove)
        {
            bool deleted = false;
            string[] lines = File.ReadAllLines(_fileName);

            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                for (int i = 1; i <= lines.Length; ++i)
                {
                    if (i == lineToRemove)
                        deleted = true;
                    else
                        sw.WriteLine(lines[i - 1]);
                }
            }
            return deleted;
        }

        public object ConvertToType(Type propType, string value)
        {
            if (propType == typeof(long))
                return (long)Convert.ChangeType(value, typeof(long));

            else if (propType == typeof(int))
                return (int)Convert.ChangeType(value, typeof(int));

            else if (propType == typeof(short))
                return (short)Convert.ChangeType(value, typeof(short));

            else if (propType == typeof(DateTime))
                return (DateTime)Convert.ChangeType(value, typeof(DateTime));

            else if (propType == typeof(bool))
                return (bool)Convert.ChangeType(value, typeof(bool));

            else if (propType == typeof(float))
                return (float)Convert.ChangeType(value, typeof(float));

            else if (propType == typeof(decimal))
                return (decimal)Convert.ChangeType(value, typeof(decimal));

            else if (propType == typeof(char))
                return (char)Convert.ChangeType(value, typeof(char));

            else
                return value;
        }
    }
}