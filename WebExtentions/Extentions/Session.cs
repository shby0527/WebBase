using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MessagePack;
using MessagePack.Resolvers;

namespace WebExtentions.Extentions
{
    public static class Session
    {
        public static object GetObjectUseBinary(this ISession session, string key)
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

        public static T GetObjectUseBinary<T>(this ISession session, string key)
        {
            return (T)GetObjectUseBinary(session, key);
        }

        public static void SetObjectUseBinary(this ISession session, string key, object obj)
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

        public static object GetObjectUseJson(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonConvert.DeserializeObject(json);
        }

        public static T GetObjectUseJson<T>(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetObjectUseJson(this ISession session, string key, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            session.SetString(key, json);
        }


        public static void SetObjectUseMsgPack<T>(this ISession session, string key, T obj)
        {
            session.Set(key, LZ4MessagePackSerializer.Serialize(obj, ContractlessStandardResolver.Instance));
        }

        public static T GetObjectUseMsgPack<T>(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
                return default(T);
            return LZ4MessagePackSerializer.Deserialize<T>(data, ContractlessStandardResolver.Instance);
        }
    }
}