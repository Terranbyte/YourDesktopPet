using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Pet
{
    public class LuaObjectManager
    {
        public static LuaObjectManager Current => _current.Value;

        private List<LuaObject> objects = new List<LuaObject>();
        private static Lazy<LuaObjectManager> _current = new Lazy<LuaObjectManager>();

        private void OnObjectDestroy(LuaObject o)
        {
            objects.Remove(o);
            Helpers.Log.WriteLine("LuaObjectManager", $"Destroying object \"{o.name}\" ({o.GetGUID()})");
        }

        public string AddObject<T>(T obj) where T : LuaObject
        {
            obj.onObjectDestroy += () => OnObjectDestroy(obj);
            objects.Add(obj);
            return obj.GetGUID();
        }

        public void DeleteObject<T>(T obj) where T : LuaObject
        {
            obj.Dispose();
            objects.Remove(obj);
        }

        public void DeleteObject(string guid)
        {
            objects.Remove(GetObjectFromGUID<LuaObject>(guid));
        }

        public T GetObjectFromGUID<T>(string guid) where T : LuaObject
        {
            return (T)objects.Find(x => x.GetGUID() == guid);
        }

        public List<T> GetObjectsOfType<T>() where T : LuaObject
        {
            return objects.Where(x => x is T).Cast<T>().ToList();
        }

        public int GetObjectCount()
        {
            return objects.Count;
        }
    }
}
