using Atestat1.Model;
using Atestat1.Repository;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.cards
{
    internal class TripCard : Panel
    {
        private CityRepository cityRep = new CityRepository();
        private PrevTrip trip;

        private TripCardIconButton view;
        private IconButton from = new IconButton();
        private IconButton to = new IconButton();
        private IconButton length = new IconButton();
        private IconButton created_at = new IconButton();

        public PrevTrip TRIP { get => this.trip; }
        public TripCardIconButton VIEW { get => this.view; set => this.view = value; }

        public TripCard(PrevTrip t, Control parent)
        {
            this.trip = t;
            this.Parent = parent;
            view = new TripCardIconButton(this);

            this.Size = new Size(1300, 50);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.BackColor = GeneralMethods.backColor;

            loadView();

            setCommon();
            loadFrom();
            loadTo();
            loadLength();
            loadCreated_At();

            ToolTip tip = new ToolTip();
            tip.AutoPopDelay = 2000;
            tip.SetToolTip(view, "View trip");
        }

        private void loadView()
        {
            view.Parent = this;
            view.Size = new Size(50, 50);
            view.Location = new Point(10, 0);

            view.BackColor = GeneralMethods.backColor;

            view.TabStop = false;
            view.FlatStyle = FlatStyle.Flat;
            view.FlatAppearance.BorderColor = view.FlatAppearance.MouseOverBackColor =
                view.FlatAppearance.MouseDownBackColor = GeneralMethods.backColor;

            view.IconChar = IconChar.Eye;
            view.IconColor = GeneralMethods.foreColor;
            view.IconSize = 48;
        }

        private void setCommon()
        {
            from.Parent = to.Parent = length.Parent = created_at.Parent = this;
            from.Size = to.Size = length.Size = created_at.Size = new Size(275, 50);

            from.BackColor = to.BackColor = length.BackColor = created_at.BackColor = GeneralMethods.backColor;
            from.ForeColor = to.ForeColor = length.ForeColor = created_at.ForeColor = GeneralMethods.foreColor;

            from.TabStop = to.TabStop = length.TabStop = created_at.TabStop = false;
            from.FlatStyle = to.FlatStyle = length.FlatStyle = created_at.FlatStyle = FlatStyle.Flat;
            
            from.FlatAppearance.BorderColor = from.FlatAppearance.MouseOverBackColor =
                from.FlatAppearance.MouseDownBackColor = to.FlatAppearance.BorderColor =
                to.FlatAppearance.MouseDownBackColor = to.FlatAppearance.MouseOverBackColor =
                length.FlatAppearance.BorderColor = length.FlatAppearance.MouseOverBackColor =
                length.FlatAppearance.MouseDownBackColor = created_at.FlatAppearance.BorderColor =
                created_at.FlatAppearance.MouseOverBackColor = created_at.FlatAppearance.MouseDownBackColor =
                GeneralMethods.backColor;

            from.Font = to.Font = length.Font = created_at.Font =
                new Font(GeneralMethods.fontFam, 11, FontStyle.Bold);
            from.ImageAlign = to.ImageAlign = length.ImageAlign = created_at.ImageAlign =
                ContentAlignment.MiddleLeft;
            from.IconColor = to.IconColor = length.IconColor = created_at.IconColor = GeneralMethods.foreColor;
        }
        private void loadFrom()
        {
            from.Location = new Point(view.Location.X + view.Width + 20
                , (this.Height - from.Height) / 2);
            City c = cityRep.getById(trip.START);
            from.Text = c.CITY_NAME;
            from.IconChar = IconChar.LocationArrow;
        }
        private void loadTo()
        {
            to.Location = new Point(from.Location.X + from.Width + 10,
                from.Location.Y);
            City c = cityRep.getById(trip.END);
            to.Text = c.CITY_NAME;
            to.IconChar = IconChar.LocationCrosshairs;
        }
        private void loadLength()
        {
            length.Location = new Point(to.Location.X + to.Width + 10,
                to.Location.Y);
            length.Text = trip.LENGTH.ToString() + " km";
            length.IconChar = IconChar.RoadBridge;
        }
        private void loadCreated_At()
        {
            created_at.Location = new Point(length.Location.X + length.Width + 10,
                length.Location.Y);
            created_at.Width = 300;
            created_at.Text = trip.CREATED_AT.ToString("MM/dd/yyyy HH:mm:ss");
            created_at.IconChar = IconChar.CalendarDays;
        }
    }
}
