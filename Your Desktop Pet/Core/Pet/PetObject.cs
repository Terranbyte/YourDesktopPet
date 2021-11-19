using NLua;
using System;
using Your_Desktop_Pet.Core.Drawing;

namespace Your_Desktop_Pet.Core.Pet
{
    class PetObject
    {
        public float frameCap = 60f;
        public float animationFrameRate = 12f;

        public float currentTime = 0;
        public float animationTime = 0;
        public float updateInterval = 1.0f / 60f;
        public float animationInterval = 60f / 12;
        public float totalTime = 0;

        public bool shouldExit
        {
            get { return _sprite.shouldExit; }
            private set {  _sprite.shouldExit = value; }
        }

        private PetLuaHandler _luaHandler = null;
        public Sprite _sprite = null;
        private string _baseDirectory = "";

        public PetObject(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
            _sprite = new Sprite(_baseDirectory + "\\sprites");
            _sprite.Hide();
        }

        public void Start()
        {
            Ini.IniFile petFile = new Ini.IniFile(_baseDirectory + "\\pet.ini");

            frameCap = Convert.ToSingle(petFile.IniReadValue("PetSettings", "UpdateCallInterval"));
            animationFrameRate = Convert.ToSingle(petFile.IniReadValue("PetSettings", "AnimationFrameRate"));
            _sprite.offset = (PositionOffset)Enum.Parse(typeof(Core.Drawing.PositionOffset), petFile.IniReadValue("PetSettings", "Offset"));
            _sprite.window.scaleFactor = Convert.ToSingle(petFile.IniReadValue("PetSettings", "SpriteScaleFactor"));

            _luaHandler = new PetLuaHandler();

            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            petObject["bounds"] = _sprite.window.Bounds;
            _luaHandler.lua.SetObjectToPath("pet", petObject);

            _sprite.animator = new Animator(ref _sprite.window, _baseDirectory + "\\sprites");

            updateInterval = 1.0f / frameCap;
            animationInterval = frameCap / animationFrameRate;

            if (Globals.luaTraceback)
            {
                _luaHandler.lua.UseTraceback = true;
                _luaHandler.lua.SetDebugHook(KeraLua.LuaHookMask.Line, 1);
                _luaHandler.lua.DebugHook += Lua_DebugHook;
            }

            _luaHandler.lua.DoFile(_baseDirectory + @"\Scripts\pet.lua");
            _luaHandler.lua.GetFunction("_Start").Call();
            Console.WriteLine((bool)petObject["show"]);
            if ((bool)petObject["show"])
                _sprite.Show();
        }

        public void Stop()
        {
            _sprite.Hide();
            _sprite.window.Dispose();
        }

        public void Update()
        {
            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            petObject["bounds"] = _sprite.window.Bounds;

            _luaHandler.lua.GetFunction("_Update").Call();
            _luaHandler.lua.GetFunction("_LateUpdate").Call();

            //Apply changes

            if ((bool)petObject["show"] == true)
                _sprite.Show();
            else
                _sprite.Hide();

            if (_sprite.shouldHalt)
                return;

           _sprite.animator.FlipSprite(Convert.ToBoolean(petObject["flipX"]));

            _sprite.SetPosition(Convert.ToSingle(petObject["x"]), Convert.ToSingle(petObject["y"]));
        }

        public void Draw()
        {
            if (_sprite.shouldHalt)
                return;

            LuaTable petObject = _luaHandler.lua.GetTable("pet");
            
            _luaHandler.lua.GetFunction("_Draw").Call();

            string anim = (string)petObject["animation"];
            if (!string.IsNullOrEmpty(anim) && anim != _sprite.animator.currentAnimation)
            {
                _sprite.animator.ChangeAnimation(anim);
                // Update position in case the size changed
                _sprite.SetPosition(Convert.ToSingle(petObject["x"]), Convert.ToSingle(petObject["y"]));
            }

            _sprite.animator.Tick();
        }

        private void Lua_DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        {
            Helpers.Log.WriteLine("LuaTraceback", _luaHandler.lua.GetDebugTraceback());
        }
    }
}
