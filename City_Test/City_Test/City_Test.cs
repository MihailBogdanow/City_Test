using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;


namespace City_Test
//Начало Модуля 1 
{
    public partial class City_Test : Form
    {
        private const int ROAD_SIZE = 32;

        private SQLiteConnection _connection = new SQLiteConnection("Data Source=database.db");
        private SQLiteCommand _command = new SQLiteCommand();

        private Bitmap[] _roadBitmaps = new Bitmap[]
        {
            null,
            Properties.Resources.Road,
            Properties.Resources.Road,
            Properties.Resources.Road,
            Properties.Resources.Road,
            Properties.Resources.Road_Cross,
            Properties.Resources.Rstart,
            Properties.Resources.Rfinish
        };

        private Bitmap[] _carBitmaps = new Bitmap[]
        {
            Properties.Resources.Cvertical,
            Properties.Resources.Cright,
            Properties.Resources.Cbottom,
            Properties.Resources.Cleft
        };

        private int[,] _roadmap = {
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,0,0,0,0 },
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,0,0,0,0 },
            {0,5,5,4,4,4,5,5,4,4,4,4,5,5,4,4,5,5,4,4 },
            {0,5,5,3,3,3,5,5,3,3,3,3,5,5,3,3,5,5,3,3 },
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,2,1,0,0 },
            {7,5,5,0,0,0,2,1,0,0,0,0,2,1,0,0,2,1,0,0 },
            {3,5,5,0,0,0,5,5,4,4,4,4,5,5,0,0,2,1,0,0 },
            {0,2,1,0,0,0,5,5,3,3,3,3,5,5,0,0,2,1,0,0 },
            {0,2,1,0,0,0,2,1,0,0,0,0,5,5,4,4,5,5,4,4 },
            {0,5,5,4,4,4,5,5,0,0,0,0,5,5,3,3,5,5,3,3 },
            {0,5,5,3,3,3,5,5,0,0,0,0,2,1,0,0,2,1,0,0 },
            {0,2,1,0,0,0,5,5,4,4,4,4,5,5,0,0,2,1,0,0 },
            {0,2,1,0,0,0,5,5,3,3,3,3,5,5,0,0,2,1,0,0 },
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,2,1,0,0 },
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,2,1,0,0 },
            {0,2,1,0,0,0,2,1,0,0,0,0,2,1,0,0,2,1,0,0 },
            {4,5,5,4,4,4,5,5,4,4,4,4,5,5,4,4,5,5,0,0 },
            {3,5,5,3,3,3,5,5,3,3,3,3,5,5,3,3,5,5,0,0 },
            {0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,0,0 },
            {0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,2,6,0,0 },
        };

        private int[,] _objects = {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0 },
            {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },
            {0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        };

        private int[] _selectedRoadPosition = new int[] { -1, -1 };

        private int[] _carPosition = new int[] { 17, 19, 0 };

        private int[] _lastCross = new int[2];
        private int _iteration = 0;
        private int _step = 0;
        private int _bestResult = 0;
//Модуль 3 База Данных
        public City_Test()
        {
            InitializeComponent();

            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "CREATE TABLE IF NOT EXISTS Logs (Log_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Log_result varchar(6) NOT NULL, Log_time INTEGER NOT NULL)";
            _command.ExecuteNonQuery();
        }
