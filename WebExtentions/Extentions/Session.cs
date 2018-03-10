using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace WebExtentions.Extentions
{
    public static class Session
    {
        public static object GetObject(this ISession session, string key)
        {
            byte[] data = session.Get(key);
            if (data == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Position = 0;
                object obj = bf.Deserialize(ms);
                ms.Close();
                return obj;
            }
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            return (T)GetObject(session, key);
        }

        public static void SetObject(this ISession session, string key, object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                byte[] data = ms.ToArray();
                ms.Close();
                session.Set(key, data);
            }
        }
    }
}