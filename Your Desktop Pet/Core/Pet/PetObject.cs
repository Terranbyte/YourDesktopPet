using NLua;
using System;

namespace Your_Desktop_Pet.Core.Pet
{
    class PetObject
    {
        public bool ShouldExit
        {
            get { return _sprite.ShouldExit; }
            private set {  _sprite.ShouldExit = value; }
        }


        private PetLuaHandler _luaHandler = null;
        public Drawing.Sprite _sprite = null;
        private string _baseDirectory = "";

        public PetObject(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        ~PetObject()
        {
            Stop();
        }

        public void Start()
        {
            _sprite = new Drawing.Sprite(_baseDirectory + "\\sprites", Globals.Offset, true);
            _sprite.Hide();
            _sprite.Offset = Globals.Offset;
            _luaHandler = new PetLuaHandler();

            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            petObject["bounds"] = _sprite.Window.Bounds;
            _luaHandler.lua.SetObjectToPath("pet", petObject);

            if (Globals.LuaTraceback)
            {
                _luaHandler.lua.UseTraceback = true;
                _luaHandler.lua.SetDebugHook(KeraLua.LuaHookMask.Line, 1);
                _luaHandler.lua.DebugHook += Lua_DebugHook;
            }

            _luaHandler.lua.DoFile(_baseDirectory + @"\Scripts\pet.lua");
            _luaHandler.lua.GetFunction("_Start").Call();
        }

        public void Stop()
        {
            _sprite.Hide();
            _sprite.Window.Dispose();
        }

        public void Update()
        {
            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            petObject["bounds"] = _sprite.Window.Bounds;

            _luaHandler.lua.GetFunction("_Update").Call();
            _luaHandler.lua.GetFunction("_LateUpdate").Call();

            //Apply changes

            if ((bool)petObject["show"] == true)
                _sprite.Show();
            else
                _sprite.Hide();

            if (_sprite.ShouldHalt)
                return;

           _sprite.Animator.FlipSprite(Convert.ToBoolean(petObject["flipX"]));

            _sprite.SetPosition(Convert.ToSingle(petObject["x"]), Convert.ToSingle(petObject["y"]));
        }

        public void Draw()
        {
            if (_sprite.ShouldHalt)
                return;

            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            
            _luaHandler.lua.GetFunction("_Draw").Call();

            string anim = (string)petObject["animation"];
            if (!string.IsNullOrEmpty(anim) && anim != _sprite.Animator.CurrentAnimation)
            {
                _sprite.Animator.ChangeAnimation(anim);
                // Update position in case the size changed
                _sprite.SetPosition(Convert.ToSingle(petObject["x"]), Convert.ToSingle(petObject["y"]));
            }

            _sprite.Animator.Tick();
        }

        private void Lua_DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        {
            Helpers.Log.WriteLine("LuaTraceback", _luaHandler.lua.GetDebugTraceback());
        }
    }
}
