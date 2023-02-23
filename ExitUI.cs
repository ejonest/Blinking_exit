//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: Ethan Jones
//Preferred speciality: Graphics 
//Mail: ejonest@csu.fullerton.edu

//Program information
//Program name: ExitSign
//Programming language: C#
//Date this version began: 2022-Aug-22
//Date of last update: 2023-Feb-22
//Files in this program: ExitMain.cs, ExitUI.cs, run.sh

//Purpose
//To draw an exit sign

//This module information
//This module file name: ExitUI.cs
//Language: C Sharp

//Translation information
//The commands for translation from C# source code to binary dll files are found in the bash file, namely run.sh.
//In the event that the run.sh file is lost the compilation command is given here
//mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:UI.dll ExitUI.cs

//Development
//This program was developed on Xubuntu 20.4 using Mono for translation services.  Obtain an installed copy of mono
//by entering the following command in the shell: sudo apt install mono-complete



//Begin source code area.
//The source code is segmented into major blocks.  Each such block has its designated purpose.
//That purpose is expressed at the beginning of each block.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Timers;

public class DrawUI : Form
{  //Declare attributes of the whole form.
   private const int formwidth = 1000;  
   private const int formheight = 600;
   private Size ui_size = new Size(formwidth,formheight);

   //Declare attributes of Header label.
   private Label header_message = new Label();
   private Point header_message_location = new Point(0,0);
   private Font  header_message_font = new Font("Arial",formheight/24,FontStyle.Bold);

   //Declare attributes of Header panel
   private const int header_panel_height = formheight/8; //75
   private Panel header_panel = new Panel();
   private Point header_panel_location = new Point(0,0);
   private Size  header_panel_size = new Size(formwidth,header_panel_height);
   private Color header_panel_color = Color.Crimson;

   //Declare attributes of Graphic panel
   private const int graphic_panel_height = (formheight/8*6)-25; //425
   private Graphic_panel drawingpane = new Graphic_panel();
   private Point graphic_panel_location = new Point(0, header_panel_height); //(0,header_panel_height)
   private Size  graphic_panel_size = new Size(formwidth,graphic_panel_height);
   private Color graphic_panel_color = Color.AntiqueWhite;

   //Declare attributes of the Control panel.
   private const int control_panel_height = formheight/8;//75
   private Panel control_panel = new Panel();
   private Point control_panel_location = new Point(0, header_panel_height+graphic_panel_height);//0,500
   private Size  control_panel_size = new Size(formwidth,control_panel_height);
   private Color control_panel_color = Color.Crimson;   //Color.LightYellow;

   //Declare attributes common to all the buttons that will appear on the Control panel.
   private const int button_height = formheight/15;
   private const int button_width  = formwidth/8;
   private Size  button_size = new Size(button_width,button_height);

   //Declare attributes of the fill ellipse button that will appear on the Control panel.
   private Color fillellipse_button_color = Color.Wheat;
   private Point fillellipse_button_location = new Point(formwidth/10, control_panel_height/5);
   private Button fillellipse_button = new Button();

   //Declare attributes of the speed button that will appear on the Control panel.
   private Color speed_button_color = Color.Wheat;
   private Point speed_button_location = new Point(formwidth / 2 - button_width / 2, control_panel_height/5);
   private Button speed_button = new Button();

   //Declare attributes of the exit button that will appear on the Control panel.
   private Color exit_button_color = Color.Wheat;
   private Point exit_button_location = new Point(formwidth - (formwidth/10) - button_width, control_panel_height/5);//15
   private Button exit_button = new Button();

   //Declare some mechanisms for managing the visibility of displayed geometric shapes.
   private static bool filled_ellipse_visible = false;
   private static bool showing_arrow = false;

   //Declare some values for the clcoks
   private static System.Timers.Timer blink_clock = new System.Timers.Timer();
   private const double fast_clock = 9.0;
   private const double slow_clock = 2.0;
   private const double one_second = 1000.0;
   private const double fast_interval = one_second / fast_clock;
   private const double slow_interval = one_second / slow_clock;
   private int fast_interval_int = (int)System.Math.Round(fast_interval);
   private int slow_interval_int = (int)System.Math.Round(slow_interval);