//Конец 3 Модуля
        private void City_Test_Load(object sender, EventArgs e)
        {
            TransposeArrays();
            BuildRoad();
            DrawRoad();
        }

        private void TransposeArrays()
        {
            int[,] roadmap = new int[20, 20];
            int[,] objects = new int[20, 20];
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    roadmap[x, y] = _roadmap[y, x];
                    objects[x, y] = _objects[y, x];
                }
            }
            _roadmap = roadmap;
            _objects = objects;
        }

        private void BuildRoad()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    PictureBox road = new PictureBox
                    {
                        Name = $"{x}_{y}",
                        Size = new Size(ROAD_SIZE, ROAD_SIZE),
                        Location = new Point(ROAD_SIZE * x, ROAD_SIZE * y),
                    };

                    road.Click += RoadClick;
                    road.MouseHover += RoadHover;

                    panel.Controls.Add(road);
                }
            }
        }

        private void DrawRoad()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    PictureBox road = panel.Controls[x + "_" + y] as PictureBox;
                    road.BackgroundImage = _roadBitmaps[_roadmap[x, y]];

                    switch (_roadmap[x, y])
                    {
                        case 2:
                            road.BackgroundImage = RotateImage(road.BackgroundImage, 180);
                            break;
                        case 3:
                            road.BackgroundImage = RotateImage(road.BackgroundImage, 90);
                            break;
                        case 4:
                            road.BackgroundImage = RotateImage(road.BackgroundImage, 270);
                            break;
                    }

                    switch (_objects[x, y])
                    {
                        case 1:
                            road.Image = Properties.Resources.Block;
                            break;
                        case 2:
                            road.Image = Properties.Resources.Cvertical;
                            break;
                    }
                }
            }
        }

        private Image RotateImage(Image image, float angle)
        {
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2);
                g.RotateTransform(angle);
                g.DrawImage(image, -image.Width / 2, -image.Height / 2, image.Width, image.Height + 1);
            }
            return rotatedImage;
        }

        private void RoadClick(object sender, EventArgs e)
        {
            PictureBox road = sender as PictureBox;
            string[] roadPosition = road.Name.Split('_');
            int x = int.Parse(roadPosition[0]);
            int y = int.Parse(roadPosition[1]);

            if (_selectedRoadPosition[0] > 0)
            {
                PictureBox selectedRoad = panel.Controls[_selectedRoadPosition[0] + "_" + _selectedRoadPosition[1]] as PictureBox;
                selectedRoad.BorderStyle = BorderStyle.None;
            }

            road.BorderStyle = BorderStyle.Fixed3D;
            button_add.Enabled = true;
            _selectedRoadPosition[0] = x;
            _selectedRoadPosition[1] = y;
            if (_objects[_selectedRoadPosition[0], _selectedRoadPosition[1]] == 1)
            {
                button_delete.Enabled = true;
            }
            else
            {
                button_delete.Enabled = false;
            }
        }

        private void RoadHover(object sender, EventArgs e)
        {
            PictureBox road = sender as PictureBox;
            label_info.Text = "Координаты выделенной зоны: " + road.Name;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int x = _selectedRoadPosition[0];
            int y = _selectedRoadPosition[1];

            if (x != -1)
            {
                if (_roadmap[x, y] >= 1 & _roadmap[x, y] <= 4)
                {
                    PictureBox selectedRoad = panel.Controls[x + "_" + y] as PictureBox;
                    selectedRoad.Image = Properties.Resources.Block;
                    _objects[x, y] = 1;
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int x = _selectedRoadPosition[0];
            int y = _selectedRoadPosition[1];

            if (x != -1)
            {
                PictureBox road = panel.Controls[x + "_" + y] as PictureBox;
                road.Image = null;
                _objects[x, y] = 0;
                button_delete.Enabled = false;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            timer.Stop();
            StopMove(false);
        }
//Конец 1 Модуля
//Модуль 2
        private void StopMove(bool result)
        {
            PictureBox car = panel.Controls[_carPosition[0] + "_" + _carPosition[1]] as PictureBox;
            car.Image = null;
            _carPosition = new int[3] { 17, 19, 0 };

            car = panel.Controls[_carPosition[0] + "_" + _carPosition[1]] as PictureBox;
            car.Image = Properties.Resources.Cvertical;

            _command.CommandText = "INSERT INTO Logs (Log_result, Log_time) VALUES ('" + result + "', " + _step + ")";
            _command.ExecuteNonQuery();

            _iteration++;
            label_iteration.Text = "Итерация: " + _iteration.ToString();
            _step = 0;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = (10 - trackBar.Value) * 100;
            label_speed.Text = "Частота обновления (мс): " + timer.Interval + "мс";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveCar();
            _step++;
            label_step.Text = "Шагов за итерацию: " + _step.ToString();
        }
//Конец 2 Модуля
//Начало 4 Модуля
        private void MoveCar()
        {
            int x = _carPosition[0];
            int y = _carPosition[1];
            int direction = _carPosition[2];

            PictureBox roadStart = panel.Controls[x + "_" + y] as PictureBox;

            ChangePosition(direction, ref x, ref y);

            if (x < 20 && y < 20 && x >= 0 && y >= 0)
            {
                if (_roadmap[x, y] != 0 && _objects[x, y] == 0 && _roadmap[x, y] != 6)
                {
                    PictureBox roadEnd = panel.Controls[x + "_" + y] as PictureBox;

                    roadStart.Image = null;
                    roadEnd.Image = _carBitmaps[direction];

                    if (_roadmap[_carPosition[0], _carPosition[1]] == 5)
                    {
                        if (_roadmap[x, y] != 5)
                        {
                            _lastCross[0] = x;
                            _lastCross[1] = y;
                        }
                    }

                    _carPosition = new int[] { x, y, direction };

                    if (_roadmap[x, y] == 7)
                    {
                        if (_bestResult == 0 || _step < _bestResult)
                        {
                            _bestResult = _step;
                            label_best.Text = "Лучший проход = " + _bestResult.ToString();
                        }
                        StopMove(true);
                    }
                }
                else
                {
                    if (_roadmap[_carPosition[0], _carPosition[1]] == 5)
                    {
                        x = _carPosition[0];
                        y = _carPosition[1];
                        direction = RotateRight(direction, 1);
                        ChangePosition(direction, ref x, ref y);

                        if (CompareRotation(direction, _roadmap[x, y]))
                        {
                            _carPosition[2] = direction;
                            roadStart.Image = _carBitmaps[direction];
                        }
                        else
                        {
                            direction = RotateRight(direction, 2);
                            x = _carPosition[0];
                            y = _carPosition[1];
                            ChangePosition(direction, ref x, ref y);
                            ChangePosition(direction, ref x, ref y);

                            if (CompareRotation(direction, _roadmap[x, y]))
                            {
                                _carPosition[2] = direction;
                                roadStart.Image = _carBitmaps[direction];
                            }
                            else
                            {
                                x = _carPosition[0];
                                y = _carPosition[1];

                                StopMove(false);
                                SetBlock(x, y);
                            }
                        }
                    }
                    else
                    {
                        StopMove(false);
                        SetBlockOnCross();
                    }
                }
            }
            else
            {
                StopMove(false);
                SetBlockOnCross();
            }
        }

        private bool CompareRotation(int directionCar, int directionRoad)
        {
            switch (directionCar)
            {
                case 0:
                    return (directionRoad == 1 || directionRoad == 5);
                case 1:
                    return (directionRoad == 3 || directionRoad == 5);
                case 2:
                    return (directionRoad == 2 || directionRoad == 5 || directionRoad == 7);
                case 3:
                    return (directionRoad == 4 || directionRoad == 5 || directionRoad == 7);
            }
            return false;
        }

        private void ChangePosition(int direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y++;
                    break;
                case 3:
                    x--;
                    break;
            }
        }

        private int RotateRight(int direction, int count)
        {
            direction += count;
            while (direction >= 4)
            {
                direction -= 4;
            }

            while (direction < 0)
            {
                direction += 4;
            }

            return direction;
        }

        private void SetBlockOnCross()
        {
            _roadmap[_lastCross[0], _lastCross[1]] = 6;
            PictureBox road = panel.Controls[_lastCross[0] + "_" + _lastCross[1]] as PictureBox;
            road.Image = Properties.Resources.Block_AI;
        }

        private void SetBlock(int x, int y)
        {
            _roadmap[x, y] = 6;
            PictureBox road = panel.Controls[x + "_" + y] as PictureBox;
            road.Image = Properties.Resources.Block_AI;
        }
//Конец 4 Модуля
//Продолжение 2 Модуля
        private void button_up_Click(object sender, EventArgs e)
        {
            MoveCar();
        }

        private void button_left_Click(object sender, EventArgs e)
        {
            RotateCar(-1);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            RotateCar(2);
        }

        private void button_right_Click(object sender, EventArgs e)
        {
            RotateCar(1);
        }

        private void RotateCar(int rotate)
        {
            int x = _carPosition[0];
            int y = _carPosition[1];
            int direction = _carPosition[2];

            direction = RotateRight(direction, rotate);
            _carPosition[2] = direction;

            PictureBox road = panel.Controls[x + "_" + y] as PictureBox;
            road.Image = _carBitmaps[direction];
        }
    }
}
//Конец 2 Модуля