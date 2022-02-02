using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Your_Desktop_Pet.Forms
{
    // Taken from https://stackoverflow.com/questions/9462592/best-practices-for-multi-form-applications-to-show-and-hide-forms

    public class FormManager : ApplicationContext
    {
        private List<Form> forms = new List<Form>();

        private void onFormClosed(object sender, EventArgs e)
        {
            forms.Remove((Form)sender);
            Core.Helpers.Log.WriteLine("Form Manager", $"Window closed: {((Form)sender).Name}");

            if (!forms.Any())
            {
                ExitThread();
            }
        }

        public void RegisterForm(Form frm)
        {
            frm.FormClosed += onFormClosed;
            forms.Add(frm);
        }

        public T CreateForm<T>() where T : Form, new()
        {
            var ret = new T();
            RegisterForm(ret);
            return ret;
        }

        private static Lazy<FormManager> _current = new Lazy<FormManager>();
        public static FormManager Current => _current.Value;
    }
}
