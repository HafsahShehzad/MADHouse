using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MADHouse
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : Window
    {

        static int room_index = 1;
        static int w_i = 1;

        struct Values
        {
            public float height;
            public float width;
            public string name;
            public double X;
            public double Y;
            Rectangle room;
            Polygon rooom;

            public double return_X()
            {
                return X;
            }

            public double return_Y()
            {
                return Y;
            }

            public float return_height()
            {
                return height;
            }

            public float return_width()
            {
                return width;
            }

            public void setWidth(float width)
            {
                this.width = width;

            }

            public void setHeight(float heght)
            {
                this.height = heght;

            }

            public Rectangle GetRoom()
            {
                return room;
            }

            public void setRoom(Rectangle r)
            {
                this.room = r;
            }

            public Polygon GetRooom()
            {
                return rooom;
            }

            public void setRooom(Polygon r)
            {
                this.rooom = r;
            }



        }
        struct RoomValues
        {
            public float height;
            public float width;
            public string name;
            public double X;
            public double Y;
            public List<int> dimens;

            public List<Values> list;
            public List<Rectangle> w_list;
            public List<Rectangle> d_list;

            public void setDimensions()
            {
                dimens = new List<int>();
            }

            public void setList()
            {
                list = new List<Values>();
                w_list = new List<Rectangle>();
                d_list = new List<Rectangle>();
            }

            public double return_X()
            {
                return X;
            }

            public double return_Y()
            {
                return Y;
            }

            public float return_height()
            {
                return height;
            }

            public float return_width()
            {
                return width;
            }

            public void setWidth(float width)
            {
                this.width = width;

            }

            public void setHeight(float heght)
            {
                this.height = heght;

            }

        }
        

        int canvas_width, canvas_length;
        Canvas c;

        int clicks;
        bool isDrag,isWDrag,isDDrag;
        bool isResize;
        Rectangle currentRoom;
        Rectangle window;
        Rectangle door;
        Polygon current_room;

        UnityHwndHost madhouse3d = null;

         List<RoomValues> dimensions;//for rooms
        List<Values> windows; //for windows
        List<Values> doors; //for doors 

        Rectangle currentWindow;
        Rectangle currentDoor;

        Point StartPoint, CurrentPoint;

        HitTestResult result;
       // Path path;
        MatrixTransform transform;

        public Project()
        {
            InitializeComponent();
            c = myCanvas;
            dimensions = new List<RoomValues>();
            windows = new List<Values>();
            doors = new List<Values>();
        }

        public Project(int width, int height, string name)
        {
            InitializeComponent();
            c = myCanvas;
            dimensions = new List<RoomValues>();
            windows = new List<Values>();
            doors = new List<Values>();
            if (name != null)
            {
                Title = "MADHouse - " + name;
            }
            else
            {
                Title = "MADHouse - New Project";
            }
            

            //if (width < 50 || height < 50)
            //{
            // canvas_width = 50;
            //canvas_length = 60;
            //}
            // else
            // {
            canvas_length = height;
                canvas_width = width;
            //}

            /*Rectangle rect = new Rectangle();
            rect.Height = canvas_length;
            rect.Width = canvas_width;

            rect.StrokeThickness = 1.5;
            rect.Stroke = System.Windows.Media.Brushes.SaddleBrown;

            rect.Fill = System.Windows.Media.Brushes.SaddleBrown;
            c.Children.Add(rect);*/
            c.MouseLeftButtonDown += canvas_mouseLeftButtonDown;
            c.MouseMove += canvas_mousemove;
            c.MouseLeftButtonUp += canvas_MouseLeftButtonUp;

            cW.Text = canvas_width.ToString()+"ft";
            cL.Text = canvas_length.ToString()+"ft";

            scale.Text = "1 grid = "; //what to do here

            transform = new MatrixTransform();
        }

        private void window_mouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            isWDrag = true;
            if(e.OriginalSource is Rectangle)
            {
                Rectangle clicked = (Rectangle)e.OriginalSource;

                if (clicked.Name.Contains("Window"))
                {
                    currentWindow = clicked;
                    currentWindow.CaptureMouse();
                }
            }
            

        }

        private void door_mouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDDrag = true;
            if (e.OriginalSource is Rectangle)
            {
                Rectangle clicked = (Rectangle)e.OriginalSource;

                if (clicked.Name.Contains("Door"))
                {
                    currentDoor = clicked;
                    currentDoor.CaptureMouse();
                }
            }


        }

        private void window_mouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isWDrag = false;
            isDrag = true;
            
            currentWindow.ReleaseMouseCapture();
            

        }

        private void door_mouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDDrag = false;
            isDrag = true;
            currentDoor.ReleaseMouseCapture();


        }

        private int returnWindow(string name)
        {
            for(int i = 0; i < windows.Count; i++)
            {
                if (windows[i].name.Equals(name))
                {
                    return i;
                }
            }
            return -1;
        }

        private int returnDoor(string name)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].name.Equals(name))
                {
                    return i;
                }
            }
            return -1;
        }
        private void createHorizontalWindow(double x, double y)
        {
            isWDrag = false;
            c.Children.Remove(currentWindow);
            Rectangle window = new Rectangle();
            window.Height = 10;
            window.Width = 30;

            window.StrokeThickness = 2;
            window.Stroke = System.Windows.Media.Brushes.CornflowerBlue;

            window.Fill = System.Windows.Media.Brushes.White;

            //add to list of shapes 

            window.Name = "window_h_" + w_i;

            Values r = new Values();
            r.height = 30;
            r.width = 10;
            r.name = "window_h_" + w_i;

            int index = returnWindow(currentWindow.Name);
            currentWindow = window;
            r.X = x;
            r.Y = y;

            windows[index] = r;
            draw_window(currentWindow);
            Canvas.SetLeft(window, x);
            Canvas.SetTop(window, y);
            c.Children.Add(window);

            w_i++;
        }
        private void door_mouseMove(object sender, MouseEventArgs e)
        {
            if (!isDDrag) return;
            int d = returnDoor(currentDoor.Name); //get window index
            Values d_v = doors[d]; //get window 
            Shape room = null;

            //if room is polygon or rectangle 
            if (d_v.GetRoom() == null)
            {
                room = (Polygon)d_v.GetRooom();

            }

            else if (d_v.GetRooom() == null)
            {
                room = (Rectangle)d_v.GetRoom();
                current_room = null;
            }
            
            isDrag = false;
            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(c);

            // center the rect on the mouse
            double left = mousePos.X - (currentDoor.ActualWidth / 2);
            double top = mousePos.Y - (currentDoor.ActualHeight / 2);

            double x = left, y = top;


            if (currentDoor.Name.Contains("l"))
            {
                if (room != null)
                {
                    double l = 0, r = 0;
                    if (room is Polygon)
                    {
                        l = Canvas.GetLeft(room) + 50;
                        r = l + ((room.ActualWidth) - currentDoor.Width);
                    }
                    else
                    {
                        l = Canvas.GetLeft(room);
                        r = l + ((room.Width) - currentDoor.Width);
                    }
                    if ((left - l) > (left - r))
                    {
                        x = l;
                    }
                    else
                    {
                        x = r;
                    }

                    double t = 0, b = 0;
                    if (room is Polygon)
                    {
                        t = Canvas.GetTop(room) + 100;
                        b = t + room.ActualHeight - currentDoor.Height - 100;
                    }
                    else
                    {
                        t = Canvas.GetTop(room);
                        b = t + room.Height - currentDoor.Height;
                    }

                    if (top > t && top < b)
                    {
                        y = top;
                    }
                    else if (top <= t)
                    {
                        /* if ((t - top) < 10)
                         {
                             y = t;
                         }
                         else
                         {
                             x = Canvas.GetLeft(current_room) - currentWindow.Width / 2;
                             createHorizontalWindow(x, t);


                         }*/
                        y = t;

                    }
                    else if (top >= b)
                    {
                        y = b;

                    }
                }

                

            }
            else if (currentDoor.Name.Contains("t"))
            {

                if (room != null)
                {
                    double t = 0, b = 0;
                    if (room is Polygon)
                    {
                        t = Canvas.GetTop(room) + 100;
                        b = t + ((room.ActualHeight) - currentDoor.Height) - 100;
                    }
                    else
                    {
                        t = Canvas.GetTop(room);
                        b = t + ((room.Height) - currentDoor.Height);
                    }

                    if (room.Name.Contains("Room1"))
                    {
                        if ((top - t) > (top - b))
                        {
                            y = b;
                        }
                        else
                        {
                            y = t;
                        }
                    }

                    else
                    {
                        if ((top - t) > (top - b))
                        {
                            y = t;
                        }
                        else
                        {
                            y = b;
                        }
                    }
                    double l = 0, r = 0;
                    if (room is Polygon)
                    {
                        l = Canvas.GetLeft(room) + 50;
                        r = l + ((room.ActualWidth) - currentDoor.Width) - 50;
                    }
                    else
                    {
                        l = Canvas.GetLeft(room);
                        r = l + ((room.Width) - currentDoor.Width);
                    }
                    if (left > l && left < r)
                    {
                        x = left;
                    }
                    else if (x <= l)
                    {
                        x = l;
                    }
                    else if (x >= r)
                    {
                        x = r;
                    }


                }
            }

            Values v = doors[d];
            v.X = x;
            v.Y = y;
            doors[d] = v;
            Canvas.SetLeft(currentDoor, x);
            Canvas.SetTop(currentDoor, y);

        }
        private void window_mouseMove(object sender, MouseEventArgs e)
        {
            if (!isWDrag) return;
            if (currentWindow != null)
            {
                isWDrag = true; 
            }
            else
            {
                isWDrag = false;
            }
            int w = returnWindow(currentWindow.Name); //get window index
            Values w_v = windows[w]; //get window 
            Shape room=null;

            //if room is polygon or rectangle 
            if (w_v.GetRoom() == null)
            {
                room =(Polygon) w_v.GetRooom();
                
            }

            else if(w_v.GetRooom() == null)
            {
                room =(Rectangle) w_v.GetRoom();
                current_room = null;
            }
            
            isDrag = false;
            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(c);

            // center the rect on the mouse
            double left = mousePos.X - (currentWindow.ActualWidth / 2);
            double top = mousePos.Y - (currentWindow.ActualHeight / 2);

            double x = left, y = top;


            if (currentWindow.Name.Contains("v"))
            {
                if (room != null)
                {
                    double l = 0, r = 0;
                    if(room is Polygon)
                    {
                         l = Canvas.GetLeft(room)+50;
                         r = l + ((room.ActualWidth) - currentWindow.Width);
                    }
                    else
                    {
                         l = Canvas.GetLeft(room);
                         r = l + ((room.Width) - currentWindow.Width);
                    }
                    if ((left - l) > (left - r))
                    {
                        x = l;
                    }
                    else
                    {
                        x = r;
                    }

                    double t = 0, b = 0;
                    if (room is Polygon)
                    {
                         t = Canvas.GetTop(room)+100;
                         b = t + room.ActualHeight - currentWindow.Height-100;
                    }
                    else
                    {
                         t = Canvas.GetTop(room);
                         b = t + room.Height - currentWindow.Height;
                    }
                    
                    if (top > t && top < b)
                    {
                        y = top;
                    }
                    else if (top <= t)
                    {
                        /* if ((t - top) < 10)
                         {
                             y = t;
                         }
                         else
                         {
                             x = Canvas.GetLeft(current_room) - currentWindow.Width / 2;
                             createHorizontalWindow(x, t);


                         }*/
                        y = t;

                    }
                    else if (top >= b)
                    {
                        y = b;

                    }
                }

            }
            else if (currentWindow.Name.Contains("h"))
            {
                
                 if (room != null)
                {
                    double t = 0, b = 0;
                    if(room is Polygon)
                    {
                         t = Canvas.GetTop(room)+100;
                         b = t + ((room.ActualHeight) - currentWindow.Height)-100;
                    }
                    else
                    {
                         t = Canvas.GetTop(room);
                         b = t + ((room.Height) - currentWindow.Height);
                    }

                    if (room.Name.Contains("Room1"))
                    {
                        if ((top - t) > (top - b))
                        {
                            y = b;
                        }
                        else
                        {
                            y = t;
                        }
                    }

                    else
                    {
                        if ((top - t) > (top - b))
                        {
                            y = t;
                        }
                        else
                        {
                            y = b;
                        }
                    }
                    double l = 0, r = 0;
                    if(room is Polygon)
                    {
                        l= Canvas.GetLeft(room)+50;
                        r = l + ((room.ActualWidth) - currentWindow.Width)-50;
                    }
                    else
                    {
                        l = Canvas.GetLeft(room);
                        r = l + ((room.Width) - currentWindow.Width);
                    }
                    if(left>l && left < r)
                    {
                        x = left;
                    }
                    else if (x <= l)
                    {
                        x = l;
                    }
                    else if (x >= r)
                    {
                        x = r;
                    }


                }
            }

            Values v = windows[w];
            v.X = x;
            v.Y = y;


            windows[w] = v;

            Canvas.SetLeft(currentWindow, x);
            Canvas.SetTop(currentWindow, y);

        }

     
        private void room_mouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            isWDrag = false;
            isDDrag = false;
            isDrag = true;
            if (e.OriginalSource is Rectangle)
            {
                current_room = null;
                currentWindow = null;
                Rectangle clickedRoom = (Rectangle)e.OriginalSource;
                currentRoom = clickedRoom;
                currentRoom.CaptureMouse();

            }
            else if(e.OriginalSource is Polygon)
            {
                currentRoom = null;
                currentWindow = null;
                Polygon clickedRoom = (Polygon)e.OriginalSource;
                current_room = clickedRoom;
                current_room.CaptureMouse();

            }

            
        }

        private void Rroom_mouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrag = false;
            currentRoom.ReleaseMouseCapture();
            
        }
        private void room_mouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrag = false;
            current_room.ReleaseMouseCapture();

        }

        private void Rroom_MouseMove(object sender, MouseEventArgs e)
        {
            isWDrag = false;
            isDDrag = false;
            if (!isDrag) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(c);

            // center the rect on the mouse
            double left = mousePos.X - (currentRoom.ActualWidth / 2);
            double top = mousePos.Y - (currentRoom.ActualHeight / 2);
            Canvas.SetLeft(currentRoom, left);
            Canvas.SetTop(currentRoom, top);

            //update dimension 
            int i = returnRoom(currentRoom.Name);
            if (i != -1)
            {

                RoomValues r = new RoomValues();
                r.name = dimensions[i].name;
                r.height = dimensions[i].return_height();
                r.width = dimensions[i].return_width();
                r.X = left;
                r.Y = top;
                r.setList();
                r.setDimensions();
                r.dimens = dimensions[i].dimens;
                r.list = dimensions[i].list;

                double wx=0, wy=0;

                if (r.list.Count != 0)
                {
                    isWDrag = true;
                    for(int j = 0; j < r.list.Count; j++)
                    {
                        if (r.list[j].name.Contains("h"))
                        {
                            wx = r.X + ((r.width / 2) - (r.list[j].width / 2));
                            wy = r.Y;

                            

                        }
                        else if (r.list[j].name.Contains("v"))
                        {
                             wx = r.X;
                             wy = r.Y + ((r.height / 2) - (r.list[j].height / 2));
                        }

                        Values v = r.list[j];
                        v.X = wx;
                        v.Y = wy;
                        r.list[j] = v;


                    }
                }

                dimensions[i] = r;
                isWDrag = false;

            }
        }

        private void room_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (!isDrag) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(c);

            if (current_room == null)
            {
                return;
            }
            // center the rect on the mouse
            double left = mousePos.X - (current_room.ActualWidth / 2);
            double top = mousePos.Y - (current_room.ActualHeight / 2);
            Canvas.SetLeft(current_room, left);
            Canvas.SetTop(current_room, top);

            //update dimension 
            int i = returnRoom(current_room.Name);
            if (i != -1)
            {
                RoomValues r = new RoomValues();
                r.name = dimensions[i].name;
                r.height = dimensions[i].return_height();
                r.width = dimensions[i].return_width();
                r.X = left;
                r.Y = mousePos.Y;
                r.setList();
                r.setDimensions();
                r.dimens = dimensions[i].dimens;
                r.list = dimensions[i].list;

                if (r.list.Count != 0)
                {
                    for (int j = 0; j < r.list.Count; j++)
                    {
                        if (r.list[j].name.Contains("h"))
                        {
                            double wx = r.X + ((r.width / 2) - (r.list[j].width / 2));
                            double wy = r.Y;
                        }
                        else if (r.list[j].name.Contains("v"))
                        {
                            double wx = r.X;
                            double wy = r.Y + ((r.height / 2) - (r.list[j].height / 2));
                        }
                    }
                }
                dimensions[i] = r;

            }
        }

        private void draw_window(Rectangle r)
        {
            currentWindow.MouseLeftButtonDown += window_mouseButtonDown;
            currentWindow.MouseLeftButtonUp += window_mouseButtonUp;
            currentWindow.MouseMove += window_mouseMove;
        }

        private void draw_door(Rectangle r)
        {
            currentDoor.MouseLeftButtonDown += door_mouseButtonDown;
            currentDoor.MouseLeftButtonUp += door_mouseButtonUp;
            currentDoor.MouseMove += door_mouseMove;
        }

        private void draw_room(Shape s)
        {
            
            if(s is Rectangle)
            {
                currentRoom.MouseLeftButtonDown += room_mouseButtonDown;
                currentRoom.MouseLeftButtonUp += Rroom_mouseButtonUp;
                currentRoom.MouseMove += Rroom_MouseMove;
                
            }
            else if(s is Polygon)
            {
                current_room.MouseLeftButtonDown += room_mouseButtonDown;
                current_room.MouseLeftButtonUp += room_mouseButtonUp;
                current_room.MouseMove += room_MouseMove;
               
            }
            
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            Rectangle newBaseRoom = new Rectangle();
            newBaseRoom.Height = 200;
            newBaseRoom.Width = 200;

            newBaseRoom.StrokeThickness = 5;
            newBaseRoom.Stroke = System.Windows.Media.Brushes.DarkSlateGray;

            newBaseRoom.Fill= new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"E:\Unity\Projects\MADHouse\images\floor.jpg", UriKind.Absolute))
            };

            //add to list of shapes 



            //apply drag drop functionality 

            newBaseRoom.Name = "Room_" + room_index;

            RoomValues r = new RoomValues();
            r.height = 200;
            r.width = 200;
            r.name = "Room_" + room_index;
            r.X = 0;
            r.Y = 0;
            r.setList();

            dimensions.Add(r);

            currentRoom = newBaseRoom;
            draw_room(currentRoom);
            Display_Length.Text = newBaseRoom.Height.ToString();
            Display_Width.Text = newBaseRoom.Width.ToString();
            Canvas.SetLeft(newBaseRoom, 0);
            Canvas.SetTop(newBaseRoom, 0);
            Canvas.SetBottom(newBaseRoom, 0);
            Canvas.SetRight(newBaseRoom, 0);
            c.Children.Add(newBaseRoom);

            room_index++;

        }

        private void AddRoom_1(object sender, RoutedEventArgs e)
        {
            
            // Create a Polygon  
            Polygon room = new Polygon();
            room.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            room.StrokeThickness = 5;
            room.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"E:\Unity\Projects\MADHouse\images\floor.jpg", UriKind.Absolute))
            };


            // Create a collection of points for a polygon  
            System.Windows.Point Point1 = new System.Windows.Point(50, 100);
            System.Windows.Point Point2 = new System.Windows.Point(120, 100);
            System.Windows.Point Point3 = new System.Windows.Point(120, 150);
            System.Windows.Point Point4 = new System.Windows.Point(200, 150);
            System.Windows.Point Point5 = new System.Windows.Point(200, 250);
            System.Windows.Point Point6 = new System.Windows.Point(50, 250);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            polygonPoints.Add(Point4);
            polygonPoints.Add(Point5);
            polygonPoints.Add(Point6);
            // Set Polygon.Points properties  
            room.Points = polygonPoints;
            // Add Polygon to the page  

            //add drag and drop 

            //add to list 

            int h = 150;
            int w = 150;

            

            RoomValues r1 = new RoomValues();
            r1.width = 150;
            r1.height = 150;
            r1.name = "Room1_" + room_index;
            r1.X = 0;
            r1.Y = 0;
            r1.setList();
            r1.setDimensions();
            r1.dimens.Add(70);
            r1.dimens.Add(50);
            r1.dimens.Add(80);
            r1.dimens.Add(100);


            dimensions.Add(r1);

            current_room = room;
            room.Name = "Room1_" + room_index;
            draw_room(current_room);
            Display_Length.Text = h.ToString();
            Display_Width.Text = w.ToString();
            Canvas.SetLeft(room, 0);
            Canvas.SetTop(room, 0);
            c.Children.Add(room);

            room_index++;
        }

        private void AddRoom_2(object sender, RoutedEventArgs e)
        {

            // Create a Polygon  
            Polygon room = new Polygon();
            room.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            room.StrokeThickness = 5;
            room.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"E:\Unity\Projects\MADHouse\images\floor.jpg", UriKind.Absolute))
            };


            // Create a collection of points for a polygon  
            System.Windows.Point Point1 = new System.Windows.Point(50, 100);
            System.Windows.Point Point2 = new System.Windows.Point(250, 100);
            System.Windows.Point Point3 = new System.Windows.Point(250, 200);
            System.Windows.Point Point4 = new System.Windows.Point(200, 200);
            System.Windows.Point Point5 = new System.Windows.Point(200, 300);
            System.Windows.Point Point6 = new System.Windows.Point(50, 300);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            polygonPoints.Add(Point4);
            polygonPoints.Add(Point5);
            polygonPoints.Add(Point6);
            // Set Polygon.Points properties  
            room.Points = polygonPoints;
            // Add Polygon to the page  

            //add drag and drop 

            //add to list 

            current_room = room;
            room.Name = "Room2_" + room_index;
            draw_room(current_room);
            int h = 200;
            int w = 200;

            RoomValues r1 = new RoomValues();
            r1.width = 200;
            r1.height = 200;
            r1.name = "Room2_" + room_index;
            r1.X = 0;
            r1.Y = 0;
            r1.setList();
            r1.setDimensions();
            r1.dimens.Add(100);
            r1.dimens.Add(50);
            r1.dimens.Add(100);
            r1.dimens.Add(150);

            dimensions.Add(r1);

            Display_Length.Text = h.ToString();
            Display_Width.Text = w.ToString();
            Canvas.SetLeft(room, 0);
            Canvas.SetTop(room, 0);
            c.Children.Add(room);

            room_index++;
        }

        private void AddWindow_vert(object sender, RoutedEventArgs e)
        {

            if (dimensions.Count == 0)
            {
                return;
            }
            Rectangle w = new Rectangle();
            w.Height = 60;
            w.Width = 10;

            w.StrokeThickness = 2;
            w.Stroke = System.Windows.Media.Brushes.CornflowerBlue;

            w.Fill = System.Windows.Media.Brushes.White;

            //add to list of shapes 

            w.Name = "Window_v_" + w_i;

            Values r = new Values();
            r.height = 60;
            r.width = 10;
            r.name = "Window_v_" + w_i;

            if (currentRoom != null)
            {
                r.setRoom(currentRoom);
            }
            else if (current_room != null)
            {
                r.setRooom(current_room);
            }


            currentWindow = w;
            draw_window(currentWindow);

            Display_Length.Text = w.Height.ToString();
            Display_Width.Text = w.Width.ToString();

            double x=0, y=0;
            string name = null;
            int index=0;
            

            if(currentRoom != null)
            {
                x = Canvas.GetLeft(currentRoom);
                y = Canvas.GetTop(currentRoom) + ((currentRoom.Height / 2) - (currentWindow.Height / 2));
                 index = returnRoom(currentRoom.Name);
            }
            else if (current_room != null)
            {
                x = Canvas.GetLeft(current_room)+50 ;
                y = Canvas.GetTop(current_room)+((current_room.ActualHeight / 2) - (currentWindow.Height / 2))+100;
                 index = returnRoom(current_room.Name);
            }

            
            

            r.X = x;
            r.Y = y;


            for (int i = 0; i < windows.Count; i++)
            {
                if ((windows[i].X == x) && (windows[i].Y == y)){
                    return;
                }
            }
            for (int i = 0; i < doors.Count; i++)
            {
                if ((doors[i].X == x) && (doors[i].Y == y)){
                    return;
                }
            }

            Canvas.SetLeft(w, x);
            Canvas.SetTop(w, y);
            //find room 
            RoomValues rv = dimensions[index];
            if (rv.list.Count == 0)
            {
                rv.setList();
            }



            //add to window list 
            rv.list.Add(r);
            rv.w_list.Add(w);
            dimensions[index] = rv;

            windows.Add(r);
            c.Children.Add(w);

            w_i++;
        }

        private void AddWindow_horz(object sender, RoutedEventArgs e)
        {

            if (dimensions.Count == 0)
            {
                return;
            }
            Rectangle w = new Rectangle();
            w.Height = 10;
            w.Width = 60;

            w.StrokeThickness = 2;
            w.Stroke = System.Windows.Media.Brushes.CornflowerBlue;

            w.Fill = System.Windows.Media.Brushes.White;

            //add to list of shapes 

            w.Name = "Window_h_" + w_i;

            Values r = new Values();
            r.height = 60;
            r.width = 10;
            r.name = "Window_h_" + w_i;

            Display_Length.Text = w.Height.ToString();
            Display_Width.Text = w.Width.ToString();

            if (currentRoom != null)
            {
                r.setRoom(currentRoom);
            }
            else if (current_room != null)
            {
                r.setRooom(current_room);
            }


            currentWindow = w;
            draw_window(currentWindow);

            double x = 0, y = 0;

            int index = 0;
            if (currentRoom != null)
            {
                x = Canvas.GetLeft(currentRoom) + ((currentRoom.Width / 2)-(currentWindow.Width/2));
                y = Canvas.GetTop(currentRoom);
                index = returnRoom(currentRoom.Name);
            }
            else if (current_room != null)
            {
                if (current_room.Name.Contains("Room1"))
                {
                    x = Canvas.GetLeft(current_room) + ((current_room.ActualWidth / 2) - (currentWindow.Width / 2)) + 0;
                    y = Canvas.GetTop(current_room) + current_room.ActualHeight - (currentWindow.Width / 2);
                }
                else
                {
                    x = Canvas.GetLeft(current_room) + ((current_room.ActualWidth / 2) - (currentWindow.Width / 2)) + 0;
                    y = Canvas.GetTop(current_room) + 100;
                }
                
                
                index = returnRoom(current_room.Name);
            }

            

            r.X = x;
            r.Y = y;

            for (int i = 0; i < windows.Count; i++)
            {
                if ((windows[i].X == x) && (windows[i].Y == y)){
                    return;
                }
            }
            for (int i = 0; i < doors.Count; i++)
            {
                if ((doors[i].X == x) && (doors[i].Y == y)){
                    return;
                }
            }

            //find room 
            RoomValues rv = dimensions[index];
            if (rv.list.Count == 0)
            {
                rv.setList();
            }
            Canvas.SetLeft(w, x);
            Canvas.SetTop(w, y);

            //add to window list
            rv.list.Add(r);
            rv.w_list.Add(w);
            dimensions[index] = rv;
            

            windows.Add(r);
            c.Children.Add(w);

            w_i++;
        }

        private void AddDoor(object sender, RoutedEventArgs e)
        {
            if (dimensions.Count == 0)
            {
                return;
            }
            Rectangle d = new Rectangle();
            d.Height = 10;
            d.Width = 50;

            d.StrokeThickness = 2;
            d.Stroke = System.Windows.Media.Brushes.SaddleBrown;

            d.Fill = System.Windows.Media.Brushes.Brown;

            

            //add to list of shapes 

            d.Name = "Door_t_" + w_i;

            Values r = new Values();
            r.height = 10;
            r.width = 50;
            r.name = "Door_t_" + w_i;

            if (currentRoom != null)
            {
                r.setRoom(currentRoom);
            }
            else if (current_room != null)
            {
                r.setRooom(current_room);
            }


            currentDoor = d;
            draw_door(currentDoor);

            Display_Length.Text = d.Height.ToString();
            Display_Width.Text = d.Width.ToString();

            double x = 0, y = 0;
            string name = null;
            int index = 0;


            if (currentRoom != null)
            {
                x = Canvas.GetLeft(currentRoom) + ((currentRoom.Width / 2) - (currentDoor.Width / 2));
                y = Canvas.GetTop(currentRoom) ;
                index = returnRoom(currentRoom.Name);
            }
            else if (current_room != null)
            {
                if (current_room.Name.Contains("Room1"))
                {
                    x = Canvas.GetLeft(current_room) + ((current_room.ActualWidth / 2) - (currentDoor.Width / 2)) + 0;
                    y = Canvas.GetTop(current_room) + current_room.ActualHeight;
                }
                else
                {
                    x = Canvas.GetLeft(current_room) + ((current_room.ActualWidth / 2) - (currentDoor.Width / 2)) + 0;
                    y = Canvas.GetTop(current_room) + 100;
                }

                index = returnRoom(current_room.Name);
            }


            

            r.X = x;
            r.Y = y;

            for(int i = 0; i < windows.Count; i++)
            {
                if ((windows[i].X == x) && (windows[i].Y == y)){
                    return;
                }
            }
            for (int i = 0; i < doors.Count; i++)
            {
                if ((doors[i].X == x) && (doors[i].Y == y)){
                    return;
                }
            }
            Canvas.SetLeft(d, x);
            Canvas.SetTop(d, y);

            //find room 
            RoomValues rv = dimensions[index];
            if (rv.list.Count == 0)
            {
                rv.setList();
            }



            //add to window list 
            rv.list.Add(r);
            rv.d_list.Add(d);
            dimensions[index] = rv;

            doors.Add(r);
            c.Children.Add(d);

            w_i++;
        }
        private void AddDoor_vert(object sender, RoutedEventArgs e)
        {

            if (dimensions.Count == 0)
            {
                return;
            }
            Rectangle d = new Rectangle();
            d.Height = 50;
            d.Width = 10;

            d.StrokeThickness = 2;
            d.Stroke = System.Windows.Media.Brushes.SaddleBrown;

            d.Fill = System.Windows.Media.Brushes.Brown;



            //add to list of shapes 

            d.Name = "Door_l_" + w_i;

            Values r = new Values();
            r.height = 50;
            r.width = 10;
            r.name = "Door_l_" + w_i;

            if (currentRoom != null)
            {
                r.setRoom(currentRoom);
            }
            else if (current_room != null)
            {
                r.setRooom(current_room);
            }


            currentDoor = d;
            draw_door(currentDoor);

            Display_Length.Text = d.Height.ToString();
            Display_Width.Text = d.Width.ToString();

            double x = 0, y = 0;
            string name = null;
            int index = 0;


            if (currentRoom != null)
            {
                x = Canvas.GetLeft(currentRoom);
                y = Canvas.GetTop(currentRoom) + ((currentRoom.Height / 2) - (currentDoor.Height / 2));
                index = returnRoom(currentRoom.Name);
            }
            else if (current_room != null)
            {
                x = Canvas.GetLeft(current_room) + 50;
                y = Canvas.GetTop(current_room) + ((current_room.ActualHeight / 2) - (currentWindow.Height / 2)) + 100;
                index = returnRoom(current_room.Name);
            }


            

            r.X = x;
            r.Y = y;


            for (int i = 0; i < windows.Count; i++)
            {
                if ((windows[i].X == x) && (windows[i].Y == y)){
                    return;
                }
            }
            for (int i = 0; i < doors.Count; i++)
            {
                if ((doors[i].X == x) && (doors[i].Y == y)){
                    return;
                }
            }


            //find room 
            RoomValues rv = dimensions[index];
            if (rv.list.Count == 0)
            {
                rv.setList();
            }

            Canvas.SetLeft(d, x);
            Canvas.SetTop(d, y);

            //add to window list 
            rv.list.Add(r);
            rv.d_list.Add(d);
            dimensions[index] = rv;

            doors.Add(r);
            c.Children.Add(d);

            w_i++;
        }

        public int returnRoom( string name)
        {
            for(int i = 0; i < dimensions.Count(); i++)
            {
                if(dimensions[i].name.Equals(name))
                {
                    return i;
                }

            }
            return -1;
        }

        private void setRoomHeight()
        {
            int length=Int32.Parse(Display_Length.Text.ToString());
            currentRoom.Height = length;

            int i = returnRoom(currentRoom.Name);
            if (i != -1)
            {
                RoomValues x = dimensions[i];
                x.height = length;
                dimensions[i] = x;

            }
            
        }

        
        public void setRoomWidth()
        {
            int width = Int32.Parse(Display_Width.Text.ToString());
            currentRoom.Width = width;
            

            int i = returnRoom(currentRoom.Name);
            if (i != -1)
            {
                RoomValues x = dimensions[i];
                x.width = width;
                dimensions[i] = x;

            }


        }

        public int return_room(String name)
        {
            for(int i = 0; i < dimensions.Count; i++)
            {
                if (dimensions[i].name.Equals(name))
                {
                    return i;
                }
            }
            return -1;
        }

        private void canvas_mouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             result = VisualTreeHelper.HitTest(myCanvas, Mouse.GetPosition(myCanvas));
             


            if (e.ClickCount == 1)
            {

                clicks = 1;
                isDrag = true;
                isResize = false;
                if (e.OriginalSource is Rectangle)
                {
                    Rectangle clicked = (Rectangle)e.OriginalSource;
                    if (clicked.Name.Contains("Room"))
                    {
                        currentRoom = (Rectangle)e.OriginalSource;
                        currentRoom.Stroke = System.Windows.Media.Brushes.Red;
                        Display_Length.Text = currentRoom.Height.ToString();
                        Display_Width.Text = currentRoom.Width.ToString();
                    }
                    



                }
                else if (e.OriginalSource is Polygon)
                {
                    current_room = (Polygon)e.OriginalSource;
                    current_room.Stroke = System.Windows.Media.Brushes.Red;

                    int indexer = return_room(current_room.Name);
                    if (indexer!= -1)
                    {
                        Display_Length.Text = dimensions[indexer].height.ToString();
                        Display_Width.Text = dimensions[indexer].width.ToString();
                    }
                    else
                    {
                        Display_Length.Text = "lol";
                        Display_Width.Text = "lol";
                    }
                    

                }
            }

            if (e.ClickCount == 2)
            {
                clicks = 2;
                isDrag = false;
                isResize = true;
                if (e.OriginalSource is Rectangle)
                {
                    currentRoom = (Rectangle)e.OriginalSource;
                    currentRoom.Stroke = System.Windows.Media.Brushes.Blue;


                }
                else if (e.OriginalSource is Polygon)
                {
                    current_room = (Polygon)e.OriginalSource;
                    current_room.Stroke = System.Windows.Media.Brushes.Blue;
                }
            }




            if (StartPoint == null)
            {
                StartPoint = e.GetPosition(myCanvas);
            }
            StartPoint = CurrentPoint;
            
        }

        private void canvas_mousemove(object sender, MouseEventArgs e)
        {
 
            
            //Fourth block
            if (isDrag == false && isResize == true)
            {
                
            }
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentRoom != null)
            {
                currentRoom.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            }
            if (current_room != null)
            {
                current_room.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            }
            isResize = false;
            isDrag = false;
        }

        

        private void set_Room_width()
        {
            int width = Int32.Parse(Display_Width.Text.ToString());
            PointCollection polygonPoints = current_room.Points;

            int index = return_room(current_room.Name);
            string n = dimensions[index].name;
            float h = dimensions[index].return_height();
            double x = dimensions[index].return_X();
            double y = dimensions[index].return_Y();

            RoomValues r = new RoomValues();
            r.height = h;
            r.width = width;
            r.name = n;
            r.X = x;
            r.Y = y;
            r.setList();
            r.list = dimensions[index].list;
            List<int> dims = dimensions[index].dimens;

            



            if (index != -1)
            {
                float old_w = dimensions[index].width;
                int add = width - (int) old_w;
                if (add > 0)
                {
                    double X = polygonPoints[3].X;
                    double Y = polygonPoints[3].Y;

                    X = X + add;

                    polygonPoints[3] = new Point(X, Y);

                     X = polygonPoints[4].X;
                     Y = polygonPoints[4].Y;

                    X = X + add;
                    dims[1] = dims[1] + add;

                    polygonPoints[4] = new Point(X, Y);

                }
                else
                {
                    double X = polygonPoints[3].X;
                    double Y = polygonPoints[3].Y;

                    X = X - add;

                    polygonPoints[3] = new Point(X, Y);

                    X = polygonPoints[4].X;
                    Y = polygonPoints[4].Y;

                    X = X- add;
                    dims[1] = dims[1] - add;

                    polygonPoints[4] = new Point(X, Y);
                }

               
            }
            r.dimens = dims;
            dimensions[index] = r;


        }

        private void set_Room_height()
        {
            int height = Int32.Parse(Display_Length.Text.ToString());
            PointCollection polygonPoints = current_room.Points;

            int index = return_room(current_room.Name);
            string n = dimensions[index].name;
            float w = dimensions[index].return_width();
            double x = dimensions[index].return_X();
            double y = dimensions[index].return_Y();


            RoomValues r = new RoomValues();
            r.height = height;
            r.width = w;
            r.name = n;
            r.X = x;
            r.Y = y;
            r.setList();
            r.list = dimensions[index].list;
            List<int> dims = dimensions[index].dimens;
            
            if (index != -1)
            {
                float old_w = dimensions[index].height;
                int add = height - (int)old_w;
                if (add > 0)
                {
                    double X = polygonPoints[4].X;
                    double Y = polygonPoints[4].Y;

                    Y = Y + add;

                    polygonPoints[4] = new Point(X, Y);

                    X = polygonPoints[5].X;
                    Y = polygonPoints[5].Y;

                    Y = Y + add;
                    dims[2] = dims[2] + add;
                    polygonPoints[5] = new Point(X, Y);

                }
                else
                {
                    double X = polygonPoints[4].X;
                    double Y = polygonPoints[4].Y;

                    Y = Y - add;

                    polygonPoints[4] = new Point(X, Y);

                    X = polygonPoints[5].X;
                    Y = polygonPoints[5].Y;

                    Y = Y - add;
                    dims[2] = dims[2] - add;
                    polygonPoints[5] = new Point(X, Y);

                }
            }
            dimensions[index] = r;
        }


            private void Open_Menu(object sender, RoutedEventArgs e)
        {
            
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if(currentRoom!=null && currentWindow==null)
            {
              
                    int i = returnRoom(currentRoom.Name);
                    RoomValues r = dimensions[i];
                    //remove windows and doors 
                    List<Values> list = dimensions[i].list;
                    for(int j = 0; i < list.Count; i++)
                    {
                    Values v = windows[i];
                    Rectangle wind = dimensions[i].w_list[j];
                    c.Children.Remove(wind);
                    windows.Remove(v);

                    }
                    
                    c.Children.Remove(currentRoom);
                    dimensions.Remove(r);
                    currentRoom = null;
                
            }
            else if (current_room != null)
            {
                int i = returnRoom(current_room.Name);
                RoomValues r = dimensions[i];
                //remove windows and doors 
                List<Values> list = dimensions[i].list;
                for (int j = 0; i < list.Count; i++)
                {
                    Values v = windows[i];
                   
                    Rectangle wind = dimensions[i].w_list[j];
                    c.Children.Remove(wind);
                    windows.Remove(v);

                }

                c.Children.Remove(current_room);
                dimensions.Remove(r);
                current_room = null;
            }

            else if (currentWindow != null)
            {
                int i = returnWindow(currentWindow.Name);
                Values v = windows[i];
                if (v.GetRoom() != null)
                {
                    Rectangle room = v.GetRoom();
                    int index = returnRoom(room.Name);
                    List<Values> x = dimensions[index].list;
                    for(int j = 0; j < x.Count; j++)
                    {
                        if (x[j].name.Equals(currentWindow.Name))
                        {
                            dimensions[index].list.Remove(x[j]);
                            dimensions[index].w_list.Remove(dimensions[index].w_list[j]);
                        }
                    }
                }
                c.Children.Remove(currentWindow);
                windows.Remove(v);
                currentWindow = null;

            }
        }

        private void NewCanvas(object sender, RoutedEventArgs e)
        {
            currentWindow = null;
            currentRoom = null;
            current_room = null;
            
            dimensions = new List<RoomValues>();
            windows = new List<Values>();

            for(int i = 0; i < c.Children.Count; i++)
            {
                c.Children.Remove(c.Children[i]);
            }
            

        }

        private void Exit(object sender,RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Display_Width_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (currentRoom != null){

                    setRoomWidth();
                }
                else if(current_room!=null)
                {
                    set_Room_width();
                }
                
                
               
            }
        }

       

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (currentRoom != null)
                {

                    setRoomHeight();
                }
                else
                {
                    set_Room_height();
                }



            }
        }

        private void TwoD_Button_Click(object sender, RoutedEventArgs e)
        {
            if (madhouse3d != null)
            {
                unityPanel.Child = null;
                madhouse3d.Dispose();
                madhouse3d = null;
            }
        }

        public void write_coordinates(object sender, RoutedEventArgs e)
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                path += "\\Documents\\MADHouse";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += "\\coords.txt";

                if (File.Exists(path))
                {
                    Console.WriteLine("file exists...");
                    File.Delete(path);
                }
                TextWriter tw = new StreamWriter(path, true);
                for(int i=0;i<dimensions.Count();i++)
                {
                    if (dimensions[i].name.Contains("Room2"))
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y()-30;
                        tw.WriteLine(part + " " + x + " " + y + " " + dimensions[i].dimens[0] + " " + dimensions[i].dimens[1]+" "+ dimensions[i].dimens[2] + " " + dimensions[i].dimens[3]);
                    }
                    else if (dimensions[i].name.Contains("Room1"))
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y();

                        tw.WriteLine(part + " " + x + " " + y + " " + dimensions[i].dimens[0] + " " + dimensions[i].dimens[1] + " " + dimensions[i].dimens[2] + " " + dimensions[i].dimens[3]);
                    }
                
                    else
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y() + 30;
                        tw.WriteLine(part + " " + x + " " +  y + " " + dimensions[i].return_width() + " " + dimensions[i].return_height());
                    }
                }

                for(int i = 0; i < windows.Count; i++)
                {
                    double x = windows[i].return_X();
                    double y = windows[i].return_Y();
                    string part = windows[i].name.Substring(0, windows[i].name.IndexOf('_'));

                    tw.WriteLine(part + " " + x + " " + y + " " + windows[i].return_width() + " " + windows[i].return_height());
                }

                for (int i = 0; i < doors.Count; i++)
                {
                    string part = doors[i].name.Substring(0, doors[i].name.IndexOf('_'));
                    double x = doors[i].return_X();
                    double y = doors[i].return_Y();

                    tw.WriteLine(part + " " + x + " " + y + " " + doors[i].return_width() + " " + doors[i].return_height());
                }
                tw.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (madhouse3d == null)
            {
                madhouse3d = new UnityHwndHost();

                unityPanel.Child = madhouse3d;

            }
        }

        

        public void write_android(object sender, RoutedEventArgs e)
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                path += "\\Documents\\MADHouse";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += "\\coords.txt";

                if (File.Exists(path))
                {
                    Console.WriteLine("file exists...");
                    File.Delete(path);
                }
                TextWriter tw = new StreamWriter(path, true);
                for (int i = 0; i < dimensions.Count(); i++)
                {
                    if (dimensions[i].name.Contains("Room2"))
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y() - 30;
                        tw.WriteLine(part + " " + x + " " + y + " " + dimensions[i].dimens[0] + " " + dimensions[i].dimens[1] + " " + dimensions[i].dimens[2] + " " + dimensions[i].dimens[3]);
                    }
                    else if (dimensions[i].name.Contains("Room1"))
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y();

                        tw.WriteLine(part + " " + x + " " + y + " " + dimensions[i].dimens[0] + " " + dimensions[i].dimens[1] + " " + dimensions[i].dimens[2] + " " + dimensions[i].dimens[3]);
                    }

                    else
                    {
                        string part = dimensions[i].name.Substring(0, dimensions[i].name.IndexOf('_'));
                        double x = dimensions[i].return_X();
                        double y = dimensions[i].return_Y() + 30;
                        tw.WriteLine(part + " " + x + " " + y + " " + dimensions[i].return_width() + " " + dimensions[i].return_height());
                    }
                }

                for (int i = 0; i < windows.Count; i++)
                {
                    double x = windows[i].return_X();
                    double y = windows[i].return_Y();
                    string part = windows[i].name.Substring(0, windows[i].name.IndexOf('_'));

                    tw.WriteLine(part + " " + x + " " + y + " " + windows[i].return_width() + " " + windows[i].return_height());
                }

                for (int i = 0; i < doors.Count; i++)
                {
                    string part = doors[i].name.Substring(0, doors[i].name.IndexOf('_'));
                    double x = doors[i].return_X();
                    double y = doors[i].return_Y();

                    tw.WriteLine(part + " " + x + " " + y + " " + doors[i].return_width() + " " + doors[i].return_height());
                }
                tw.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    
}
namespace MADHouse.Tools
{

    [ValueConversion(typeof(string), typeof(string))]
    public class RatioConverter : MarkupExtension, IValueConverter
    {
        private static RatioConverter _instance;

        public RatioConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { // do not let the culture default to local to prevent variable outcome re decimal syntax
            double size = System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return size.ToString("G0", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { // read only converter...
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new RatioConverter());
        }

    }
}