   public DrawUI()   //The constructor of this class
   {//Set the attributes of this form.
    Text = "Exit Sign";
    Size = ui_size;
    MaximumSize = ui_size;        //This inhibits resizing by the user.
    MinimumSize = ui_size;

    //Construct the header panel
    header_message.Text = "Exit Sign By Ethan Jones";  //Header_message_text;
    header_message.Font = header_message_font;
    header_message.ForeColor = Color.AntiqueWhite;
    header_message.TextAlign = ContentAlignment.MiddleCenter;
    header_message.Size = new Size(formwidth,formheight/12);
    header_message.Location = header_message_location;
    header_panel.BackColor = header_panel_color;
    header_panel.Size = header_panel_size;
    header_panel.Location = header_panel_location;
    header_panel.Controls.Add(header_message);

    //Construct the Exit in the middle
    // Creating and setting the label
        Label Exit_Text = new Label();
        Exit_Text.Text = "Exit";
        Exit_Text.Location = new Point(formwidth/3, formheight/8);
        Exit_Text.AutoSize = true;
        Exit_Text.Font = new Font("Calibri", formwidth/9);
        Exit_Text.ForeColor = Color.Crimson;
        Exit_Text.BackColor = Color.AntiqueWhite;
        Exit_Text.Padding = new Padding(6);

        // Adding this control to the form
        this.Controls.Add(Exit_Text);

    //Construct the middle panel also called the "graphic panel".
    drawingpane.BackColor = graphic_panel_color;
    drawingpane.Size = graphic_panel_size;
    drawingpane.Location = graphic_panel_location;

    //Construct the bottom panel also called the "control panel".
    control_panel.BackColor = control_panel_color;
    control_panel.Size = control_panel_size;
    control_panel.Location = control_panel_location;

    //Construct the fill ellipse button
    fillellipse_button.BackColor = fillellipse_button_color;
    fillellipse_button.Size = button_size;
    fillellipse_button.Location = fillellipse_button_location;
    fillellipse_button.Text = "Start";
    fillellipse_button.TextAlign = ContentAlignment.MiddleCenter;
    fillellipse_button.Click += new EventHandler(fill_ellipse);
    fillellipse_button.TabIndex = 5;
    fillellipse_button.TabStop = true;

    //Construct the exit button
    exit_button.BackColor = exit_button_color;
    exit_button.Size = button_size;
    exit_button.Location = exit_button_location;
    exit_button.Text = "Exit";
    exit_button.TextAlign = ContentAlignment.MiddleCenter;
    exit_button.Click += new EventHandler(terminate_execution);
    exit_button.TabIndex = 5;
    exit_button.TabStop = true;

    //Construct the speed button
    speed_button.BackColor = speed_button_color;
    speed_button.Size = button_size;
    speed_button.Location = speed_button_location;
    speed_button.Text = "Fast";
    speed_button.TextAlign = ContentAlignment.MiddleCenter;
    speed_button.Click += new EventHandler(change_speed);
    speed_button.TabIndex = 5;
    speed_button.TabStop = true;

    //Add buttons to the control panel
    control_panel.Controls.Add(fillellipse_button);
    control_panel.Controls.Add(exit_button);
    control_panel.Controls.Add(speed_button);

    //Add panels to the UI form
    Controls.Add(header_panel);
    Controls.Add(drawingpane);
    Controls.Add(control_panel);

    blink_clock.Enabled = false;
    blink_clock.Elapsed += new ElapsedEventHandler(arrow_blink);
    blink_clock.Interval = slow_interval_int;
    //blink_clock.Enabled = true;


   }//End of constructor

   //Method to execute when the fill ellipse button receives a mouse click
   protected void fill_ellipse(Object sender, EventArgs h){
     if(showing_arrow) {
        showing_arrow = false;
        filled_ellipse_visible = false;
        fillellipse_button.Text = "Resume";
        blink_clock.Enabled = false;
       }
    else {
        showing_arrow = true;
        filled_ellipse_visible = true;
        fillellipse_button.Text = "Pause";
        blink_clock.Enabled = true;
       }
    drawingpane.Invalidate();
   }//End of method fill_ellipse

   protected void change_speed(Object sender, EventArgs g){
     if(blink_clock.Interval == slow_interval_int){
        blink_clock.Interval = fast_interval_int;
        speed_button.Text = "Slow";
      }
      else {
        blink_clock.Interval = slow_interval_int;
        speed_button.Text = "Fast";
     }
   }

   //arrow_blink
   protected void arrow_blink(System.Object sender, ElapsedEventArgs evt) {
    if(filled_ellipse_visible) {
         filled_ellipse_visible = false;
       }
    else {
         filled_ellipse_visible = true;
       }
    drawingpane.Invalidate();
   }

   //This is the handler function for the exit button.
   protected void terminate_execution(Object sender, EventArgs i)
   {System.Console.WriteLine("This program will end execution.");
    Close();
   }

   //The next block created a new derived class from Panel class.  Inside of the derived class
   //is a copy of the OnPaint function.  The presence of OnPaint allows the program to call
   //graphic functions such as DrawLine, DrawRectangle, and others.
   public class Graphic_panel: Panel            //Class Graphicpanel inherits from class Panel
   {public Graphic_panel()                      //Constructor for derived class Graphicpanel
    {Console.WriteLine("A graphic enabled panel was created");
    }//End of constructor
    
    protected override void OnPaint(PaintEventArgs ee)
    {Graphics graph = ee.Graphics;
     /*If filled_ellipse_visible is true then we want it to blink. So we can do while that is true we show then blink. Do that by
     making filled_ellipse_visible true wait a second then make it false and keep the ifs we have*/
    for (int i = 0; i < 11; i++){
      if(filled_ellipse_visible){
          graph.FillEllipse(Brushes.Crimson,formwidth / 20 * i + (formwidth/4),formheight/2 ,formwidth/40,formwidth/40); //50 * i + 250
      }
    }
    int j = 3;
    for (int i = 0; i < 3; i++){
      if(filled_ellipse_visible) graph.FillEllipse(Brushes.Crimson,formwidth / 20 * (i + 7) + (formwidth/4),formheight/2 + (30 * j) ,formwidth/40,formwidth/40); //50 * i + 250
      if(filled_ellipse_visible) graph.FillEllipse(Brushes.Crimson,formwidth / 20 * (i + 7) + (formwidth/4),formheight/2 - (30 * j) ,formwidth/40,formwidth/40);
      j--;
    }


     base.OnPaint(ee);
    }//End of OnPaint belonging only to Graph Panel class.

   }//End of derived class Graphicpanel

}//End of class DrawUI