using MoonSharp.Interpreter;
using MoonSharp.RemoteDebugger;
using System;
using System.Diagnostics;
using Your_Desktop_Pet.Core.Drawing;
using Your_Desktop_Pet.Core.Lua;

namespace Your_Desktop_Pet.Core.Pet
{
    class PetObject : Sprite
    {
        public float frameCap = 60f;
        public float animationFrameRate = 12f;

        public float currentTime = 0;
        public float animationTime = 0;
        public float updateInterval = 1.0f / 60f;
        public float animationInterval = 60f / 12;
        public float totalTime = 0;

        public bool ready
        {
            get { return _luaHandler != null && _luaHandler.ready; }
        }

        private RemoteDebuggerService remoteDebugger;
        private PetLuaHandler _luaHandler = null;
        private Animator _animator = null;
        private Table _petTable = null;
        private string _baseDirectory = "";

        // Jesus christ, what the fuck am I doing
        public PetObject(string baseDirectory) : base(baseDirectory + "\\Sprites", new Ini.IniFile(baseDirectory + "\\pet.ini").IniReadValue("PetSettings", "DefaultSprite"))
        {
            _baseDirectory = baseDirectory;
            _animator = new Animator(this, _baseDirectory + "\\sprites");
            components ^= LuaObjectComponents.PetObject;
        }

        public void Start()
        {
            Ini.IniFile petFile = new Ini.IniFile(_baseDirectory + "\\pet.ini");

            frameCap = Convert.ToSingle(petFile.IniReadValue("PetSettings", "UpdateCallInterval"));
            animationFrameRate = Convert.ToSingle(petFile.IniReadValue("PetSettings", "AnimationFrameRate"));
            anchor = (AnchorPoint)Enum.Parse(typeof(AnchorPoint), petFile.IniReadValue("PetSettings", "Anchor"));
            window.scaleFactor = Convert.ToSingle(petFile.IniReadValue("PetSettings", "SpriteScaleFactor"));

            name = petFile.IniReadValue("PetInfo", "Name");
            SetSprite(petFile.IniReadValue("PetSettings", "DefaultSprite"));
            window.ChangeSize(_size.X / window.scaleFactor, _size.Y / window.scaleFactor);

            _luaHandler = new Lua.PetLuaHandler();

            if (Globals.luaTraceback)
            {
                _luaHandler.lua.DebuggerEnabled = true;
                ActivateRemoteDebugger(_luaHandler.lua);
            }

            _petTable = _luaHandler.lua.Globals.Get("pet").Table;
            _petTable["AABB"] = Lua.LuaHelper.RectToTable(window.Bounds, _luaHandler.lua);

            updateInterval = 1.0f / frameCap;
            animationInterval = frameCap / animationFrameRate;

            _luaHandler.lua.DoFile(_baseDirectory + @"\Scripts\pet.lua");
            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Start"]);

            if ((bool)_petTable["show"])
                Show();

            _animator.FlipSprite(Convert.ToBoolean(_petTable["flipX"]));
            SetPosition(Convert.ToSingle(_petTable["x"]), Convert.ToSingle(_petTable["y"]));
        }

        public void Update()
        {
            if (!_luaHandler.ready || _petTable == null)
                return;

            _petTable["AABB"] = Lua.LuaHelper.RectToTable(window.Bounds, _luaHandler.lua);

            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Update"]);
            _luaHandler.lua.Call(_luaHandler.lua.Globals["_LateUpdate"]);

            if ((bool)_petTable["show"] == true)
                Show();
            else
                Hide();

            if (shouldHalt)
                return;

            _animator.FlipSprite(Convert.ToBoolean(_petTable["flipX"]));
            SetPosition(Convert.ToSingle(_petTable["x"]), Convert.ToSingle(_petTable["y"]));
        }

        public void Draw()
        {
            if (shouldHalt || !_luaHandler.ready)
                return;

            _luaHandler.lua.Call(_luaHandler.lua.Globals["_Draw"]);

            string anim = (string)_petTable["animation"];
            if (!string.IsNullOrEmpty(anim) && anim != _animator.currentAnimation)
            {
                _animator.ChangeAnimation(anim);
                SetPosition(Convert.ToSingle(_petTable["x"]), Convert.ToSingle(_petTable["y"]));
            }

            _animator.Tick();
        }

        private void ActivateRemoteDebugger(Script script)
        {
            if (remoteDebugger == null)
            {
                remoteDebugger = new RemoteDebuggerService();

                remoteDebugger.Attach(script, "Description of the script", false);
            }

            Process.Start(remoteDebugger.HttpUrlStringLocalHost);
        }

        public Forms.SDK.PetDebugInfo GetPetDebugInfo()
        {
            return new Forms.SDK.PetDebugInfo(GetObjectDebugInfo(), _animator.GetAnimatorDebugInfo());
        }

        public void Stop()
        {
            window.Close();
        }

        public override void Dispose()
        {
            _animator = null;
            window.Dispose();

            base.Dispose();
        }
    }
}
