using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GridBoxApp
{
    public partial class Form1 : Form
    {
        private List<Point> points = new List<Point>(); // List to store drawn points
        private int pointSize = 10; // Size of the points

        // Form1 Class Constructor
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.state_zones; // Set the background image (50 United States of America Zones)
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Resize += (s, e) => this.Invalidate(); // Handler to trigger a repaint, or refresh the image
            this.MouseMove += Form1_MouseMove; // Subscribe to MouseMove and MouseClick events
            this.MouseClick += Form1_MouseClick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the base method - ensures other painting is done correctly
            base.OnPaint(e);

            // Draw points
            foreach (Point point in points)
            {
                DrawPointAtLocation(e.Graphics, point);
            }
        }

        // MouseMove event handler
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Mouse Coordinates: X={e.X}, Y={e.Y}"; // Display mouse coordinates
        }

        // MouseClick event handler
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Mouse Clicked: X={e.X}, Y={e.Y}"; // Display mouse click coordinates

            if (e.Button == MouseButtons.Left)
            {
                DrawPoint(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                RemovePoint(e.Location);
            }
        }

        // Method to draw a point at specified coordinates
        private void DrawPoint(Point location)
        {
            points.Add(location); // Add the point to the list of points
            this.Invalidate(); // Invalidate the form to trigger a repaint
        }

        private void RemovePoint(Point location)
        {
            foreach (Point point in points) // Check if there's a point near the clicked location
            {
                if (IsPointClicked(point, location))
                {
                    points.Remove(point); // Remove the point
                    this.Invalidate(); // Trigger to repaint [refresh]
                    break; // Exit loop after removing the point
                }
            }
        }

        // Method to check if a point is clicked
        private bool IsPointClicked(Point point, Point click)
        {
            // Calculate the distance between the point and the click
            double distance = Math.Sqrt(Math.Pow(point.X - click.X, 2) + Math.Pow(point.Y - click.Y, 2));

            // Return true if the distance is within the point's radius (half of the point size)
            return distance <= pointSize / 2;
        }

        // Method to draw a point at a specific location
        private void DrawPointAtLocation(Graphics g, Point location)
        {
            // Calculate the position to draw the point
            int x = location.X - pointSize / 2;
            int y = location.Y - pointSize / 2;

            // Draw the point
            g.FillEllipse(Brushes.Green, x, y, pointSize, pointSize);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
