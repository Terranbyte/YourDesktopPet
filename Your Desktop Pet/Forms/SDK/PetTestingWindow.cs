using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Your_Desktop_Pet.Forms.SDK
{
    public partial class PetTestingWindow : Form
    {
        private string _petPath;
        private Core.Pet.PetObject _pet;
        private Queue<float> fpsSamples = new Queue<float>();

        public PetTestingWindow(string path)
        {
            InitializeComponent();
            _petPath = path;

            for (int i = 0; i < 5; i++)
            {
                fpsSamples.Enqueue(1f / 60f);
            }
        }

        private void PetTestWindow_Load(object sender, EventArgs e)
        {
            SpawnPet();
        }

        private void SpawnPet()
        {
            if (_pet != null)
            {
                _pet.Stop();
                _pet.Dispose();
            }

            _pet = new Core.Pet.PetObject(_petPath);
            Core.Pet.LuaObjectManager.Current.AddObject(_pet);
            _pet.Start();
        }

        private void UpdateDebugInfo()
        {
            PetDebugInfo info = _pet.GetDebugInfo();

            pos_x.Text = info.position.X.ToString();
            pos_y.Text = info.position.Y.ToString();
            size_x.Text = info.size.X.ToString();
            size_y.Text = info.size.Y.ToString();
            curr_anim.Text = info.animatorInfo.currentAnimation;
            num_frames.Text = info.animatorInfo.numFrames.ToString();
            anim_index.Text = info.animatorInfo.currentFrame.ToString();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Core.Helpers.Time.Update();
            Core.API.Input.InputProvider.Update();

            fpsSamples.Dequeue();
            fpsSamples.Enqueue(Core.Helpers.Time.deltaTime);

            Console.Title = $"FPS: {Math.Round(1f / fpsSamples.Average())}";

            if (_pet != null && _pet.ready)
            {
                if (_pet.shouldExit)
                {
                    _pet = null;
                    return;
                }

                _pet.currentTime += Core.Helpers.Time.deltaTime;
                _pet.totalTime += Core.Helpers.Time.deltaTime;

                if (_pet.currentTime < _pet.updateInterval)
                    return;

                _pet.animationTime += 1;
                _pet.Update();

                UpdateDebugInfo();

                if (_pet.animationTime >= _pet.animationInterval)
                {
                    _pet.Draw();
                    _pet.animationTime -= _pet.animationInterval;
                }

                _pet.currentTime -= _pet.updateInterval;
            }
        }

        private void btn_respawn_Click(object sender, EventArgs e)
        {
            SpawnPet();
        }

        private void PetTestingWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pet != null)
            {
                _pet.Stop();
                _pet.Dispose();
            }
        }
    }
}
