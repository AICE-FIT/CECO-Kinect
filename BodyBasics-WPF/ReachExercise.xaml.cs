//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.BodyBasics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
 

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class ReachExercise : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as closed
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as opened
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as in lasso (pointer) position
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Drawing group for body rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Reader for body frames
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Array for the bodies
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// definition of bones
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;

        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// List of colors for each body tracked
        /// </summary>
        private List<Pen> bodyColors;

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;

        ///<summary>
        /// Body for Users
        /// Tracking id given by kinect once user is being tracked
        /// </summary>
        private Body userBody1 = null;
        private Body userBody2 = null;
        private string userKinectTrackingID1;
        private string userKinectTrackingID2;

        ///<summary>
        ///Start Event Button
        ///</summary>>
        private Button buttonStartEvent = null;

        ///<summary>
        ///Stopwatch timer for event
        ///</summary>
        private Stopwatch stopWatch;
        
        ///<summary>
        /// event status
        /// </summary>
        private bool isEventStarted = false;

        ///<summary>
        /// radius of circle used for reach detection
        /// </summary>
        private const double cirRadius = 45;

        ///<summary>
        /// DUMMY VARS FOR DUMMY INPUT DATA
        /// </summary>
        private const int sessionDummyID = 1;
        private const int patientnDummyID = 1;
        private const int employeeDummyID = 1;
        private DateTime exerciseDate;

        /// <summary>
        /// Initializes a new instance of the ReachWindow class.
        /// </summary>
        public ReachExercise()
        {
            // one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // get the depth (display) extents
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // get size of joint space
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;

            // open the reader for the body frames
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // a bone defined as a line between two joints
            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // populate body colors, one for each BodyIndex
            this.bodyColors = new List<Pen>();

            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Orange, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Blue, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6));
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.NoSensorStatusText;

            //Tracking kinect set ids
            UserKinectTrackingID1 = "User 1: not tracking";
            UserKinectTrackingID2 = "User 2: not tracking";

            //Set date
            exerciseDate = DateTime.Today;

            //Set timer
            stopWatch = new Stopwatch();

            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // use the window object as the view model in this simple example
            this.DataContext = this;

            // initialize the components (controls) of the window
            this.InitializeComponent();
        }

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }

        /// <summary>
        /// Gets or sets the current status text to display
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }

       
        /// <summary>
        /// Gets or sets the current user kinect tracking id to display for user 1
        /// </summary>
        public string UserKinectTrackingID1
        {
            get
            {
                return this.userKinectTrackingID1;
            }

            set
            {
                if (this.userKinectTrackingID1 != value)
                {
                    this.userKinectTrackingID1 = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UserKinectTrackingID1"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the current user kinect tracking id to display for user 2
        /// </summary>
        public string UserKinectTrackingID2
        {
            get
            {
                return this.userKinectTrackingID2;
            }

            set
            {
                if (this.userKinectTrackingID2 != value)
                {
                    this.userKinectTrackingID2 = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UserKinectTrackingID2"));
                    }
                }
            }
        }

        /// <summary>
        /// Starts Reach Exercise
        /// </summary>
        private void startReachExercise(object sender, RoutedEventArgs e)
        {

            buttonStartEvent = (Button)e.OriginalSource;

            if (userBody1 != null && userBody2 != null)
            {
                buttonStartEvent.IsEnabled = false;
                buttonStartEvent.Visibility = System.Windows.Visibility.Collapsed;
          
                isEventStarted = true;
                stopWatch.Start();

                //record childs hand positions
            }else if (userBody1 == null || userBody2 == null)
            {
                MessageBox.Show("User(s) not being tracked");
            }
        

        }

        /// <summary>
        /// Logic for the exercise. Check if user1 has reached user2
        /// </summary>
        private void determineUserReach()
        {

            CameraSpacePoint lHandPositionUser1 = userBody1.Joints[JointType.HandLeft].Position;
            CameraSpacePoint lHandPositionUser2 = userBody2.Joints[JointType.HandLeft].Position;
            CameraSpacePoint rHandPositionUser1 = userBody1.Joints[JointType.HandRight].Position;
            CameraSpacePoint rHandPositionUser2 = userBody2.Joints[JointType.HandRight].Position;
            

            if (lHandPositionUser1.Z < 0)
            {
                lHandPositionUser1.Z = InferredZPositionClamp;
            }

            if (lHandPositionUser2.Z < 0)
            {
                lHandPositionUser2.Z = InferredZPositionClamp;
            }

            if (rHandPositionUser1.Z < 0)
            {
                rHandPositionUser1.Z = InferredZPositionClamp;
            }

            if (rHandPositionUser2.Z < 0)
            {
                rHandPositionUser2.Z = InferredZPositionClamp;
            }

            DepthSpacePoint lDepth1 = this.coordinateMapper.MapCameraPointToDepthSpace(lHandPositionUser1);
            DepthSpacePoint lDepth2 = this.coordinateMapper.MapCameraPointToDepthSpace(lHandPositionUser2);
            DepthSpacePoint rDepth1 = this.coordinateMapper.MapCameraPointToDepthSpace(rHandPositionUser1);
            DepthSpacePoint rDepth2 = this.coordinateMapper.MapCameraPointToDepthSpace(rHandPositionUser2);

            Point lHandUser1 = new Point(lDepth1.X, lDepth1.Y);
            Point lHandUser2 = new Point(lDepth2.X, lDepth2.Y);
            Point rHandUser1 = new Point(rDepth1.X, rDepth1.Y);
            Point rHandUser2 = new Point(rDepth2.X, rDepth2.Y);

            //Vars for data to be collected. Will be filled in when a success is met
            /*
            Time and distance values will have to be dealt with with something global I suppose? 
            Maybe we can have a global variable for the starting positions of the child's hands that stats null
            and will be recorded when the exercise starts, the same time as the stopwatch, and then set back to null 
            upon the success. I think that should work. -HTC
            */
            double angle = -1;
            string hands = null;
            double time = -1;
            
               /*
               Equation for points x,y that fall within a circle: (x - center_x)^2 + (y - center_y)^2 < radius^2.
               Here, points x,y are set to user1 (the patient). The centers are taken from the hand of user2 (conductor)
               The radius is predefined via a constant
               */

            //first step in if statements should be to stop the stopwatch!
            if ( ( Math.Pow( (lHandUser1.X - lHandUser2.X), 2) + Math.Pow( (lHandUser1.Y - lHandUser2.Y), 2) ) < Math.Pow(cirRadius,2) )
            {
                stopWatch.Stop();
                isEventStarted = false;
                angle = ReachAngle(userBody1.Joints[JointType.ElbowLeft]);
                hands = "ll";
                time = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Reset();

                MessageBox.Show("Patient Reached Hand - L->L");
                buttonStartEvent.IsEnabled = true;
                buttonStartEvent.Visibility = System.Windows.Visibility.Visible;

            } else if ((Math.Pow((lHandUser1.X - rHandUser2.X), 2) + Math.Pow((lHandUser1.Y - rHandUser2.Y), 2)) < Math.Pow(cirRadius, 2))
            {
                stopWatch.Stop();
                isEventStarted = false;
                angle = ReachAngle(userBody1.Joints[JointType.ElbowLeft]);
                hands = "lr";
                time = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Reset();

                MessageBox.Show("Patient Reached Hand - L->R");
                buttonStartEvent.IsEnabled = true;
                buttonStartEvent.Visibility = System.Windows.Visibility.Visible;

            } else if ((Math.Pow((rHandUser1.X - lHandUser2.X), 2) + Math.Pow((rHandUser1.Y - lHandUser2.Y), 2)) < Math.Pow(cirRadius, 2))
            {
                stopWatch.Stop();
                isEventStarted = false;
                angle = ReachAngle(userBody1.Joints[JointType.ElbowRight]);
                hands = "rl";
                time = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Reset();

                MessageBox.Show("Patient Reached Hand - R->L");
                buttonStartEvent.IsEnabled = true;
                buttonStartEvent.Visibility = System.Windows.Visibility.Visible;

            } else if ((Math.Pow((rHandUser1.X - rHandUser2.X), 2) + Math.Pow((rHandUser1.Y - rHandUser2.Y), 2)) < Math.Pow(cirRadius, 2))
            {
                stopWatch.Stop();
                isEventStarted = false;
                angle = ReachAngle(userBody1.Joints[JointType.ElbowRight]);
                hands = "rr";
                time = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Reset();

                MessageBox.Show("Patient Reached Hand - R->R");
                buttonStartEvent.IsEnabled = true;
                buttonStartEvent.Visibility = System.Windows.Visibility.Visible;

            }

            if (!isEventStarted)
            {
                //convert stopwatch data, write data to database
                //Remember to add time and distance next!
                ReportService service = new ReportService();
                service.writeReachData(patientnDummyID, employeeDummyID, sessionDummyID, hands, angle, exerciseDate, time);
                
            }
        }

        private double ReachAngle(Joint elbow)
        {
            Double angle;
            Joint spineShoulder = userBody1.Joints[JointType.SpineShoulder];
            Joint spineMid = userBody1.Joints[JointType.SpineMid];

            Vector3D spineMidVector = new Vector3D(spineMid.Position.X, spineMid.Position.Y, spineMid.Position.Z);
            Vector3D spineShoulderVector = new Vector3D(spineShoulder.Position.X, spineShoulder.Position.Y, spineShoulder.Position.Z);
            Vector3D elbowVector = new Vector3D(elbow.Position.X, elbow.Position.Y, elbow.Position.Z);

            angle = calculateAngles(spineMidVector - spineShoulderVector, spineMidVector - elbowVector);

            return angle;
        }

        private double calculateAngles(Vector3D a, Vector3D b)
        {

            double dproduct = 0.0;
            a.Normalize();
            b.Normalize();

            dproduct = Vector3D.DotProduct(a, b);
            return (double)Math.Acos(dproduct) / Math.PI * 180;
        }

        /// <summary>
        /// Execute start up tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ReachExercise_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ReachExercise_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    // Draw a transparent background to set the render size
                    dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                    //updates skeleton tracking if anyone leaves
                    if (userBody1 != null)
                    {
                        if (!userBody1.IsTracked)
                        {
                            userBody1 = null;
                            UserKinectTrackingID1 = "User 1: not tracking";
                        }
                    }

                    if (userBody2 != null)
                    {
                        if (!userBody2.IsTracked)
                        {
                            userBody2 = null;
                            UserKinectTrackingID2 = "User 2: not tracking";
                        }
                    }

                    int penIndex = 0;
                    foreach (Body body in this.bodies)
                    {                    
                        Pen drawPen = this.bodyColors[penIndex++];

                        if (body.IsTracked)
                        {
                            
                            //Instantiates the users with their respective skeletons, when they are being tracked
                            if (userBody1 == null && body != userBody2)
                            {
                                userBody1 = body;
                                UserKinectTrackingID1 = "User One Tracking ID: " + userBody1.TrackingId;

                            }
                            else if (userBody2 == null && body != userBody1)
                            {
                                userBody2 = body;
                                UserKinectTrackingID2 = "User Two Tracking ID: " + userBody2.TrackingId;
                            }

                            if (body == userBody1 || body == userBody2)
                            {
                                this.DrawClippedEdges(body, dc);

                                IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                                // convert the joint points to depth (display) space
                                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                foreach (JointType jointType in joints.Keys)
                                {
                                    // sometimes the depth(Z) of an inferred joint may show as negative
                                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                                    CameraSpacePoint position = joints[jointType].Position;
                                    if (position.Z < 0)
                                    {
                                        position.Z = InferredZPositionClamp;
                                    }

                                    DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                    jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                }

                                this.DrawBody(joints, jointPoints, dc, drawPen);

                                this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);
                            }  
                        }
                    }
           
                    // prevent drawing outside of our render area
                    this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                }

                //EVENT
                if (isEventStarted)
                {
                    if (userBody1.IsTracked && userBody2.IsTracked)
                    {
                        determineUserReach();
                    }
                    
                
                }
            }
        }

        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(this.handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(this.handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(this.handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, this.displayHeight - ClipBoundsThickness, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(this.displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
            }
        }

        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.SensorNotAvailableStatusText;
        }
    }
}
