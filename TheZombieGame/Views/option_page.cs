using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheZombieGame
{
    public partial class option_page : Form
    {
        public option_page()
        {
            InitializeComponent();
        }

        private void option_page_Load(object sender, EventArgs e)
        {

        }

        private void btn_setting_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string vkUrl = "https://vk.com/groups"; // Замените на ссылку вашего профиля

            try
            {
                // Открываем ссылку в браузере по умолчанию
                Process.Start(new ProcessStartInfo
                {
                    FileName = vkUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Обработка ошибки, если не удалось открыть ссылку
                MessageBox.Show("Не удалось открыть ссылку. Пожалуйста, проверьте ваше подключение к Интернету и попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}