using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_1
{
	public partial class Form1 : Form
	{
		readonly int[] Temp_x = new int[3], Temp_y = new int[3], Temp_z = new int[3];
		double[] Light_Koef;
		double x_light, y_light, z_light;
		readonly Z_Buf[] Z_Buffer;
		int Checked = -1;
		double[,] Arr;
		double[,] originalArray;
		int xi, yi;
		bool ZBuff, Roberts, BMP;
		readonly Graphics g;
		readonly List<Cords> Draw_Sides = new List<Cords>();
		readonly List<double> zs = new List<double>();
		List<double> heights = new List<double>();
		//readonly Bitmap bmp;
		//Bitmap Newbmp;
		readonly List<List<Vertic>> Vertics = new List<List<Vertic>>();
		readonly List<List<Vertic>> Normals = new List<List<Vertic>>();
		readonly List<List<Cords>> Sides = new List<List<Cords>>();
		readonly List<Vertic> temp_Vertics = new List<Vertic>();
		readonly List<Vertic> temp_Normals = new List<Vertic>();
		readonly List<Cords> temp_Sides = new List<Cords>();

		public Form1()
		{
			InitializeComponent();
			Z_Buffer = new Z_Buf[Canvas.Width];
			g = Canvas.CreateGraphics();
			for (int i = 0; i < 3; i++)
			{
				Vertics.Add(new List<Vertic>());
				Normals.Add(new List<Vertic>());
				Sides.Add(new List<Cords>());
			}
            g.TranslateTransform(0, Canvas.Height);
            g.ScaleTransform(1, -1);
            zs.Sort();
			Reader();
		}
		void Draw_BMP()
		{
			Bitmap bmp = new Bitmap("a1b.bmp");
			Bitmap outputImage = new Bitmap(bmp.Width, bmp.Height);

			xi = bmp.Width;
			yi = bmp.Height;

			originalArray = new double[xi, yi];

			for (int i = 0; i < xi; i++)
			{
				for (int j = 0; j < yi; j++)
				{
					originalArray[i, j] = bmp.GetPixel(i, j).GetBrightness();
				}
			}
			g.Clear(Color.Black);
			for (int i = 0; i < 1; i++)
			{
				for (int j = 0; j < xi; j++)
				{
					if (j + 1 < xi)
					{
						g.DrawLine(
							new Pen(Color.Yellow),
							j,
							(float)(originalArray[j, i] * 100),
							j + 1,
							(float)(originalArray[j + 1, i] * 100)
						);
					}
				}
			}
		}
		void Rotate_BMP(double angle_x, double angle_y)
		{
			g.Clear(Color.Black);
			double Cos_y = Math.Cos(angle_y * Math.PI / 180);
			double Sin_y = Math.Sin(angle_y * Math.PI / 180);
			double Cos_x = Math.Cos(angle_x * Math.PI / 180);
			double Sin_x = Math.Sin(angle_x * Math.PI / 180);
			float smeshenie = (float)(Cos_y * 320 - Sin_y * 320);
			float[,] New_Arr = new float[Canvas.Width, Canvas.Height];
			for (int i = 0; i < Canvas.Height; i++)
			{
				for (int j = 0; j < Canvas.Width; j++)
				{
					New_Arr[j, i] = (float)(Sin_x * i);
				}
			}
			for (int i = 0; i < yi; i++)
			{
				for (int j = 0; j < xi; j++)
				{
					float new_X = (float)(Cos_y * j - Sin_y * i) + (320 - smeshenie) + 180;
					float new_Z = (float)(Sin_x * i + Cos_x * originalArray[j, i] * 100);
					if(new_X < Canvas.Width && new_X >= 0)
                    {
						New_Arr[(int)new_X, i] = new_Z;
					}
				}
			}
			float[] lower_z = new float[Canvas.Width], high_z = new float[Canvas.Width];
			for(int i = 0; i < Canvas.Width; i++)
            {
				high_z[i] = New_Arr[i, 0];
				lower_z[i] = New_Arr[i, 0];
				if (i + 1 < Canvas.Width)
				{
					g.DrawLine(new Pen(Color.Yellow), i, New_Arr[i, 0], i + 1, New_Arr[i + 1, 0]);
				}
			}
			for (int i = 0; i < Canvas.Height; i += 10)
			{
				for (int j = 0; j < Canvas.Width; j += 2)
				{
					if (j + 1 < Canvas.Width)
					{
						if (New_Arr[j, i] >= high_z[j])
                        {
							if(New_Arr[j + 1, i] > high_z[j + 1])
                            {
								g.DrawLine(new Pen(Color.Yellow), j, New_Arr[j, i], j + 1, New_Arr[j + 1, i]);
								high_z[j] = New_Arr[j, i];
								high_z[j + 1] = New_Arr[j + 1, i];
							}
							if(New_Arr[j + 1, i] < lower_z[j + 1])
                            {
								g.DrawLine(new Pen(Color.Yellow), j, New_Arr[j, i], j + 1, New_Arr[j + 1, i]);
								high_z[j] = New_Arr[j, i];
								lower_z[j + 1] = New_Arr[j + 1, i];
							}
						}
						else if(New_Arr[j, i] <= lower_z[j])
                        {
							if (New_Arr[j + 1, i] > high_z[j + 1])
							{
								g.DrawLine(new Pen(Color.Yellow), j, New_Arr[j, i], j + 1, New_Arr[j + 1, i]);
								lower_z[j] = New_Arr[j, i];
								high_z[j + 1] = New_Arr[j + 1, i];
							}
							if (New_Arr[j + 1, i] < lower_z[j + 1])
							{
								g.DrawLine(new Pen(Color.Yellow), j, New_Arr[j, i], j + 1, New_Arr[j + 1, i]);
								lower_z[j] = New_Arr[j, i];
								lower_z[j + 1] = New_Arr[j + 1, i];
							}
						}
					}
				}
			}
		}
		
		async void Reader()
        {
			StreamReader fstream_isac = new StreamReader("icaso.obj");
			StreamReader fstream_dode = new StreamReader("dodeca.obj");
			StreamReader fstream_cube = new StreamReader("Cube.obj");
			StreamReader[] fstream = new StreamReader[3] { fstream_isac, fstream_dode, fstream_cube };
			string str, X, Y, Z, normal = "0";
			List<string> verts = new List<string>();
			for(int i = 0; i < fstream.Length; i++)
            {
				while ((str = Convert.ToString(await fstream[i].ReadLineAsync())) != null)
                {
					verts.Clear();
					normal = "0";
					if (str[0] != 'v' && str[0] != 'f') continue;
					if (str[0] == 'v')
                    {
						X = "";
						Y = "";
						Z = "";
						if (str[1] == ' ')
                        {
							for (int g = 2, f = 0; g < str.Length; g++)
							{
								if (str[g] == ' ') f++;
								switch (f)
								{
									case 0:
										X += str[g];
										break;
									case 1:
										Y += str[g];
										break;
									case 2:
										Z += str[g];
										break;
								}
							}
							temp_Vertics.Add(new Vertic(Convert.ToDouble(X), Convert.ToDouble(Y), Convert.ToDouble(Z)));
						}
						else
                        {
							for (int g = 3, f = 0; g < str.Length; g++)
							{
								if (str[g] == ' ') f++;
								switch (f)
								{
									case 0:
										X += str[g];
										break;
									case 1:
										Y += str[g];
										break;
									case 2:
										Z += str[g];
										break;
								}
							}
							temp_Normals.Add(new Vertic(Convert.ToDouble(X), Convert.ToDouble(Y), Convert.ToDouble(Z)));
						}
					}
                    else
					{
						string[] date = str.Split(' ');
						for (int p = 1, a = 0; p < date.Length; p++, a++)
                        {
							string[] nums = date[p].Split('/');
							if(nums.Length > 1)
                            {
								normal = nums[2];
							}
							verts.Add(nums[0]);
						}
						int[] myInts = Array.ConvertAll(verts.ToArray(), int.Parse);
						temp_Sides.Add(new Cords(myInts, Convert.ToInt32(normal)));
					}
                }
				Vertics[i] = temp_Vertics.GetRange(0, temp_Vertics.Count);
				Normals[i] = temp_Normals.GetRange(0, temp_Normals.Count);
				Sides[i] = temp_Sides.GetRange(0, temp_Sides.Count);
				temp_Vertics.Clear();
				temp_Normals.Clear();
				temp_Sides.Clear();
			}			
		}
		Bitmap Reader_BMP()
		{
			using (FileStream fs = new FileStream("a1.bmp", FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return new Bitmap(fs);
			}
		}

		private void IcoCheck_CheckedChanged(object sender, EventArgs e)
        {
			if(IcoCheck.Checked == true)
            {
				DodCheck.Enabled = !DodCheck.Enabled;
				CubeCheck.Enabled = !CubeCheck.Enabled;
				Checked = 0;
			}
			else
            {
				DodCheck.Enabled = !DodCheck.Enabled;
				CubeCheck.Enabled = !CubeCheck.Enabled;
				Checked = -1;
			}
        }
        private void DodCheck_CheckedChanged(object sender, EventArgs e)
        {
			if (DodCheck.Checked == true)
			{
				IcoCheck.Enabled = !IcoCheck.Enabled;
				CubeCheck.Enabled = !CubeCheck.Enabled;
				Checked = 1;
			}
			else
			{
				IcoCheck.Enabled = !IcoCheck.Enabled;
				CubeCheck.Enabled = !CubeCheck.Enabled;
				Checked = -1;
			}
        }
        private void CubeCheck_CheckedChanged(object sender, EventArgs e)
        {
			if (CubeCheck.Checked == true)
			{
				DodCheck.Enabled = !DodCheck.Enabled;
				IcoCheck.Enabled = !IcoCheck.Enabled;
				Checked = 2;
			}
			else
			{
				DodCheck.Enabled = !DodCheck.Enabled;
				IcoCheck.Enabled = !IcoCheck.Enabled;
				Checked = -1;
			}
        }
        private void Draw_Click(object sender, EventArgs e)
        {
            g.Clear(BackColor);
			switch(Checked)
            {
				case -1:
					MessageBox.Show("Выберите фигуру");
					break;
				default:
					for (int i = 0; i < Sides[Checked].Count(); i++)
					{
						for (int h = 0; h < Sides[Checked][i].vertics.Length; h++)
						{
							if (h + 1 >= Sides[Checked][i].vertics.Length)
							{
								g.DrawLine(Pens.Green, (float)Vertics[Checked][Sides[Checked][i].vertics[h] - 1].x, (float)Vertics[Checked][Sides[Checked][i].vertics[h] - 1].y, (float)Vertics[Checked][Sides[Checked][i].vertics[0] - 1].x, (float)Vertics[Checked][Sides[Checked][i].vertics[0] - 1].y);
							}
							else
							{
								g.DrawLine(Pens.Green, (float)Vertics[Checked][Sides[Checked][i].vertics[h] - 1].x, (float)Vertics[Checked][Sides[Checked][i].vertics[h] - 1].y, (float)Vertics[Checked][Sides[Checked][i].vertics[h + 1] - 1].x, (float)Vertics[Checked][Sides[Checked][i].vertics[h + 1] - 1].y);
							}
						}
					}
					break;
            }
		}
        private void RotateBut_Click(object sender, EventArgs e)
        {
			double X = 0, Y = 0, Z = 0;
			if (X_Cord.Text != "")
			{
				X = Convert.ToDouble(X_Cord.Text);
			}
			if (Y_Cord.Text != "")
			{
				Y = Convert.ToDouble(Y_Cord.Text);
			}
            if (Z_Cord.Text != "")
            {
                Z = Convert.ToDouble(Z_Cord.Text);
            }
			if(BMP)
            {
				Rotate_BMP(X, Y);
				return;
			}
            switch (Checked)
            {
                case -1:
                    MessageBox.Show("Выберите фигуру");
                    return;
                default:
                    for (int i = 0; i < Vertics[Checked].Count(); i++)
                    {
                        var str = Vertics[Checked][i];
                        str.x -= Temp_x[Checked];
                        str.y -= Temp_y[Checked];
                        str.z -= Temp_z[Checked];
                        Vertics[Checked][i] = str;
                    }
                    break;
            }
            double COS = Math.Cos(X * Math.PI / 180);
            double SIN = Math.Sin(X * Math.PI / 180);
            for (int g = 0; g < Vertics[Checked].Count; g++)
            {
                var str = Vertics[Checked][g];
                str.y = Math.Round(Vertics[Checked][g].y * COS - Vertics[Checked][g].z * SIN, 5);
                str.z = Math.Round(Vertics[Checked][g].y * SIN + Vertics[Checked][g].z * COS, 5);
                Vertics[Checked][g] = str;
            }
            for (int g = 0; g < Normals[Checked].Count; g++)
            {
                var str = Normals[Checked][g];
                str.y = Math.Round(Normals[Checked][g].y * COS - Normals[Checked][g].z * SIN, 5);
                str.z = Math.Round(Normals[Checked][g].y * SIN + Normals[Checked][g].z * COS, 5);
                Normals[Checked][g] = str;
            }
            COS = Math.Cos(Y * Math.PI / 180);
            SIN = Math.Sin(Y * Math.PI / 180);
            for (int g = 0; g < Vertics[Checked].Count; g++)
            {
                var str = Vertics[Checked][g];
                str.x = Math.Round(Vertics[Checked][g].x * COS - Vertics[Checked][g].z * SIN, 5);
                str.z = Math.Round(Vertics[Checked][g].x * SIN + Vertics[Checked][g].z * COS, 5);
                Vertics[Checked][g] = str;
            }
            for (int g = 0; g < Normals[Checked].Count; g++)
            {
                var str = Normals[Checked][g];
                str.x = Math.Round(Normals[Checked][g].x * COS - Normals[Checked][g].z * SIN, 5);
                str.z = Math.Round(Normals[Checked][g].x * SIN + Normals[Checked][g].z * COS, 5);
                Normals[Checked][g] = str;
            }
            COS = Math.Cos(Z * Math.PI / 180);
            SIN = Math.Sin(Z * Math.PI / 180);
            for (int g = 0; g < Vertics[Checked].Count; g++)
            {
                var str = Vertics[Checked][g];
                str.x = Math.Round(Vertics[Checked][g].x * COS - Vertics[Checked][g].y * SIN, 5);
                str.y = Math.Round(Vertics[Checked][g].x * SIN + Vertics[Checked][g].y * COS, 5);
                Vertics[Checked][g] = str;
            }
            for (int g = 0; g < Normals[Checked].Count; g++)
            {
                var str = Normals[Checked][g];
                str.x = Math.Round(Normals[Checked][g].x * COS - Normals[Checked][g].y * SIN, 5);
                str.y = Math.Round(Normals[Checked][g].x * SIN + Normals[Checked][g].y * COS, 5);
                Normals[Checked][g] = str;
            }
            for (int i = 0; i < Vertics[Checked].Count; i++)
            {
                var str = Vertics[Checked][i];
                str.x += Temp_x[Checked];
                str.y += Temp_y[Checked];
                str.z += Temp_z[Checked];
                Vertics[Checked][i] = str;
            }
            if (Roberts) Draw_Roberts();
            else if (ZBuff) Check_ZBuff_Click();
            else Draw_Click(sender, e);
        }
        private void ScaleBut_Click(object sender, EventArgs e)
        {
			for (int i = 0; i < Vertics[Checked].Count(); i++)
			{
				var str = Vertics[Checked][i];
				str.x -= Temp_x[Checked];
				str.y -= Temp_y[Checked];
				str.z -= Temp_z[Checked];
				Vertics[Checked][i] = str;
			}
			int X = 1, Y = 1, Z = 1;
			if (X_Cord.Text != "")
			{
				X = Convert.ToInt32(X_Cord.Text);
			}
			if (Y_Cord.Text != "")
			{
				Y = Convert.ToInt32(Y_Cord.Text);
			}
			if (Z_Cord.Text != "")
			{
				Z = Convert.ToInt32(Z_Cord.Text);
			}
			switch (Checked)
			{
				case -1:
					MessageBox.Show("Выберите фигуру");
					break;
				default:
					for (int i = 0; i < Vertics[Checked].Count(); i++)
					{
						var str = Vertics[Checked][i];
						str.x *= X;
						str.y *= Y;
						str.z *= Z;
						Vertics[Checked][i] = str;
					}
					break;
			}
			for (int i = 0; i < Vertics[Checked].Count; i++)
			{
				var str = Vertics[Checked][i];
				str.x += Temp_x[Checked];
				str.y += Temp_y[Checked];
				str.z += Temp_z[Checked];
				Vertics[Checked][i] = str;
			}
			if (Roberts) Draw_Roberts();
			else if (ZBuff) Check_ZBuff_Click();
			else Draw_Click(sender, e);
		}
        private void TransBut_Click(object sender, EventArgs e)
        {
			int X = 0, Y = 0, Z = 0;
			if(X_Cord.Text != "")
            {
				X = Convert.ToInt32(X_Cord.Text);
				Temp_x[Checked] += X;
			}
			if(Y_Cord.Text != "")
            {
				Y = Convert.ToInt32(Y_Cord.Text);
				Temp_y[Checked] += Y;
			}
			if(Z_Cord.Text != "")
            {
				Z = Convert.ToInt32(Z_Cord.Text);
				Temp_z[Checked] += Z;
			}
			switch (Checked)
			{
				case -1:
					MessageBox.Show("Выберите фигуру");
					break;
				default:
					for (int i = 0; i < Vertics[Checked].Count(); i++)
					{
						var str = Vertics[Checked][i];
						str.x += X;
						str.y += Y;
						str.z += Z;
						Vertics[Checked][i] = str;
					}
						break;
			}
			if (Roberts) Draw_Roberts();
			else if (ZBuff) Check_ZBuff_Click();
			else Draw_Click(sender, e);
		}

        private void Check_Roberts_CheckedChanged(object sender, EventArgs e)
        {
			if(Check_Roberts.Checked)
            {
				Roberts = true;
				ZBuff = false;
				Draw_Roberts();
			}
			else
            {
				Roberts = false;
				Draw_Click(sender, e);
			}
		}
		private void Draw_Roberts()
        {
			g.Clear(BackColor);
			CreateMatr();
			ChoseSides();
			for (int i = 0; i < Draw_Sides.Count(); i++)
			{
				for (int h = 0; h < Draw_Sides[i].vertics.Length; h++)
				{
					if (h + 1 >= Draw_Sides[i].vertics.Length)
					{
						g.DrawLine(Pens.Green, (float)Vertics[Checked][Draw_Sides[i].vertics[h] - 1].x, (float)Vertics[Checked][Draw_Sides[i].vertics[h] - 1].y, (float)Vertics[Checked][Draw_Sides[i].vertics[0] - 1].x, (float)Vertics[Checked][Draw_Sides[i].vertics[0] - 1].y);
					}
					else
					{
						g.DrawLine(Pens.Green, (float)Vertics[Checked][Draw_Sides[i].vertics[h] - 1].x, (float)Vertics[Checked][Draw_Sides[i].vertics[h] - 1].y, (float)Vertics[Checked][Draw_Sides[i].vertics[h + 1] - 1].x, (float)Vertics[Checked][Draw_Sides[i].vertics[h + 1] - 1].y);
					}
				}
			}
		}
		private void CreateMatr()
        {
			Arr = new double[Sides[Checked].Count(), 4];
			double X, Y, Z, D;
			for(int i = 0; i < Sides[Checked].Count(); i++)
            {
				X = Normals[Checked][Sides[Checked][i].normals - 1].x;
				Y = Normals[Checked][Sides[Checked][i].normals - 1].y;
				Z = Normals[Checked][Sides[Checked][i].normals - 1].z;
				D = X * (-Vertics[Checked][Sides[Checked][i].vertics[0] - 1].x) + Y * (-Vertics[Checked][Sides[Checked][i].vertics[0] - 1].y) + Z * (-Vertics[Checked][Sides[Checked][i].vertics[0] - 1].z);
				Arr[i, 0] = X / D;
				Arr[i, 1] = Y / D;
				Arr[i, 2] = Z / D;
				Arr[i, 3] = 1;
			}
		}
		private void ChoseSides()
        {
			double temp_X = (Vertics[Checked].Max(x => x.x) + Vertics[Checked].Min(x => x.x)) / 2;
			double temp_Y = (Vertics[Checked].Max(y => y.y) + Vertics[Checked].Min(y => y.y)) / 2;
			double temp_Z = (Vertics[Checked].Max(z => z.z) + Vertics[Checked].Min(z => z.z)) / 2;
			double[] temp_arr = new double[Sides[Checked].Count()];
			Draw_Sides.Clear();
			for (int i = 0; i < Sides[Checked].Count(); i++)
			{
				temp_arr[i] = temp_X * Arr[i, 0] + temp_Y * Arr[i, 1] + temp_Z * Arr[i, 2] + Arr[i, 3];
				if (temp_arr[i] < 0)
				{
					for (int g = 0; g < 4; g++)
					{
						Arr[i, g] = -Arr[i, g];
					}
				}
				temp_arr[i] = -1 * Arr[i, 2];
				if (temp_arr[i] <= 0)
				{
					continue;
				}
				Draw_Sides.Add(Sides[Checked][i]);
			}
		}
		private void Check_ZBuff_CheckedChanged(object sender, EventArgs e)
		{
			if (Check_ZBuff.Checked)
			{
				ZBuff = true;
				Roberts = false;
				Check_ZBuff_Click();
			}
			else
			{
				ZBuff = false;
				Draw_Click(sender, e);
			}
		}
		private void Check_ZBuff_Click()
		{
			CalculateLight();
			g.Clear(BackColor);
			CreateMatr();
			int Max = (int)FindMax();
			int Min = (int)FindMin();
			int Start = -1, finish = -1;
			int first, second, third, fouth;
			List<SideWithKoef> edges = new List<SideWithKoef>(), temp_edges = new List<SideWithKoef>();
			double Dx1, Dy1, Dx2, Dy2, X1, X2, Z, Z_temp, temp, C;
			for (int i = Max; i < Min; i++)
			{
				for (int p = 0; p < Canvas.Width; p++)
				{
					Z_Buffer[p] = new Z_Buf(-9999, Color.White);
				}
				edges.Clear();
				temp_edges.Clear();
				temp_edges.AddRange(FindSides(i));

				for (int j = 0; j < temp_edges.Count; j++)
				{
					if (!edges.Contains(temp_edges[j]))
					{
						edges.Add(temp_edges[j]);
					}
				}
				for (int j = 0; j < edges.Count; j++)
				{
					first = -1;
					second = -1;
					third = -1;
					fouth = -1;
					for (int k = 0; k < edges[j].vertics.Length; k++)
					{
						if (k + 1 < edges[j].vertics.Length)
						{
							if ((Vertics[Checked][edges[j].vertics[k] - 1].y >= i && Vertics[Checked][edges[j].vertics[k + 1] - 1].y <= i) || (Vertics[Checked][edges[j].vertics[k] - 1].y <= i && Vertics[Checked][edges[j].vertics[k + 1] - 1].y >= i))
							{
								if (first == -1)
								{
									first = edges[j].vertics[k] - 1;
									second = edges[j].vertics[k + 1] - 1;
								}
								else if (third == -1)
								{
									third = edges[j].vertics[k] - 1;
									fouth = edges[j].vertics[k + 1] - 1;
								}
							}
						}
						else
						{
							if ((Vertics[Checked][edges[j].vertics[k] - 1].y >= i && Vertics[Checked][edges[j].vertics[0] - 1].y <= i) || (Vertics[Checked][edges[j].vertics[k] - 1].y <= i && Vertics[Checked][edges[j].vertics[0] - 1].y >= i))
							{
								if (first == -1)
								{
									first = edges[j].vertics[k] - 1;
									second = edges[j].vertics[0] - 1;
								}
								else if (third == -1)
								{
									third = edges[j].vertics[k] - 1;
									fouth = edges[j].vertics[0] - 1;
								}
							}
						}
					}
					Dx1 = Vertics[Checked][second].x - Vertics[Checked][first].x;
					Dx1 = Dx1 == 0 ? 1 : Dx1;
					Dx2 = Vertics[Checked][fouth].x - Vertics[Checked][third].x;
					Dx2 = Dx2 == 0 ? 1 : Dx2;
					Dy1 = Vertics[Checked][second].y - Vertics[Checked][first].y;
					Dy1 = Dy1 == 0 ? 1 : Dy1;
					Dy2 = Vertics[Checked][fouth].y - Vertics[Checked][third].y;
					Dy2 = Dy2 == 0 ? 1 : Dy2;
					X1 = (i * Dx1 - Vertics[Checked][first].y * Dx1 + Vertics[Checked][first].x * Dy1) / Dy1;
					X2 = (i * Dx2 - Vertics[Checked][third].y * Dx2 + Vertics[Checked][third].x * Dy2) / Dy2;
					C = Arr[Sides[Checked].IndexOf(new Cords(edges[j])), 2];
					C = C == 0 ? 1 : C;
					if (X1 > X2)
					{
						temp = X1;
						X1 = X2;
						X2 = temp;
					}
					X1 = X1 < 0 ? 0 : X1;
					Z_temp = (-Arr[Sides[Checked].IndexOf(new Cords(edges[j])), 3] - Arr[Sides[Checked].IndexOf(new Cords(edges[j])), 0] * X1 - Arr[Sides[Checked].IndexOf(new Cords(edges[j])), 1] * i) / C;
					for (int q = (int)X1; q < (int)X2; q++)
					{
						Z = Z_temp - (Arr[Sides[Checked].IndexOf(new Cords(edges[j])), 0] / C);
						Z_temp = Z;
						if (Z_Buffer[q].Z < Z)
						{
							Z_Buffer[q].Z = Z;
							Z_Buffer[q].color = Color.FromArgb((int)(255 - 150 * edges[j].Light_koef), (int)(255 - 150 * edges[j].Light_koef), (int)(255 - 150 * edges[j].Light_koef));
						}
					}
				}
				for (int q = 0; q < Z_Buffer.Length; q++)
				{
					Start = q;
					while (q + 1 < Z_Buffer.Length && Z_Buffer[q].color == Z_Buffer[q + 1].color)
					{
						q++;
					}
					finish = q;
					g.DrawLine(new Pen(Z_Buffer[q].color), Start, i, finish, i);
				}
			}
		}
		private List<SideWithKoef> FindSides(int height)
        {
			List<SideWithKoef> edges = new List<SideWithKoef>();
			for(int i = 0; i < Sides[Checked].Count; i++)
            {
				if(FindEdges(Sides[Checked][i], height))
                {
					SideWithKoef temp = new SideWithKoef(Sides[Checked][i], Light_Koef[i]);
					edges.Add(temp);
				}
            }
			return edges;
        }
		private bool FindEdges(Cords sides, int height)
		{
			for (int j = 0; j < sides.vertics.Length; j++)
			{
				if (j + 1 < sides.vertics.Length)
				{
					if ((Vertics[Checked][sides.vertics[j] - 1].y > height && Vertics[Checked][sides.vertics[j + 1] - 1].y < height) || (Vertics[Checked][sides.vertics[j] - 1].y < height && Vertics[Checked][sides.vertics[j + 1] - 1].y > height))
					{
						return true;
					}
					else if (Vertics[Checked][sides.vertics[j] - 1].y == height && Vertics[Checked][sides.vertics[j + 1] - 1].y == height && Vertics[Checked][sides.vertics[j] - 1].x != Vertics[Checked][sides.vertics[j + 1] - 1].x)
					{
						return true;
					}
				}
				else
				{
					if ((Vertics[Checked][sides.vertics[j] - 1].y > height && Vertics[Checked][sides.vertics[0] - 1].y < height) || (Vertics[Checked][sides.vertics[j] - 1].y < height && Vertics[Checked][sides.vertics[0] - 1].y > height))
					{
						return true;
					}
					else if (Vertics[Checked][sides.vertics[j] - 1].y == height && Vertics[Checked][sides.vertics[0] - 1].y == height && Vertics[Checked][sides.vertics[j] - 1].x != Vertics[Checked][sides.vertics[0] - 1].x)
					{
						return true;
					}
				}
			}
			return false;
		}
		private double FindMax()
        {
			double max = 100000;
			for(int i = 0; i < Vertics[Checked].Count; i++)
            {
				if(Vertics[Checked][i].y < max)
                {
					max = Vertics[Checked][i].y;
                }
            }
			return max;
        }
		private double FindMin()
        {
			double min = -100000;
			for(int i = 0; i < Vertics[Checked].Count; i++)
            {
				if(Vertics[Checked][i].y > min)
                {
					min = Vertics[Checked][i].y;
                }
            }
			return min;
        }

		private void BMP_Ckeck_CheckedChanged(object sender, EventArgs e)
		{
			IcoCheck.Enabled = !IcoCheck.Enabled;
			CubeCheck.Enabled = !CubeCheck.Enabled;
			DodCheck.Enabled = !DodCheck.Enabled;
			Check_Roberts.Enabled = !Check_Roberts.Enabled;
			Check_ZBuff.Enabled = !Check_ZBuff.Enabled;
			TransBut.Enabled = !TransBut.Enabled;
			ScaleBut.Enabled = !ScaleBut.Enabled;
			Z_Cord.Enabled = !Z_Cord.Enabled;
			label3.Enabled = !label3.Enabled;
			BMP = !BMP;
			Draw_BMP();
		}

        private void CalculateLight()
		{
			double Aver_X = 0, Aver_Y = 0, Aver_Z = 0;
			double[] koefs = new double[3];
			double res;
			Light_Koef = new double[Sides[Checked].Count];
			for (int i = 0; i < Sides[Checked].Count; i++)
			{
				for (int g = 0; g < Sides[Checked][i].vertics.Length; g++)
				{
					Aver_X += Vertics[Checked][Sides[Checked][i].vertics[g] - 1].x;
					Aver_Y += Vertics[Checked][Sides[Checked][i].vertics[g] - 1].y;
					Aver_Z += Vertics[Checked][Sides[Checked][i].vertics[g] - 1].z;
				}
				Aver_X /= Sides[Checked][i].vertics.Length;
				Aver_Y /= Sides[Checked][i].vertics.Length;
				Aver_Z /= Sides[Checked][i].vertics.Length;
				koefs[0] = Aver_X - x_light;
				koefs[1] = Aver_Y - y_light;
				koefs[2] = Aver_Z - z_light;
				res = Math.Abs(koefs[0] * Normals[Checked][Sides[Checked][i].normals - 1].x + koefs[1] * Normals[Checked][Sides[Checked][i].normals - 1].y + koefs[2] * Normals[Checked][Sides[Checked][i].normals - 1].z) / (Math.Sqrt(koefs[0] * koefs[0] + koefs[1] * koefs[1] + koefs[2] * koefs[2]) * Math.Sqrt(Normals[Checked][Sides[Checked][i].normals - 1].x * Normals[Checked][Sides[Checked][i].normals - 1].x + Normals[Checked][Sides[Checked][i].normals - 1].y * Normals[Checked][Sides[Checked][i].normals - 1].y + Normals[Checked][Sides[Checked][i].normals - 1].z * Normals[Checked][Sides[Checked][i].normals - 1].z));
				if(res < 0) res = 0;
				Light_Koef[i] = res;
			}
		}
		struct Vertic
		{
			public double x, y, z;

            public Vertic(double x1, double y1, double z1)
            {
                x = x1;
                y = y1;
                z = z1;
            }
        }

  //      private void Light_Butt_Click(object sender, EventArgs e)
  //      {
		//	if (X_Light_Cords.Text != "")
		//	{
		//		x_light = Convert.ToDouble(X_Light_Cords.Text);
		//	}
		//	if (Y_Light_Cords.Text != "")
		//	{
		//		y_light = Convert.ToDouble(Y_Light_Cords.Text);
		//	}
		//	if (Z_Light_Cords.Text != "")
		//	{
		//		z_light = Convert.ToDouble(Z_Light_Cords.Text);
		//	}
		//}

        struct Cords
		{
			public int[] vertics;
			public int normals;

            public Cords(int[] verts, int normal)
			{
				vertics = verts;
				normals = normal;
            }
			public Cords(SideWithKoef sidewithkoef)
            {
				vertics = sidewithkoef.vertics;
				normals = sidewithkoef.normals;
			}
        }
		struct Z_Buf
		{
			public double Z;
			public Color color;
			public Z_Buf(double z, Color col)
			{
				Z = z;
				color = col;
			}
		}
		struct Edge
        {
			public double X1, Y1, X2, Y2;
			public int Side;

            public Edge(double x1, double y1, double x2, double y2, int side)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
				Side = side;
            }
        }
		struct SideWithKoef
		{
			public int[] vertics;
			public int normals;
			public double Light_koef;

			public SideWithKoef(int[] verts, int normal, double light_koef)
			{
				vertics = verts;
				normals = normal;
				Light_koef = light_koef;
			}
			public SideWithKoef(Cords cord, double light_koef)
			{
				vertics = cord.vertics;
				normals = cord.normals;
				Light_koef = light_koef;
			}
		}
	}
}
