using MoonSharp.Interpreter;
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

        private Lua.PetLuaHandler _luaHandler = null;
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
            _sprite.offset = (PositionOffset)Enum.Parse(typeof(PositionOffset), petFile.IniReadValue("PetSettings", "Offset"));
            _sprite.window.scaleFactor = Convert.ToSingle(petFile.IniReadValue("PetSettings", "SpriteScaleFactor"));

            _luaHandler = new Lua.PetLuaHandler();

            Table petTable = _luaHandler.lua.Globals.Get("pet").Table;
            petTable["bounds"] = Lua.LuaHelper.RectToTable(_sprite.window.Bounds, _luaHandler.lua);

            _sprite.animator = new Animator(ref _sprite.window, _baseDirectory + "\\sprites");

            updateInterval = 1.0f / frameCap;
            animationInterval = frameCap / animationFrameRate;

            _luaHandler.lua.DoFile(_baseDirectory + @"\Scripts\pet.lua");
            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Start"]);

            if ((bool)petTable["show"])
                _sprite.Show();
        }

        public void Stop()
        {
            //_sprite.Stop();                                                                                
        }

        public void Update()
        {
            Table petTable = _luaHandler.lua.Globals.Get("pet").Table;
            petTable["bounds"] = Lua.LuaHelper.RectToTable(_sprite.window.Bounds, _luaHandler.lua);

            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Update"]);
            _luaHandler.lua.Call(_luaHandler.lua.Globals["_LateUpdate"]);

            if ((bool)petTable["show"] == true)
                _sprite.Show();
            else
                _sprite.Hide();

            if (_sprite.shouldHalt)
                return;

           _sprite.animator.FlipSprite(Convert.ToBoolean(petTable["flipX"]));

            _sprite.SetPosition(Convert.ToSingle(petTable["x"]), Convert.ToSingle(petTable["y"]));
        }

        public void Draw()
        {
            if (_sprite.shouldHalt)
                return;

            Table petTable = _luaHandler.lua.Globals.Get("pet").Table;

            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Draw"]);

            string anim = (string)petTable["animation"];
            if (!string.IsNullOrEmpty(anim) && anim != _sprite.animator.currentAnimation)
            {
                _sprite.animator.ChangeAnimation(anim);
                _sprite.SetPosition(Convert.ToSingle(petTable["x"]), Convert.ToSingle(petTable["y"]));
            }

            _sprite.animator.Tick();
        }
    }
}
