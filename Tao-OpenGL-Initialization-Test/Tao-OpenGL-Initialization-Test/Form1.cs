using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//------------------------------------------------------
// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGLUT
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
//------------------------------------------------------

namespace Tao_OpenGL_Initialization_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация Glut
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // Отчитка окна
            Gl.glClearColor(255, 255, 255, 1);

            // Установка порта вывода в соответствии с размерам элемента AnT
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // Настройка проекции
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            // Настройка параметров OpenGL для визуализации
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();
            Gl.glColor3f(1.0f, 0, 0);

            Gl.glPushMatrix();
            Gl.glTranslated(0, 0, -6);
            Gl.glRotated(45, 1, 1, 0);

            // Рисуем сферу с помощью библиотеки FreeGLUT
            Glut.glutWireSphere(2, 32, 32);

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }
    }
}
